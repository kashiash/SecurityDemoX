using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace SecurityDemoX.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class PartyRoleType : BaseObject
    {
        public PartyRoleType(Session session) : base(
            session)
        {
        }

        string description;
        string name;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name
        {
            get { return name; }
            set
            {
                SetPropertyValue(
                    nameof(Name),
                    ref name,
                    value);
            }
        }


        [Size(SizeAttribute.Unlimited)]
        public string Description
        {
            get { return description; }
            set
            {
                SetPropertyValue(
                    nameof(Description),
                    ref description,
                    value);
            }
        }
    }
}
