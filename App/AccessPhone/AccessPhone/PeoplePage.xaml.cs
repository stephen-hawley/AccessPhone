using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Plugin.ContactService;
using Plugin.ContactService.Shared;
using Xamarin.Forms;
using System.Linq;


namespace AccessPhone {
	public partial class PeoplePage : ContentPage {
		ObservableCollection<Contact> contactsCollection;
		Contact currentContact = null;
		public PeoplePage ()
		{
			InitializeComponent ();
			GetContacts ();
			listView.ItemTapped += OnTapped;
		}


		async Task GetContacts ()
		{
			var contacts = await CrossContactService.Current.GetContactListAsync();
			contacts.OrderBy (ct => ct.Name);
			listView.BindingContext = contacts;
		}


		void OnTapped (object sender, ItemTappedEventArgs args)
		{
		}
	}
}
