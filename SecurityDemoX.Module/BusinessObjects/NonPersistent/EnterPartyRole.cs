using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Editors;
using System;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace SecurityDemoX.Module.BusinessObjects.NonPersistent
{
	[DomainComponent]
	public class EnterPartyRole : NonPersistentBaseObject
	{
		private Party party;
		private PartyRole partyRole;
		private bool isPerson;
		private bool isOrganization;


		[ImmediatePostData]
		public bool IsOrganization
		{
			get => isOrganization;
			set
			{
				bool modified = SetPropertyValue(ref isOrganization, value);
				if (modified)
				{
					if (value == true)
					{
						IsPerson = false;
						if (Party != null) Party.Delete();
						Party = ObjectSpace.CreateObject<Organization>();
					}
				}
			}
		}


		[ImmediatePostData]
		public bool IsPerson
		{
			get => isPerson;
			set
			{
				bool modified = SetPropertyValue(ref isPerson, value);
				if (modified)
				{
					if (value == true)
					{
						IsOrganization = false;
						if (Party != null) Party.Delete();
						Party = ObjectSpace.CreateObject<Person>();
					}
				}
			}
		}


		[EditorAlias(EditorAliases.DetailPropertyEditor)]
		[ExpandObjectMembers(ExpandObjectMembers.Never)]
		public Party Party
		{
			get => party;
			set => SetPropertyValue(ref party, value);
		}



		[EditorAlias(EditorAliases.DetailPropertyEditor)]
		[ExpandObjectMembers(ExpandObjectMembers.Never)]
		public PartyRole PartyRole
		{
			get => partyRole;
			set => SetPropertyValue(ref partyRole, value);
		}


		public void SetPartyRoleType(Type partyRoleType)
		{
			if (partyRoleType == typeof(Customer))
			{
				PartyRole = ObjectSpace.CreateObject<Customer>();
			}
		}
	}
}