using System;
using System.Collections.Generic;

namespace AccessPhone.Contacts {
	public interface IContactService {
		IEnumerable<Contact> GetContacts ();
	}
}
