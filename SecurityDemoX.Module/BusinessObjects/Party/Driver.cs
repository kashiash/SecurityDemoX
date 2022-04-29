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
	//TODO: For driver we can only choose Person as Party
	[DefaultClassOptions]
	public class Driver : PartyRole
	{
		public Driver(Session session) : base(session) { }



	}
}
