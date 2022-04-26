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


        public virtual PartyRole CreatePersistentPartyRole<T>(IObjectSpace objectSpace) where T : PartyRole
        {
            var partyRole = objectSpace.CreateObject<T>();
            partyRole.Name = Name;
            partyRole.Description = Description;
            return partyRole;
        }
    }
}
