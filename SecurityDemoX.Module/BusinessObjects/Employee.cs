using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace SecurityDemoX.Module.BusinessObjects {
    [MapInheritance(MapInheritanceType.ParentTable)]
    [DefaultProperty(nameof(UserName))]
    public class Employee : PermissionPolicyUser, IObjectSpaceLink, ISecurityUserWithLoginInfo {
        public Employee(Session session) : base(session) { }

        [Browsable(false)]
        [Aggregated, Association("User-LoginInfo")]
        public XPCollection<ApplicationUserLoginInfo> LoginInfo {
            get { return GetCollection<ApplicationUserLoginInfo>(nameof(LoginInfo)); }
        }

        IEnumerable<ISecurityUserLoginInfo> IOAuthSecurityUser.UserLogins => LoginInfo.OfType<ISecurityUserLoginInfo>();

        IObjectSpace IObjectSpaceLink.ObjectSpace { get; set; }

        ISecurityUserLoginInfo ISecurityUserWithLoginInfo.CreateUserLoginInfo(string loginProviderName, string providerUserKey) {
            ApplicationUserLoginInfo result = ((IObjectSpaceLink)this).ObjectSpace.CreateObject<ApplicationUserLoginInfo>();
            result.LoginProviderName = loginProviderName;
            result.ProviderUserKey = providerUserKey;
            result.User = this;
            return result;
        }

        private string _LastName;
        private string _FirstName;
        private Department department;

        public string FirstName
        {
            get { return _FirstName; }
            set { SetPropertyValue(nameof(FirstName), ref _FirstName, value); }
        }

        public string LastName
        {
            get { return _LastName; }
            set { SetPropertyValue(nameof(LastName), ref _LastName, value); }
        }

        [PersistentAlias("concat(FirstName, ' ', LastName)")]
        public string FullName { get { return Convert.ToString(EvaluateAlias("FullName")); } }

        [Association]
        [RuleRequiredField]
        public Department Department
        {
            get { return department; }
            set { SetPropertyValue(nameof(Department), ref department, value); }
        }

        [Association]
        public XPCollection<EmployeeTask> Tasks { get { return GetCollection<EmployeeTask>(nameof(Tasks)); } }
    }
}
