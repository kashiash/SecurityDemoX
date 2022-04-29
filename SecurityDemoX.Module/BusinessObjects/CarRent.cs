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
	//TODO: Easy way to select PartyRole from database for each Type (Customer, Driver, CarRenter).
	[DefaultClassOptions]
	public class CarRent : BaseObject
	{
		public CarRent(Session session) : base(session) { }


		Customer payer;
		Driver driver;
		CarRenter carRenter;
		string description;
		DateTime rentDate;


		public DateTime RentDate
		{
			get => rentDate;
			set => SetPropertyValue(nameof(RentDate), ref rentDate, value);
		}


		[Size(250)]
		public string Description
		{
			get => description;
			set => SetPropertyValue(nameof(Description), ref description, value);
		}


		[Association("CarRenter-CarRents")]
		public CarRenter CarRenter
		{
			get => carRenter;
			set => SetPropertyValue(nameof(CarRenter), ref carRenter, value);
		}


		public Driver Driver
		{
			get => driver;
			set => SetPropertyValue(nameof(Driver), ref driver, value);
		}

	
		public Customer Payer
		{
			get => payer;
			set => SetPropertyValue(nameof(Payer), ref payer, value);
		}
	}
}
