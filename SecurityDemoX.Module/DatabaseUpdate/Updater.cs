using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using SecurityDemoX.Module.BusinessObjects;
using DevExpress.Persistent.Base.General;

namespace SecurityDemoX.Module.DatabaseUpdate
{
	// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
	public class Updater : ModuleUpdater
	{
		public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
			base(objectSpace, currentDBVersion)
		{
		}
		public override void UpdateDatabaseAfterUpdateSchema()
		{
			base.UpdateDatabaseAfterUpdateSchema();
			//string name = "MyName";
			//DomainObject1 theObject = ObjectSpace.FirstOrDefault<DomainObject1>(u => u.Name == name);
			//if(theObject == null) {
			//    theObject = ObjectSpace.CreateObject<DomainObject1>();
			//    theObject.Name = name;
			//}
			Employee sampleUser = ObjectSpace.FirstOrDefault<Employee>(u => u.UserName == "User");
			if (sampleUser == null)
			{
				sampleUser = ObjectSpace.CreateObject<Employee>();
				sampleUser.UserName = "User";
				// Set a password if the standard authentication type is used
				sampleUser.SetPassword("");

				// The UserLoginInfo object requires a user object Id (Oid).
				// Commit the user object to the database before you create a UserLoginInfo object. This will correctly initialize the user key property.
				ObjectSpace.CommitChanges(); //This line persists created object(s).
				((ISecurityUserWithLoginInfo)sampleUser).CreateUserLoginInfo(SecurityDefaults.PasswordAuthentication, ObjectSpace.GetKeyValueAsString(sampleUser));
			}
			EmployeeRole defaultRole = CreateDefaultRole();
			sampleUser.EmployeeRoles.Add(defaultRole);

			Employee userAdmin = ObjectSpace.FirstOrDefault<Employee>(u => u.UserName == "Admin");
			if (userAdmin == null)
			{
				userAdmin = ObjectSpace.CreateObject<Employee>();
				userAdmin.UserName = "Admin";
				// Set a password if the standard authentication type is used
				userAdmin.SetPassword("");

				// The UserLoginInfo object requires a user object Id (Oid).
				// Commit the user object to the database before you create a UserLoginInfo object. This will correctly initialize the user key property.
				ObjectSpace.CommitChanges(); //This line persists created object(s).
				((ISecurityUserWithLoginInfo)userAdmin).CreateUserLoginInfo(SecurityDefaults.PasswordAuthentication, ObjectSpace.GetKeyValueAsString(userAdmin));
			}
			// If a role with the Administrators name doesn't exist in the database, create this role
			EmployeeRole adminRole = ObjectSpace.FirstOrDefault<EmployeeRole>(r => r.Name == "Administrators");
			if (adminRole == null)
			{
				adminRole = ObjectSpace.CreateObject<EmployeeRole>();
				adminRole.Name = "Administrators";
			}
			adminRole.IsAdministrative = true;
			userAdmin.EmployeeRoles.Add(adminRole);

			var managerRole = GetManagerRole();
			var userRole = GetUserRole();

			//Create departments.
			Department devDepartment = ObjectSpace.FindObject<Department>(CriteriaOperator.Parse("Title == 'R&D'"));
			if (devDepartment == null)
			{
				devDepartment = ObjectSpace.CreateObject<Department>();
				devDepartment.Title = "R&D";
				devDepartment.Office = "1";
				devDepartment.Save();
			}
			Department supDepartment = ObjectSpace.FindObject<Department>(CriteriaOperator.Parse("Title == 'Technical Support'"));
			if (supDepartment == null)
			{
				supDepartment = ObjectSpace.CreateObject<Department>();
				supDepartment.Title = "Technical Support";
				supDepartment.Office = "2";
				supDepartment.Save();
			}
			Department mngDepartment = ObjectSpace.FindObject<Department>(CriteriaOperator.Parse("Title == 'Management'"));
			if (mngDepartment == null)
			{
				mngDepartment = ObjectSpace.CreateObject<Department>();
				mngDepartment.Title = "Management";
				mngDepartment.Office = "3";
				mngDepartment.Save();
			}

			Employee managerJurek = CreateUser("Jurek", "Jurek", "Ogórek", devDepartment);
			managerJurek.EmployeeRoles.Add(defaultRole);
			managerJurek.EmployeeRoles.Add(managerRole);


			Employee managerJanusz = CreateUser("Janusz", "Janusz", "Internetów", supDepartment);
			managerJurek.EmployeeRoles.Add(defaultRole);
			managerJurek.EmployeeRoles.Add(managerRole);

			Employee managerJakub = CreateUser("Jakub", "Jakub", "Jarząbek", mngDepartment);
			managerJurek.EmployeeRoles.Add(defaultRole);
			managerJurek.EmployeeRoles.Add(managerRole);


			Employee userJohn = CreateUser("John", "John", "Wysocky", devDepartment);
			userJohn.EmployeeRoles.Add(defaultRole);
			userJohn.EmployeeRoles.Add(userRole);


			Employee userJesica = CreateUser("Jesica", "Dżesika", "Nowak", supDepartment);
			userJesica.EmployeeRoles.Add(defaultRole);
			userJesica.EmployeeRoles.Add(userRole);

			Employee userBrajan = CreateUser("Brajan", "Brajan", "Maximus", mngDepartment);
			userBrajan.EmployeeRoles.Add(defaultRole);
			userBrajan.EmployeeRoles.Add(userRole);

			AddTask("Read project", managerJakub);
			AddTask("Read emails", managerJanusz);
			AddTask("Order laptop for Brajan", managerJurek);


			AddTask("Prepare coffe for everyone", userJesica);
			AddTask("Prepare tea for everyone", userBrajan);
			AddTask("Clean the table", userJohn);

			AddPartyRoleTypes();

			ObjectSpace.CommitChanges(); //This line persists created object(s).
		}

