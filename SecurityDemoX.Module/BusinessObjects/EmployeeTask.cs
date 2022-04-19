using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;


namespace SecurityDemoX.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("BO_Task")]
    [DefaultProperty("Subject")]
    [ModelDefault("Caption", "Task")]
    public class EmployeeTask : BaseObject
    {
        private string _Description;
        private string _Subject;
        private DateTime _DueDate;
        private TaskStatus _Status;
        private Employee _AssignedTo;
        public EmployeeTask(Session session) : base(session)
        {
        }
        [Size(255)]
        public string Subject
        {
            get { return _Subject; }
            set { SetPropertyValue(nameof(Subject), ref _Subject, value); }
        }

        public TaskStatus Status
        {
            get { return _Status; }
            set { SetPropertyValue(nameof(Status), ref _Status, value); }
        }

        [Size(SizeAttribute.Unlimited)]
        public string Description
        {
            get { return _Description; }
            set { SetPropertyValue(nameof(Description), ref _Description, value); }
        }

        public DateTime DueDate
        {
            get { return _DueDate; }
            set { SetPropertyValue(nameof(DueDate), ref _DueDate, value); }
        }

        [Action(ToolTip = "Postpone the task to the next day", ImageName = "State_Task_Deferred")]
        public void Postpone()
        {
            if (DueDate == DateTime.MinValue)
            {
                DueDate = DateTime.Now;
            }
            DueDate = DueDate + TimeSpan.FromDays(1);
        }

        [Association]
        [RuleRequiredField]
        public Employee AssignedTo
        {
            get { return _AssignedTo; }
            set { SetPropertyValue(nameof(AssignedTo), ref _AssignedTo, value); }
        }
    }
}
