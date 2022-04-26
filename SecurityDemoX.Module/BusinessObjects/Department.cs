﻿using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
using System.Linq;

namespace SecurityDemoX.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("BO_Department")]
    [DefaultProperty("Title")]
    public class Department : BaseObject
    {
        private string title;
        private string office;
        public Department(Session session) : base(session)
        {
        }

        public string Title
        {
            get { return title; }
            set
            {
                SetPropertyValue(
                    nameof(Title),
                    ref title,
                    value);
            }
        }

        public string Office
        {
            get { return office; }
            set
            {
                SetPropertyValue(
                    nameof(Office),
                    ref office,
                    value);
            }
        }

        [Association]
        public XPCollection<Employee> Employees
        {
            get
            {
                return GetCollection<Employee>(
                    nameof(Employees));
            }
        }
    }
}
