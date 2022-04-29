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
	public class ProductDetailViewController : ObjectViewController<DetailView, Product>
	{
		private readonly PopupWindowShowAction assignProductRoleAction;

		public ProductDetailViewController()
		{
			assignProductRoleAction = new PopupWindowShowAction(this, nameof(assignProductRoleAction), PredefinedCategory.Unspecified)
			{
				Caption = "Assign product role",
			};
			assignProductRoleAction.CustomizePopupWindowParams += AssignProductRoleAction_CustomizePopupWindowParams;
			assignProductRoleAction.Execute += AssignProductRoleAction_Execute;
		}

		private void AssignProductRoleAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
		{
			e.View = Application.CreateListView(typeof(ProductRoleType), true);
		}

		private void AssignProductRoleAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
		{
			foreach (ProductRoleType partyRoleType in e.PopupWindowViewSelectedObjects)
			{
				var productType = ObjectSpace.CreateObject(Type.GetType(partyRoleType.FullName)) as ProductType;
				ViewCurrentObject.ProductTypes.Add(productType);
			}
		}
	}
}
