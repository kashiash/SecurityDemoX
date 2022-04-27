using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Base;
using SecurityDemoX.Module.BusinessObjects;
using SecurityDemoX.Module.BusinessObjects.NonPersistent;
using SecurityDemoX.Module.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityDemoX.Module.Controllers
{
	public class EnterPartyRoleController : ObjectViewController<DetailView, IEnterPartyRole>
	{
		private readonly SingleChoiceAction newPartyRoleAction;

		public EnterPartyRoleController()
		{
			newPartyRoleAction = new SingleChoiceAction(this, nameof(newPartyRoleAction), "EnterPartyRoleNewAction")
			{
				Caption = "New",
			};
			newPartyRoleAction.Execute += NewPartyRoleAction_Execute;
		}

		protected override void OnActivated()
		{
			base.OnActivated();
			FillNewPartyRoleActionItems();
		}

		private void FillNewPartyRoleActionItems()
		{
			var partyTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes())
				.Where(type => type.IsSubclassOf(typeof(Party)));

			newPartyRoleAction.Items.Clear();
			foreach (var partyType in partyTypes)
			{
				newPartyRoleAction.Items.Add(new ChoiceActionItem($"{partyType.Name}", partyType));
			}
		}

		private void NewPartyRoleAction_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
		{
			var objectSpace = Application.CreateObjectSpace();
			var enterPartyRole = objectSpace.CreateObject<EnterPartyRole>();
			enterPartyRole.SetPartyRoleType(objectSpace, ViewCurrentObject.PartyRoleType, e.SelectedChoiceActionItem.Data as Type);
			var dc = new DialogController();
			dc.AcceptAction.Execute += AcceptAction_Execute;
			e.ShowViewParameters.CreatedView = Application.CreateDetailView(objectSpace, enterPartyRole);
			e.ShowViewParameters.Controllers.Add(dc);
		}

		private void AcceptAction_Execute(object sender, SimpleActionExecuteEventArgs e)
		{
			var enterPartyRole = e.CurrentObject as EnterPartyRole;
			ViewCurrentObject.PartyRole = ObjectSpace.GetObject(enterPartyRole.PartyRole);
			View.Refresh(true);
		}
	}
}
