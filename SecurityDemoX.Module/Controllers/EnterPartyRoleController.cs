using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
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
		private readonly PopupWindowShowAction newPartyRoleAction;

		public EnterPartyRoleController()
		{
			newPartyRoleAction = new PopupWindowShowAction(this, nameof(newPartyRoleAction), PredefinedCategory.Unspecified)
			{
				Caption = "New",
				PaintStyle = ActionItemPaintStyle.Caption
			};
			newPartyRoleAction.CustomizePopupWindowParams += NewPartyRoleAction_CustomizePopupWindowParams;
			newPartyRoleAction.Execute += NewPartyRoleAction_Execute;
		}


		private void NewPartyRoleAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
		{
			var objectSpace = Application.CreateObjectSpace(typeof(EnterPartyRole));
			var enterPartyRole = objectSpace.CreateObject<EnterPartyRole>();
			enterPartyRole.SetPartyRoleType(ViewCurrentObject.PartyRoleType);
			e.View = Application.CreateDetailView(objectSpace, enterPartyRole);
		}

		private void NewPartyRoleAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
		{
			var enterPartyRole = e.PopupWindowViewCurrentObject as EnterPartyRole;
			CreatePersistentObjects(enterPartyRole);
		}

		private void CreatePersistentObjects(EnterPartyRole enterPartyRole)
		{
			var partyRole = enterPartyRole.PartyRole.CreatePersistentPartyRole(ObjectSpace);
			partyRole.Party = enterPartyRole.Party.CreatePersistentParty(ObjectSpace);
			ViewCurrentObject.PartyRole = partyRole;
		}
	}
}
