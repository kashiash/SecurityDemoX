using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Editors;
using System;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace SecurityDemoX.Module.BusinessObjects.NonPersistent
{
	[DomainComponent]
	public class EnterPartyRole : NonPersistentLiteObject
	{
		private PartyRole partyRole;

		[EditorAlias(EditorAliases.DetailPropertyEditor)]
		[ExpandObjectMembers(ExpandObjectMembers.Never)]
		public PartyRole PartyRole
		{
			get => partyRole;
			set => SetPropertyValue(ref partyRole, value);
		}


		public void SetPartyRoleType(IObjectSpace objectSpace, Type partyRoleType, Type partyType)
		{
			PartyRole = objectSpace.CreateObject(partyRoleType) as PartyRole;
			PartyRole.Party = objectSpace.CreateObject(partyType) as Party;
		}
	}
}