using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityDemoX.Module.BusinessObjects
{
	//TODO: For insurer we can only choose Organization as Party
	[DefaultClassOptions]
	public class Insurer : PartyRole
	{
		public Insurer(Session session) : base(session) { }
	}
}
