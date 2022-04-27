using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using SecurityDemoX.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityDemoX.Module.Controllers
{
	public class PartyDetailViewController : ObjectViewController<DetailView, Party>
	{
		private readonly PopupWindowShowAction assignPartyRoleAction;

		public PartyDetailViewController()
		{
			assignPartyRoleAction = new PopupWindowShowAction(this, nameof(assignPartyRoleAction), PredefinedCategory.Unspecified)
			{
				Caption = "Assign party role",
			};
			assignPartyRoleAction.CustomizePopupWindowParams += AssignPartyRoleAction_CustomizePopupWindowParams;
			assignPartyRoleAction.Execute += AssignPartyRoleAction_Execute;
		}

        private void AssignPartyRoleAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
		{
			e.View = Application.CreateListView(typeof(PartyRoleType), true);
		}

		private void AssignPartyRoleAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
		{
			foreach (PartyRoleType partyRoleType in e.PopupWindowViewSelectedObjects)
			{
				var partyRole = ObjectSpace.CreateObject(Type.GetType(partyRoleType.FullName)) as PartyRole;
				ViewCurrentObject.Roles.Add(partyRole);
			}
		}
	}
}
