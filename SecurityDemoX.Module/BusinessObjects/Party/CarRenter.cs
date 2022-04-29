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
	public class CarRenter : PartyRole
	{
		public CarRenter(Session session) : base(session) { }


		[Association("CarRenter-CarRents")]
		public XPCollection<CarRent> CarRents
		{
			get
			{
				return GetCollection<CarRent>(nameof(CarRents));
			}
		}
	}
}
