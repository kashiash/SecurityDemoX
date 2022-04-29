using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityDemoX.Module.BusinessObjects
{
	public class ProductType : BaseObject
	{
		public ProductType(Session session) : base(session) { }


		ProductRoleType productRoleType;
		string name;
		Product product;


		[Association("Product-ProductTypes")]
		public Product Product
		{
			get => product;
			set => SetPropertyValue(nameof(Product), ref product, value);
		}


		public string Name
		{
			get => name;
			set => SetPropertyValue(nameof(Name), ref name, value);
		}

	
		public ProductRoleType ProductRoleType
		{
			get => productRoleType;
			set => SetPropertyValue(nameof(ProductRoleType), ref productRoleType, value);
		}


		public override void AfterConstruction()
		{
			base.AfterConstruction();
			productRoleType = Session.Query<ProductRoleType>().Where(x => x.Name == GetType().Name).FirstOrDefault();
		}
	}
}