		private void AddTask(string subject, Employee user)
		{
			if (ObjectSpace.FindObject<EmployeeTask>(CriteriaOperator.Parse("Subject == ?", subject)) == null)
			{
				EmployeeTask task = ObjectSpace.CreateObject<EmployeeTask>();
				task.Subject = subject;
				task.AssignedTo = user;
				task.DueDate = DateTime.Now;
				task.Status = TaskStatus.NotStarted;
				task.Description = $"This is a task for {user.FullName}";
				task.Save();
			}
		}

		private Employee CreateUser(string userName, string firstName, string lastName, Department department)
		{
			Employee managerJurek = ObjectSpace.FirstOrDefault<Employee>(u => u.UserName == userName);
			if (managerJurek == null)
			{
				managerJurek = ObjectSpace.CreateObject<Employee>();
				managerJurek.UserName = userName;
				// Set a password if the standard authentication type is used
				managerJurek.SetPassword("");
				managerJurek.FirstName = firstName;
				managerJurek.LastName = lastName;
				managerJurek.Department = department;


				// The UserLoginInfo object requires a user object Id (Oid).
				// Commit the user object to the database before you create a UserLoginInfo object. This will correctly initialize the user key property.
				ObjectSpace.CommitChanges(); //This line persists created object(s).
				((ISecurityUserWithLoginInfo)managerJurek).CreateUserLoginInfo(SecurityDefaults.PasswordAuthentication, ObjectSpace.GetKeyValueAsString(managerJurek));
			}

			return managerJurek;
		}

		public override void UpdateDatabaseBeforeUpdateSchema()
		{
			base.UpdateDatabaseBeforeUpdateSchema();
			//if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
			//    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
			//}
		}
		private EmployeeRole CreateDefaultRole()
		{
			EmployeeRole defaultRole = ObjectSpace.FirstOrDefault<EmployeeRole>(role => role.Name == "Default");
			if (defaultRole == null)
			{
				defaultRole = ObjectSpace.CreateObject<EmployeeRole>();
				defaultRole.Name = "Default";

				defaultRole.AddObjectPermissionFromLambda<Employee>(SecurityOperations.Read, cm => cm.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
				defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow);
				defaultRole.AddMemberPermissionFromLambda<Employee>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", cm => cm.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
				defaultRole.AddMemberPermissionFromLambda<Employee>(SecurityOperations.Write, "StoredPassword", cm => cm.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
				defaultRole.AddTypePermissionsRecursively<PermissionPolicyRole>(SecurityOperations.Read, SecurityPermissionState.Deny);
				defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
				defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
				defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.Create, SecurityPermissionState.Allow);
				defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.Create, SecurityPermissionState.Allow);
			}
			return defaultRole;
		}

