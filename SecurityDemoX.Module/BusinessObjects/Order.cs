using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityDemoX.Module.BusinessObjects
{
	[DefaultClassOptions]
	public class Order : BaseObject
	{
		public Order(Session session) : base(session) { }


		string notes;
		Supplier supplier;
		Customer customer;
		DateTime orderDate;


		public DateTime OrderDate
		{
			get => orderDate;
			set => SetPropertyValue(nameof(OrderDate), ref orderDate, value);
		}


		public Customer Customer
		{
			get => customer;
			set => SetPropertyValue(nameof(Customer), ref customer, value);
		}


		public Supplier Supplier
		{
			get => supplier;
			set => SetPropertyValue(nameof(Supplier), ref supplier, value);
		}

	
		[Size(SizeAttribute.Unlimited)]
		public string Notes
		{
			get => notes;
			set => SetPropertyValue(nameof(Notes), ref notes, value);
		}
	}
}
