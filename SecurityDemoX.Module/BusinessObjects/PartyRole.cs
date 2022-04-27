using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System.Linq;

namespace SecurityDemoX.Module.BusinessObjects
{
    public class PartyRole : BaseObject
    {
        public PartyRole(Session session) : base(session)
        {
        }


        PartyRoleType partyRoleType;
        Party party;
        string description;
        string name;


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


        [ModelDefault(nameof(IModelCommonMemberViewItem.AllowEdit),
            "false")]
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

		public override void AfterConstruction()
		{
			base.AfterConstruction();
            partyRoleType = Session.Query<PartyRoleType>().Where(x => x.Name == GetType().Name).FirstOrDefault();
		}
	}
}
