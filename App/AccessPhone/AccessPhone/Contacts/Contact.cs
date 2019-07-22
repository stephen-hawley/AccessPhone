using System;
using System.Collections.Generic;

namespace AccessPhone.Contacts {
	public class Contact {
		public Contact ()
		{
			Numbers = new List<PhoneNumber> ();
			Emails = new List<EmailAddress> ();
		}
		public string FullName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public List<PhoneNumber> Numbers { get; }
		public List<EmailAddress> Emails { get; }
		public string ImagePath { get; set; }
		public string ThumbnailPath { get; set; }
	}
}
