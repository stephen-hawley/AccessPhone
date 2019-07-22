using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using AccessPhone.Contacts;
using System.IO;

namespace AccessPhone {
	public partial class PeoplePage : ContentPage {
		PeopleActivity peopleActivity;
		public PeoplePageViewModel peopleViewModel;
		public PeoplePage (PeopleActivity people)
		{
			peopleActivity = people;
			InitializeComponent ();
			listView.ItemTapped += OnTapped;
			peopleViewModel = new PeoplePageViewModel ();
			BigPhoto.BindingContext = peopleViewModel;
			BigLabel.BindingContext = peopleViewModel;
			Message.BindingContext = peopleViewModel;
			Call.BindingContext = peopleViewModel;
			Contacts = new ObservableCollection<Contact> ();
			listView.BindingContext = Contacts;
		}



		public async Task GetContactsAsync ()
		{
			var contactService = DependencyService.Get<IContactService> ();
			await Task.Run (() => {
				var contacts = contactService.GetContacts ();
				foreach (var contact in contacts) {
					Contacts.Add (contact);
				}
				Contacts.OrderBy (ct => ct.FullName);
			});
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
				peopleViewModel.Image = contact.ImagePath ?? "person.png";
				peopleViewModel.Name = contact.FullName ?? "?";
				peopleViewModel.CallPermission = Permission.UnavailableNow;
				peopleViewModel.MessagePermission = Permission.Allowed;
			}
		}

		void Message_Clicked (object sender, System.EventArgs e)
		{

		}

		void Call_Clicked (object sender, System.EventArgs e)
		{

		}

		public ObservableCollection<Contact> Contacts { get; private set; }
	}
}
