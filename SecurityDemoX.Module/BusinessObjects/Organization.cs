using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityDemoX.Module.BusinessObjects
{
	[DefaultClassOptions]
	public class Organization : Party
	{
		string nipNumber;
		string fullName = "";
		string profile = "";
		string email = "";
		string webSite = "";
		string description = "";
		string name = "";

		public Organization(Session session) : base(session) { }

		public string FullName
		{
			get { return fullName; }
			set { SetPropertyValue(nameof(FullName), ref fullName, value); }
		}

		public string Profile
		{
			get { return profile; }
			set { SetPropertyValue(nameof(Profile), ref profile, value); }
		}

		public string Email
		{
			get { return email; }
			set { SetPropertyValue(nameof(Email), ref email, value); }
		}

		public string WebSite
		{
			get { return webSite; }
			set { SetPropertyValue(nameof(WebSite), ref webSite, value); }
		}

		[Size(4096), ObjectValidatorIgnoreIssue(typeof(ObjectValidatorLargeNonDelayedMember))]
		public string Description
		{
			get { return description; }
			set { SetPropertyValue(nameof(Description), ref description, value); }
		}

		public string Name
		{
			get { return name; }
			set { SetPropertyValue(nameof(Name), ref name, value); }
		}

		[ObjectValidatorIgnoreIssue(typeof(ObjectValidatorDefaultPropertyIsVirtual))]
#pragma warning disable XAF0002 // XPO business class properties should not be overriden
		public override string DisplayName
#pragma warning restore XAF0002 // XPO business class properties should not be overriden
		{
			get { return Name; }
		}

		
		[Size(10)]
		[RuleUniqueValue]
		public string NipNumber
		{
			get => nipNumber;
			set => SetPropertyValue(nameof(NipNumber), ref nipNumber, value);
		}


		public override Party CreatePersistentParty(IObjectSpace objectSpace)
		{
			throw new NotImplementedException();
		}
	}
}