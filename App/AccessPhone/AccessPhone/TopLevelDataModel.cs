using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using AccessPhone.Contacts;
using AccessPhone.SpeechRecognition;
using AccessPhone.ToDo;
using Xamarin.Forms;

namespace AccessPhone {
	public class TopLevelDataModel : INotifyPropertyChanged {

		private TopLevelDataModel ()
		{
			Contacts = new ObservableCollection<Contact> ();
			ToDo = new ObservableCollection<IToDoItem> ();
			ToDone = new ObservableCollection<IToDoItem> ();
		}


		public static async Task<TopLevelDataModel> Load ()
		{
			var model = new TopLevelDataModel ();
			await model.LoadModel ();
			return model;
		}

		async Task LoadModel ()
		{
			await GetContactsAsync ();
		}


		string userFirstName = "Alice";
		public string UserFirstName {
			get => userFirstName;
			set {
				if (value == userFirstName)
					return;
				userFirstName = value;
				OnPropertyChanged (nameof (UserFirstName));
			}
		}

		string userLastName = "Hawley";
		public string UserLastName {
			get => userLastName;
			set {
				if (value == userLastName)
					return;
				userLastName = value;
				OnPropertyChanged (nameof (UserLastName));
			}
		}

		string userFullName = "Alice Hawley";
		public string UserFullName {
			get => userFullName;
			set {
				if (value == userFullName)
					return;
				userFullName = value;
				OnPropertyChanged (nameof (UserFullName));
			}
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

		public ObservableCollection<Contact> Contacts { get; private set; }

		public ObservableCollection<IToDoItem> ToDo { get; private set; }
		public ObservableCollection<IToDoItem> ToDone { get; private set; }

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged (string propertyName)
		{
			PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (propertyName));
		}

	}
}
