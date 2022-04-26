using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

using System;
using System.Linq;

namespace SecurityDemoX.Module.BusinessObjects
{
    [VisibleInDashboards]
    [VisibleInReports]
    public class InvoiceItem : BaseObject
    {
        public InvoiceItem(Session session) : base(session)
        {
        }

        Invoice invoice;
        decimal brutto;
        decimal vat;
        decimal netto;
        decimal unitPrice;
        decimal quantity;
        Product product;

        [ImmediatePostData]
        public Product Product
        {
            get { return product; }
            set
            {
                bool modified = SetPropertyValue(
                    nameof(Product),
                    ref product,
                    value);
                if(modified &&
                    !IsLoading &&
                    !IsSaving &&
                    Product != null)
                {
                    unitPrice = Product.UnitPrice;
                    vatRate = Product.VatRate;
                    RecalculateItem();
                }
            }
        }


        [Association]
        public Invoice Invoice
        {
            get { return invoice; }
            set
            {
                var oldInvoice = invoice;
                bool modified = SetPropertyValue(
                    nameof(Invoice),
                    ref invoice,
                    value);
                if(!IsLoading && !IsSaving && modified)
                {
                    oldInvoice = oldInvoice ?? invoice;
                    oldInvoice.RecalculateTotals(true);
                }
            }
        }

        [ImmediatePostData]
        public decimal Quantity
        {
            get { return quantity; }
            set
            {
                bool modified = SetPropertyValue(
                    nameof(Quantity),
                    ref quantity,
                    value);
                if(modified && !IsLoading && !IsSaving)
                {
                    RecalculateItem();
                }
            }
        }

        [ImmediatePostData]
        public decimal UnitPrice
        {
            get { return unitPrice; }
            set
            {
                bool modified = SetPropertyValue(
                    nameof(UnitPrice),
                    ref unitPrice,
                    value);
                if(modified && !IsLoading && !IsSaving)
                {
                    RecalculateItem();
                }
            }
        }

        VatRate vatRate;
        [ImmediatePostData]
        public VatRate VatRate
        {
            get { return vatRate; }
            set
            {
                bool modified = SetPropertyValue(
                    nameof(VatRate),
                    ref vatRate,
                    value);
                if(modified && !IsLoading && !IsSaving)
                {
                    RecalculateItem();
                }
            }
        }

        [ModelDefault("AllowEdit", "False")]
        public decimal Netto
        {
            get { return netto; }
            set
            {
                SetPropertyValue(
                    nameof(Netto),
                    ref netto,
                    value);
            }
        }

        [ModelDefault("AllowEdit", "False")]
        public decimal Vat
        {
            get { return vat; }
            set
            {
                SetPropertyValue(
                    nameof(Vat),
                    ref vat,
                    value);
            }
        }

        [ModelDefault("AllowEdit", "False")]
        public decimal Brutto
        {
            get { return brutto; }
            set
            {
                SetPropertyValue(
                    nameof(Brutto),
                    ref brutto,
                    value);
            }
        }

        private void RecalculateItem()
        {
            Netto = Quantity * UnitPrice;
            if(Product != null && Product.VatRate != null)
            {
                Brutto = Netto *
                    (100 + Product.VatRate.Value) /
                    100;
            } else
            {
                Brutto = Netto;
            }
            Vat = Brutto - Netto;

            if(Invoice != null)
            {
                Invoice.RecalculateTotals(true);
            }
        }
    }
}
