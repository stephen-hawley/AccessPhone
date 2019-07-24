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
		TopLevelDataModel topLevelDataModel;
		public PeoplePage (PeopleActivity people, TopLevelDataModel topLevelDataModel)
		{
			peopleActivity = people;
			InitializeComponent ();
			listView.ItemTapped += OnTapped;
			this.topLevelDataModel = topLevelDataModel;

			peopleViewModel = new PeoplePageViewModel ();
			BigPhoto.BindingContext = peopleViewModel;
			BigLabel.BindingContext = peopleViewModel;
			Message.BindingContext = peopleViewModel;
			Call.BindingContext = peopleViewModel;
			Contacts = new ObservableCollection<Contact> ();

			listView.BindingContext = topLevelDataModel.Contacts;
		}


		void OnTapped (object sender, ItemTappedEventArgs args)
		{
			var contact = args.Item as Contact;
			if (contact == null) {
				peopleViewModel.Image = null;
				peopleViewModel.Name = null;
				peopleViewModel.FirstName = null;
				peopleViewModel.LastName = null;
				peopleViewModel.CallPermission = Permission.Disallowed;
				peopleViewModel.MessagePermission = Permission.Disallowed;
			} else {
				peopleViewModel.Image = contact.ImagePath ?? "personblue.png";
				peopleViewModel.Name = contact.FullName ?? "?";
				peopleViewModel.FirstName = contact.FirstName ?? contact.LastName ?? contact.FullName ?? "?";
				peopleViewModel.LastName = contact.LastName ?? contact.FullName ?? "?";
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
