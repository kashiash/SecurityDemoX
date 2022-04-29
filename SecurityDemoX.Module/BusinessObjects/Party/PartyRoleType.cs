using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.ComponentModel;

namespace SecurityDemoX.Module.BusinessObjects
{
	[DefaultClassOptions]
	public class PartyRoleType : BaseObject
	{
		public PartyRoleType(Session session) : base(
			session)
		{
		}


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
