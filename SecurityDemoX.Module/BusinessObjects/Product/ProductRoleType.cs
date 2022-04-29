using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityDemoX.Module.BusinessObjects
{
	[XafDefaultProperty(nameof(Name))]
	[DefaultClassOptions]
	public class ProductRoleType : XPObject
	{
		public ProductRoleType(Session session) : base(session) { }


		string fullName;
		string description;
		string name;

		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		[ModelDefault(nameof(IModelCommonMemberViewItem.AllowEdit),
			"false")]
		public string Name
		{
			get { return name; }
			set
			{
				SetPropertyValue(
					nameof(Name),
					ref name,
					value);
			}
		}


		[Size(500)]
		[Browsable(false)]
		public string FullName
		{
			get => fullName;
			set => SetPropertyValue(nameof(FullName), ref fullName, value);
		}


		[Size(SizeAttribute.Unlimited)]
		public string Description
		{
			get { return description; }
			set
			{
				SetPropertyValue(
					nameof(Description),
					ref description,
					value);
			}
		}
	}
}
