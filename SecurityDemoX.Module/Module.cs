using System;
using System.Text;
using System.Linq;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.DomainLogics;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;

namespace SecurityDemoX.Module
{
	public sealed partial class SecurityDemoXModule : ModuleBase
	{
		public SecurityDemoXModule()
		{
			InitializeComponent();
		}

		public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB)
		{
			ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);
			return new ModuleUpdater[] { updater };
		}

		public override void Setup(XafApplication application)
		{
			base.Setup(application);
			application.ObjectSpaceCreated += Application_ObjectSpaceCreated;
		}

		private void Application_ObjectSpaceCreated(object sender, ObjectSpaceCreatedEventArgs e)
		{
			if (e.ObjectSpace is CompositeObjectSpace compositeObjectSpace)
			{
				if (compositeObjectSpace.Owner is not CompositeObjectSpace)
				{
					compositeObjectSpace.PopulateAdditionalObjectSpaces((XafApplication)sender);
				}
			}
		}

		public override void CustomizeTypesInfo(ITypesInfo typesInfo)
		{
			base.CustomizeTypesInfo(typesInfo);
			CalculatedPersistentAliasHelper.CustomizeTypesInfo(typesInfo);
		}
	}
}