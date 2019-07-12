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
		PeopleActivity peopleActivity;
		public PeoplePageViewModel peopleViewModel;
		public PeoplePage (PeopleActivity people)
		{
			peopleActivity = people;
			InitializeComponent ();
			GetContacts ();
			listView.ItemTapped += OnTapped;
			peopleViewModel = new PeoplePageViewModel ();
			BigPhoto.BindingContext = peopleViewModel;
			BigLabel.BindingContext = peopleViewModel;
			Message.BindingContext = peopleViewModel;
			Call.BindingContext = peopleViewModel;
		}


		async Task GetContacts ()
		{
			var contacts = await CrossContactService.Current.GetContactListAsync();
			contacts.OrderBy (ct => ct.Name);
			listView.BindingContext = contacts;
		}


		void OnTapped (object sender, ItemTappedEventArgs args)
		{
			var contact = args.Item as Contact;
			if (contact == null) {
				peopleViewModel.Image = null;
				peopleViewModel.Name = null;
				peopleViewModel.CallPermission = Permission.Disallowed;
				peopleViewModel.MessagePermission = Permission.Disallowed;
			} else {
				peopleViewModel.Image = contact.PhotoUri ?? "person.png";
				peopleViewModel.Name = contact.Name ?? "?";
				peopleViewModel.CallPermission = Permission.Allowed;
				peopleViewModel.MessagePermission = Permission.Allowed;
			}
		}
	}
}
