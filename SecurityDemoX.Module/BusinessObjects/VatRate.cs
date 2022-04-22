using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Linq;

namespace SecurityDemoX.Module.BusinessObjects
{
    [DefaultClassOptions]
    [NavigationItem("Others")]
    public class VatRate : XPLiteObject
    {
        public VatRate(Session session) : base(session)
        {
        }


        decimal rateValue;
        string symbol;

        [Size(3)]
        [Key(false)]
        public string Symbol { get { return symbol; } set { SetPropertyValue(nameof(Symbol), ref symbol, value); } }


        public decimal Value
        {
            get { return rateValue; }
            set { SetPropertyValue(nameof(Value), ref rateValue, value); }
        }
    }
}
