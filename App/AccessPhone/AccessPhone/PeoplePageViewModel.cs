using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace AccessPhone {
	public class PeoplePageViewModel : INotifyPropertyChanged {

		public PeoplePageViewModel ()
		{
		}

		string image;
		public string Image {
			get => image;
			set {
				if (image == value)
					return;
				image = value;
				OnPropertyChanged (nameof (Image));
			}
		}

		string name;
		public string Name {
			get => name;
			set {
				if (name == value)
					return;
				name = value;
				OnPropertyChanged (nameof (Name));
			}
		}


		Permission callPermission = Permission.Disallowed;
		public Permission CallPermission {
			get => callPermission;
			set {
				callPermission = value;
				ShowCall = callPermission == Permission.Allowed || callPermission == Permission.Unavailable;
			}
		}

		bool showCall;
		public bool ShowCall {
			get => showCall;
			set {
				if (showCall == value)
					return;
				showCall = value;
				OnPropertyChanged (nameof (ShowCall));
			}
		}

		Permission messagePermission = Permission.Disallowed;
		public Permission MessagePermission {
			get => messagePermission;
			set {
				messagePermission = value;
				ShowMessage = messagePermission == Permission.Allowed || messagePermission == Permission.Unavailable;
			}

		}

		bool showMessage;
		public bool ShowMessage {
			get => showMessage;
			set {
				if (showMessage == value)
					return;
				showMessage = value;
				OnPropertyChanged (nameof (ShowMessage));
			}
		}


		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged (string propertyName)
		{
			PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (propertyName));
		}
	}
}

