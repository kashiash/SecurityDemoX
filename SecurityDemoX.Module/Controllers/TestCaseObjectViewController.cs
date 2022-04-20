using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Validation;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using SecurityDemoX.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityDemoX.Module.Controllers
{
    public class TestCaseViewController : ObjectViewController<DetailView, TestCase>
    {
        public TestCaseViewController() : base()
        {
            // Target required Views (use the TargetXXX properties) and create their Actions.
            
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }

        protected override void OnFrameAssigned()
        {
            base.OnFrameAssigned();
            PersistenceValidationController controller = Frame.GetController<PersistenceValidationController>();
            if (controller != null)
            {
                controller.CustomGetAggregatedObjectsToValidate += delegate (object sender, CustomGetAggregatedObjectsToValidateEventArgs args)
                {
                    if (args.OwnerObject is TestCase)
                    {
                        args.AggregatedObjects.Clear();
                        args.Handled = true;
                    }
                };
            }
        }
    }

}
