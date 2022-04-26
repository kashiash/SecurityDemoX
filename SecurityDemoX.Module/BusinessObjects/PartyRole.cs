using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using DevExpress.ExpressApp.Editors;

namespace SecurityDemoX.Module.BusinessObjects
{
    public class PartyRole : BaseObject
    {
        public PartyRole(Session session) : base(session) { }

   
        PartyRoleType partyRoleType;
        Party party;
        string description;
        string name;


        [Association("Party-Roles")]
        [EditorAlias(EditorAliases.DetailPropertyEditor)]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        public Party Party
        {
            get => party;
            set => SetPropertyValue(nameof(Party), ref party, value);
        }


        public PartyRoleType PartyRoleType
        {
            get => partyRoleType;
            set => SetPropertyValue(nameof(PartyRoleType), ref partyRoleType, value);
        }

 
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }

        
        [Size(SizeAttribute.Unlimited)]
        public string Description
        {
            get => description;
            set => SetPropertyValue(nameof(Description), ref description, value);
        }


		public virtual PartyRole CreatePersistentPartyRole<T>(IObjectSpace objectSpace) where T : PartyRole
		{
            var partyRole = objectSpace.CreateObject<T>();
            partyRole.Name = Name;
            partyRole.Description = Description;
            return partyRole;
        }
	}
}
