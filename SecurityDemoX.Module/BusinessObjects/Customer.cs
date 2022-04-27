﻿using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace SecurityDemoX.Module.BusinessObjects
{
    [DefaultClassOptions]
    [MapInheritance(MapInheritanceType.ParentTable)]
    [XafDefaultProperty(nameof(FullName))]
    public class Customer : PartyRole, IPartyRoleType
    {
        [ObjectValidatorIgnoreIssue(
            typeof(ObjectValidatorDefaultPropertyIsVirtual))]
        public virtual string FullName
        {
            get
            {
                return ObjectFormatter.Format(
                    $"{Party?.DisplayName} ; {Party?.Address1?.FullAddress}",
                    this,
                    EmptyEntriesMode.RemoveDelimiterWhenEntryIsEmpty);
            }
        }

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
