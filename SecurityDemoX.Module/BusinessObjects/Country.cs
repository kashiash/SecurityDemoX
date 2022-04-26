﻿using DevExpress.Persistent.Base.General;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
using System.Linq;

namespace SecurityDemoX.Module.BusinessObjects
{
    [DefaultProperty(nameof(Name))]
    public class Country : BaseObject, ICountry
    {
        private string name;
        private string phoneCode;
        public Country(Session session) : base(session)
        {
        }
        public override string ToString() { return Name; }

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

        public string PhoneCode
        {
            get { return phoneCode; }
            set
            {
                SetPropertyValue(
                    nameof(PhoneCode),
                    ref phoneCode,
                    value);
            }
        }
    }

    [DefaultProperty(nameof(LongName))]
    public class State : BaseObject
    {
        private string shortName = string.Empty;
        private string longName = string.Empty;
        public State(Session session) : base(session)
        {
        }

        public string ShortName
        {
            get { return shortName; }
            set
            {
                SetPropertyValue(
                    nameof(ShortName),
                    ref shortName,
                    value);
            }
        }

        public string LongName
        {
            get { return longName; }
            set
            {
                SetPropertyValue(
                    nameof(LongName),
                    ref longName,
                    value);
            }
        }
    }
}
