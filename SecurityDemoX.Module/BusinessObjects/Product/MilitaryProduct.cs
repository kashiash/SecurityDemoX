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
	public class MilitaryProduct : ProductType
	{
		public MilitaryProduct(Session session) : base(session) { }

		

	}
}
