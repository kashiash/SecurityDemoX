using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.Security;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SecurityDemoX.Module.BusinessObjects
{
    [MapInheritance(MapInheritanceType.ParentTable)]
    [DefaultProperty(nameof(UserName))]
    public class Employee : Person, ISecurityUser, IAuthenticationStandardUser, ISecurityUserWithRoles, IPermissionPolicyUser, ICanInitialize, IObjectSpaceLink, ISecurityUserWithLoginInfo
    {
        public Employee(Session session) : base(session)
        {
        }

        [Browsable(false)]
        [Aggregated, Association("User-LoginInfo")]
        public XPCollection<ApplicationUserLoginInfo> LoginInfo
        {
            get
            {
                return GetCollection<ApplicationUserLoginInfo>(
                    nameof(LoginInfo));
            }
        }

        IEnumerable<ISecurityUserLoginInfo> IOAuthSecurityUser.UserLogins => LoginInfo.OfType<ISecurityUserLoginInfo>(
            );

        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get;
            set;
        }

        ISecurityUserLoginInfo ISecurityUserWithLoginInfo.CreateUserLoginInfo(
            string loginProviderName,
            string providerUserKey)
        {
            ApplicationUserLoginInfo result = ((IObjectSpaceLink)this).ObjectSpace
                .CreateObject<ApplicationUserLoginInfo>();
            result.LoginProviderName = loginProviderName;
            result.ProviderUserKey = providerUserKey;
            result.User = this;
            return result;
        }


        private Department department;

        #region ISecurityUser Members
        private bool isActive = true;

        public bool IsActive
        {
            get { return isActive; }
            set
            {
                SetPropertyValue(
                    nameof(IsActive),
                    ref isActive,
                    value);
            }
        }

        private string userName = String.Empty;
        [RuleRequiredField(
            "EmployeeUserNameRequired",
            DefaultContexts.Save)]
        [RuleUniqueValue(
            "EmployeeUserNameIsUnique",
            DefaultContexts.Save,
            "Użytkownik o takiej nazwie jest już w systemie.")]
        public string UserName
        {
            get { return userName; }
            set
            {
                SetPropertyValue(
                    nameof(UserName),
                    ref userName,
                    value);
            }
        }
        #endregion

        #region IAuthenticationStandardUser Members
        private bool changePasswordOnFirstLogon;

        public bool ChangePasswordOnFirstLogon
        {
            get { return changePasswordOnFirstLogon; }
            set
            {
                SetPropertyValue(
                    nameof(ChangePasswordOnFirstLogon),
                    ref changePasswordOnFirstLogon,
                    value);
            }
        }

        private string storedPassword;
        [Browsable(false), Size(SizeAttribute.Unlimited), Persistent, SecurityBrowsable]
        protected string StoredPassword
        {
            get { return storedPassword; }
            set { storedPassword = value; }
        }

        public bool ComparePassword(string password)
        {
            return PasswordCryptographer.VerifyHashedPasswordDelegate(
                this.storedPassword,
                password);
        }

        public void SetPassword(string password)
        {
            this.storedPassword = PasswordCryptographer.HashPasswordDelegate(
                password);
            OnChanged(nameof(StoredPassword));
        }
        #endregion

        #region ISecurityUserWithRoles Members
        IList<ISecurityRole> ISecurityUserWithRoles.Roles
        {
            get
            {
                IList<ISecurityRole> result = new List<ISecurityRole>(
                    );
                foreach(EmployeeRole role in EmployeeRoles)
                {
                    result.Add(role);
                }
                return result;
            }
        }
        #endregion

        #region IPermissionPolicyUser Members
        IEnumerable<IPermissionPolicyRole> IPermissionPolicyUser.Roles
        {
            get
            {
                return EmployeeRoles.OfType<IPermissionPolicyRole>(
                    );
            }
        }
        #endregion

        #region ICanInitialize Members
        void ICanInitialize.Initialize(
            IObjectSpace objectSpace,
            SecurityStrategyComplex security)
        {
            EmployeeRole newUserRole = objectSpace.FindObject<EmployeeRole>(
                new BinaryOperator(
                    "Name",
                    security.NewUserRoleName));
            if(newUserRole == null)
            {
                newUserRole = objectSpace.CreateObject<EmployeeRole>(
                    );
                newUserRole.Name = security.NewUserRoleName;
                newUserRole.IsAdministrative = true;
                newUserRole.Employees.Add(this);
            }
        }
        #endregion

        [Association("Employees-Roles")]
        [RuleRequiredField(
            "EmployeeRoleIsRequired",
            DefaultContexts.Save,
            TargetCriteria = "IsActive",
            CustomMessageTemplate = "Aktywny użytkownik musi mieć przypisana co najmniej jedną rolę")]
        public XPCollection<EmployeeRole> EmployeeRoles
        {
            get
            {
                return GetCollection<EmployeeRole>(
                    nameof(EmployeeRoles));
            }
        }

        [Association]
        [RuleRequiredField]
        public Department Department
        {
            get { return department; }
            set
            {
                SetPropertyValue(
                    nameof(Department),
                    ref department,
                    value);
            }
        }

        [Association]
        public XPCollection<EmployeeTask> Tasks
        {
            get
            {
                return GetCollection<EmployeeTask>(
                    nameof(Tasks));
            }
        }
    }
}
