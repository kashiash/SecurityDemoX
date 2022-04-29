using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using SecurityDemoX.Module.Services;

namespace SecurityDemoX.Module.BusinessObjects
{
    [DefaultClassOptions]
    [MapInheritance(MapInheritanceType.ParentTable)]
    public class Customer : PartyRole, IParty, IPartyRoleType
    {
        public Customer(Session session) : base(session)
        {
        }

        [Association("Customer-Invoices")]
        public XPCollection<Invoice> Invoices
        {
            get
            {
                return GetCollection<Invoice>(
                    nameof(Invoices));
            }
        }


	}
}
