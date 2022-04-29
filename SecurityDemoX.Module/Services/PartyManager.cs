using DevExpress.ExpressApp;
using DevExpress.Persistent.BaseImpl;
using SecurityDemoX.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Organization = SecurityDemoX.Module.BusinessObjects.Organization;
using Party = SecurityDemoX.Module.BusinessObjects.Party;

namespace SecurityDemoX.Module.Services
{
	public class PartyManager
	{
		//public bool Process(Party party)
		//{


		//	return true;
		//}


		//public bool Process(PartyRole partyRole)
		//{



		//	return true;
		//}


		public virtual bool Process(IParty party)
		{
			party.Party.Address1 = new BusinessObjects.Address(null);

			return true;
		}
	}


	public interface IParty
	{
		Party Party { get; }
	}


	public class Client
	{
		private readonly PartyManager partyManager;
		private readonly IObjectSpace objectSpace;

		public Client(IObjectSpace objectSpace)
		{
			partyManager = new PartyManager();
			this.objectSpace = objectSpace;
		}


		public void Execute()
		{
			var customer = objectSpace.CreateObject<Customer>();

			var organization = objectSpace.CreateObject<Organization>();

			
			partyManager.Process(customer);
			partyManager.Process(organization);
		}
	}
}
