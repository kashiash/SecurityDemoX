﻿using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SecurityDemoX.Module.BusinessObjects
{
    [ImageName("BO_Role")]
    public class EmployeeRole : PermissionPolicyRoleBase, IPermissionPolicyRoleWithUsers
    {
        public EmployeeRole(Session session) : base(session)
        {
        }
        [Association("Employees-Roles")]
        public XPCollection<Employee> Employees
        {
            get
            {
                return GetCollection<Employee>(
                    nameof(Employees));
            }
        }

        IEnumerable<IPermissionPolicyUser> IPermissionPolicyRoleWithUsers.Users
        {
            get
            {
                return Employees.OfType<IPermissionPolicyUser>(
                    );
            }
        }
    }
}
