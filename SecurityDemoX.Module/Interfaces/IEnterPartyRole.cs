using SecurityDemoX.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityDemoX.Module.Interfaces
{
	public interface IEnterPartyRole
	{
		public PartyRole PartyRole { get; set; }
		public Type PartyRoleType { get; }
	}
}
