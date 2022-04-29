using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityDemoX.Module.BusinessObjects
{
	[DefaultClassOptions]
	public class MedicalProduct : ProductType
	{
		public MedicalProduct(Session session) : base(session) { }
	

	}
}
