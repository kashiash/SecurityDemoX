using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace SecurityDemoX.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class Supplier : PartyRole, IPartyRoleType
    {
        public Supplier(Session session) : base(session)
        {
        }
    }
}
