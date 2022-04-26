namespace SecurityDemoX.Module.BusinessObjects
{
    [DefaultClassOptions]
    [XafDefaultProperty(nameof(InvoiceNumber))]


    public class Invoice : BaseObject
    {
        public Invoice(Session session) : base(session)
        {
        }

        //[Browsable(false)]
        public bool Overdue => SumOfPayments < TotalBrutto &&
            PaymentDate < DateTime.Now;

        DateTime paymentDate;
        decimal sumOfPayments;
        string notes;
        decimal totalBrutto;
        decimal totalVat;
        decimal totalNetto;
        Customer customer;
        DateTime dueDate;
        DateTime invoiceDate;
        string invoiceNumber;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleRequiredField]
        [RuleUniqueValue]
        public string InvoiceNumber
        {
            get { return invoiceNumber; }
            set
            {
                SetPropertyValue(
                    nameof(InvoiceNumber),
                    ref invoiceNumber,
                    value);
            }
        }


        public DateTime InvoiceDate
        {
            get { return invoiceDate; }
            set
            {
                SetPropertyValue(
                    nameof(InvoiceDate),
                    ref invoiceDate,
                    value);
            }
        }


        public DateTime DueDate
        {
            get { return dueDate; }
            set
            {
                SetPropertyValue(
                    nameof(DueDate),
                    ref dueDate,
                    value);
            }
        }


        public decimal SumOfPayments
        {
            get { return sumOfPayments; }
            set
            {
                SetPropertyValue(
                    nameof(SumOfPayments),
                    ref sumOfPayments,
                    value);
            }
        }


        public DateTime PaymentDate
        {
            get { return paymentDate; }
            set
            {
                SetPropertyValue(
                    nameof(PaymentDate),
                    ref paymentDate,
                    value);
            }
        }

        [Association("Customer-Invoices")]
        public Customer Customer
        {
            get { return customer; }
            set
            {
                SetPropertyValue(
                    nameof(Customer),
                    ref customer,
                    value);
            }
        }

        [ModelDefault("AllowEdit", "False")]
        public decimal TotalNetto
        {
            get { return totalNetto; }
            set
            {
                SetPropertyValue(
                    nameof(TotalNetto),
                    ref totalNetto,
                    value);
            }
        }

        [ModelDefault("AllowEdit", "False")]
        public decimal TotalVat
        {
            get { return totalVat; }
            set
            {
                SetPropertyValue(
                    nameof(TotalVat),
                    ref totalVat,
                    value);
            }
        }

        [ModelDefault("AllowEdit", "False")]
        public decimal TotalBrutto
        {
            get { return totalBrutto; }
            set
            {
                SetPropertyValue(
                    nameof(TotalBrutto),
                    ref totalBrutto,
                    value);
            }
        }

        [DetailViewLayoutAttribute(
            "ItemsNotes",
            LayoutGroupType.TabbedGroup,
            100)]
        [Association, DevExpress.Xpo.Aggregated]
        public XPCollection<InvoiceItem> InvoiceItems
        {
            get
            {
                return GetCollection<InvoiceItem>(
                    nameof(InvoiceItems));
            }
        }


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

        internal void RecalculateTotals(
            bool forceChangeEvents = true)
        {
            decimal oldNetto = TotalNetto;
            decimal oldVAT = TotalVat;
            decimal oldBrutto = TotalBrutto;


            decimal tmpNetto = 0m;
            decimal tmpVAT = 0m;
            decimal tmpBrutto = 0m;

            foreach (var rec in InvoiceItems)
            {
                tmpNetto += rec.Netto;
                tmpVAT += rec.Vat;
                tmpBrutto += rec.Brutto;
            }
            TotalNetto = tmpNetto;
            TotalVat = tmpVAT;
            TotalBrutto = tmpBrutto;

            var a = totalBrutto;

            if (forceChangeEvents)
            {
                OnChanged(
                    nameof(TotalNetto),
                    oldNetto,
                    TotalNetto);
                OnChanged(
                    nameof(TotalVat),
                    oldVAT,
                    TotalVat);
                OnChanged(
                    nameof(TotalBrutto),
                    oldBrutto,
                    TotalBrutto);
            }
        }
    }
}
