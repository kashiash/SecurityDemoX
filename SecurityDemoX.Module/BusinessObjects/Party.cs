using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
using System.Linq;

namespace SecurityDemoX.Module.BusinessObjects
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    [DefaultProperty(nameof(DisplayName))]
    public abstract class Party : BaseObject
    {
        private Address address1;
        private Address address2;
        protected Party(Session session) : base(session)
        {
        }
        public override string ToString()
        { return DisplayName; }
        [Size(SizeAttribute.Unlimited), Delayed(true)]
        [ImageEditor]
        public byte[] Photo
        {
            get
            {
                return GetDelayedPropertyValue<byte[]>(
                    nameof(Photo));
            }
            set
            {
                SetDelayedPropertyValue<byte[]>(
                    nameof(Photo),
                    value);
            }
        }

        [Aggregated, ExpandObjectMembers(
            ExpandObjectMembers.Never)]
        public Address Address1
        {
            get { return address1; }
            set
            {
                SetPropertyValue(
                    nameof(Address1),
                    ref address1,
                    value);
            }
        }

        [Aggregated, ExpandObjectMembers(
            ExpandObjectMembers.Never)]
        public Address Address2
        {
            get { return address2; }
            set
            {
                SetPropertyValue(
                    nameof(Address2),
                    ref address2,
                    value);
            }
        }


        [ObjectValidatorIgnoreIssue(
            typeof(ObjectValidatorDefaultPropertyIsVirtual),
            typeof(ObjectValidatorDefaultPropertyIsNonPersistentNorAliased))]
        public abstract string DisplayName { get; }

        [Aggregated, Association("Party-PhoneNumbers")]
        public XPCollection<PhoneNumber> PhoneNumbers
        {
            get
            {
                return GetCollection<PhoneNumber>(
                    nameof(PhoneNumbers));
            }
        }

        [Association("Party-Roles")]
        public XPCollection<PartyRole> Roles
        {
            get
            {
                return GetCollection<PartyRole>(
                    nameof(Roles));
            }
        }
    }
}
