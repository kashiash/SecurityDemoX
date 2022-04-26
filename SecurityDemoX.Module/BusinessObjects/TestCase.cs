using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
using System.Linq;

namespace SecurityDemoX.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class TestCase : BaseObject
    {
        public TestCase(Session session) : base(session)
        {
        }


        int odometer;
        CaseStatus caseStatus;
        string description;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
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


        public CaseStatus CaseStatus
        {
            get { return caseStatus; }
            set
            {
                SetPropertyValue(
                    nameof(CaseStatus),
                    ref caseStatus,
                    value);
            }
        }


        public int Odometer
        {
            get { return odometer; }
            set
            {
                SetPropertyValue(
                    nameof(Odometer),
                    ref odometer,
                    value);
            }
        }


        [Association("TestCase-CaseItems"),Aggregated]
        public XPCollection<CaseItem> CaseItems
        {
            get
            {
                return GetCollection<CaseItem>(
                    nameof(CaseItems));
            }
        }
    }

    public enum CaseStatus
    {
        New,
        InProgres,
        Completed,
        Rejected
    }


    [DefaultClassOptions]
    public class CaseItem : BaseObject
    {
        public CaseItem(Session session) : base(session)
        {
        }


        int odometer;
        TestCase testCase;

        [Association("TestCase-CaseItems")]
        public TestCase TestCase
        {
            get { return testCase; }
            set
            {
                SetPropertyValue(
                    nameof(TestCase),
                    ref testCase,
                    value);
            }
        }


        public int Odometer
        {
            get { return odometer; }
            set
            {
                SetPropertyValue(
                    nameof(Odometer),
                    ref odometer,
                    value);
            }
        }


        [NonPersistent]
        [Browsable(false)]
        [RuleFromBoolProperty(
            "TestCase_OdometerIntervalValid",
            DefaultContexts.Save,
            "Odometer should be greater then saved odometer!",
            SkipNullOrEmptyValues = false,
            UsedProperties = "Odometer")]
        public bool OdometerIsIntervalValid
        {
            get
            {
                if(TestCase == null)
                    return true;


                return Odometer > TestCase.Odometer;
            }
        }
    }
}