		//Users can access and partially edit data (no create and delete capabilities) from their own department.
		private EmployeeRole GetUserRole()
		{
			EmployeeRole userRole = ObjectSpace.FindObject<EmployeeRole>(new BinaryOperator("Name", "Users"));
			if (userRole == null)
			{
				userRole = ObjectSpace.CreateObject<EmployeeRole>();
				userRole.Name = "Users";

				userRole.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow);
				userRole.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/Employee_ListView", SecurityPermissionState.Allow);
				userRole.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/EmployeeTask_ListView", SecurityPermissionState.Allow);

				userRole.AddObjectPermission<Employee>(SecurityOperations.Read, "Department.Employees[Oid = CurrentUserId()]", SecurityPermissionState.Allow);
				userRole.AddMemberPermission<Employee>(SecurityOperations.Write, "ChangePasswordOnFirstLogon;StoredPassword;FirstName;LastName", "Oid=CurrentUserId()", SecurityPermissionState.Allow);
				userRole.AddMemberPermission<Employee>(SecurityOperations.Write, "Tasks", "Department.Employees[Oid = CurrentUserId()]", SecurityPermissionState.Allow);

				userRole.SetTypePermission<PermissionPolicyRole>(SecurityOperations.Read, SecurityPermissionState.Allow);

				//   userRole.AddObjectPermission<EmployeeTask>(SecurityOperations.ReadOnlyAccess, "AssignedTo.Department.Employees[Oid = CurrentUserId()]", SecurityPermissionState.Allow);

				userRole.AddObjectPermission<EmployeeTask>(SecurityOperations.ReadWriteAccess, "AssignedTo.Oid = CurrentUserId()", SecurityPermissionState.Allow);
				//  userRole.AddMemberPermission<EmployeeTask>(SecurityOperations.Read, "AssignedTo", "AssignedTo.Oid != CurrentUserId()", SecurityPermissionState.Allow);

				userRole.AddMemberPermission<EmployeeTask>(SecurityOperations.Write, "AssignedTo", "AssignedTo.Department.Employees[Oid = CurrentUserId()]", SecurityPermissionState.Allow);


				userRole.AddObjectPermission<Department>(SecurityOperations.Read, "Employees[Oid=CurrentUserId()]", SecurityPermissionState.Allow);
			}
			return userRole;

		}
		//Managers can access and fully edit (including create and delete capabilities) data from their own department. However, they cannot access data from other departments.
		private EmployeeRole GetManagerRole()
		{
			EmployeeRole managerRole = ObjectSpace.FindObject<EmployeeRole>(new BinaryOperator("Name", "Managers"));
			if (managerRole == null)
			{
				managerRole = ObjectSpace.CreateObject<EmployeeRole>();
				managerRole.Name = "Managers";

				managerRole.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow);
				managerRole.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/Department_ListView", SecurityPermissionState.Allow);
				managerRole.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/Employee_ListView", SecurityPermissionState.Allow);
				managerRole.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/EmployeeTask_ListView", SecurityPermissionState.Allow);

				managerRole.AddObjectPermission<Department>(SecurityOperations.FullObjectAccess, "Employees[Oid=CurrentUserId()]", SecurityPermissionState.Allow);

				managerRole.SetTypePermission<Employee>(SecurityOperations.Create, SecurityPermissionState.Allow);
				managerRole.AddObjectPermission<Employee>(SecurityOperations.FullObjectAccess, "IsNull(Department) || Department.Employees[Oid=CurrentUserId()]", SecurityPermissionState.Allow);

				managerRole.SetTypePermission<EmployeeTask>(SecurityOperations.Create, SecurityPermissionState.Allow);
				managerRole.AddObjectPermission<EmployeeTask>(SecurityOperations.FullObjectAccess,
					"IsNull(AssignedTo) || IsNull(AssignedTo.Department) || AssignedTo.Department.Employees[Oid=CurrentUserId()]", SecurityPermissionState.Allow);

				managerRole.SetTypePermission<PermissionPolicyRole>(SecurityOperations.Read, SecurityPermissionState.Allow);
			}
			return managerRole;
		}


		private void AddPartyRoleTypes()
		{
			if (ObjectSpace.GetObjectsQuery<PartyRoleType>().Any()) return;
	
			var partyRoleTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes())
				.Where(type => type.IsSubclassOf(typeof(PartyRole)));

			foreach (var item in partyRoleTypes)
			{
				var partyRoleType = ObjectSpace.CreateObject<PartyRoleType>();
				partyRoleType.Name = item.Name;
				partyRoleType.FullName = item.AssemblyQualifiedName;
			}
		}
	}
}
