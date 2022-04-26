using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace SecurityDemoX.Module.BusinessObjects
{
    public class PartyRole : BaseObject
    {
        public PartyRole(Session session) : base(session)
        {
        }


        PartyRoleType partyRoleType;
        Party party;
        [Association("Party-Roles")]
        [EditorAlias(EditorAliases.DetailPropertyEditor)]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        public Party Party
        {
            get { return party; }
            set
            {
                SetPropertyValue(
                    nameof(Party),
                    ref party,
                    value);
            }
        }


        public PartyRoleType PartyRoleType
        {
            get { return partyRoleType; }
            set
            {
                SetPropertyValue(
                    nameof(PartyRoleType),
                    ref partyRoleType,
                    value);
            }
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
