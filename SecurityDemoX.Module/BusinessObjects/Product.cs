using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

using System;
using System.Linq;

namespace SecurityDemoX.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class Product : BaseObject
    {
        public Product(Session session) : base(session)
        {
        }

        string shortName;

        string notes;
        string gTIN;
        string productName;
        string symbol;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Symbol
        {
            get { return symbol; }
            set
            {
                SetPropertyValue(
                    nameof(Symbol),
                    ref symbol,
                    value);
            }
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string ProductName
        {
            get { return productName; }
            set
            {
                SetPropertyValue(
                    nameof(ProductName),
                    ref productName,
                    value);
            }
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string ShortName
        {
            get { return shortName; }
            set
            {
                SetPropertyValue(
                    nameof(ShortName),
                    ref shortName,
                    value);
            }
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string GTIN
        {
            get { return gTIN; }
            set
            {
                SetPropertyValue(
                    nameof(GTIN),
                    ref gTIN,
                    value);
            }
        }


        VatRate vatRate;
        decimal unitPrice;

        public decimal UnitPrice
        {
            get { return unitPrice; }
            set
            {
                SetPropertyValue(
                    nameof(UnitPrice),
                    ref unitPrice,
                    value);
            }
        }


        public VatRate VatRate
        {
            get { return vatRate; }
            set
            {
                SetPropertyValue(
                    nameof(VatRate),
                    ref vatRate,
                    value);
            }
        }

        [DetailViewLayout(
            "GroupsAndNotes",
            LayoutGroupType.TabbedGroup,
            100)]
        [Size(SizeAttribute.Unlimited)]
        public string Notes
        {
            get { return notes; }
            set
            {
                SetPropertyValue(
                    nameof(Notes),
                    ref notes,
                    value);
            }
        }
    }
}
