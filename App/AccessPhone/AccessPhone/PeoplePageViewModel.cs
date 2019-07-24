using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace AccessPhone {
	public class PeoplePageViewModel : INotifyPropertyChanged {

		const string kButtonActiveBackground = "#4177DD";
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

		string firstName;
		public string FirstName
		{
			get => firstName;
			set {
				if (firstName == value)
					return;
				firstName = value;
				OnPropertyChanged(nameof(FirstName));
			}
		}

		string lastName;
		public string LastName
		{
			get => lastName;
			set
			{
				if (lastName == value)
					return;
				lastName = value;
				OnPropertyChanged(nameof(LastName));
			}
		}


		Permission callPermission = Permission.UnavailableNow;
		public Permission CallPermission {
			get => callPermission;
			set {
				callPermission = value;
				ShowCall = callPermission == Permission.Allowed || callPermission == Permission.UnavailableNow;
				CallBackgroundColor = ColorFromPermission (callPermission);
				CanCall = callPermission == Permission.Allowed;
			}
		}

		Color callColor = Color.FromHex (kButtonActiveBackground);
		public Color CallBackgroundColor {
			get => callColor;
			set {
				if (callColor == value)
					return;
				callColor = value;
				OnPropertyChanged (nameof (CallBackgroundColor));
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

		bool canCall;
		public bool CanCall {
			get => canCall;
			set {
				if (canCall == value)
					return;
				canCall = value;
				OnPropertyChanged (nameof (CanCall));
			}
		}

		Permission messagePermission = Permission.Allowed;
		public Permission MessagePermission {
			get => messagePermission;
			set {
				messagePermission = value;
				ShowMessage = messagePermission == Permission.Allowed || messagePermission == Permission.UnavailableNow;
				MessageBackgroundColor = ColorFromPermission (messagePermission);
				CanMessage = messagePermission == Permission.Allowed;
			}

		}

		Color messageColor = Color.FromHex (kButtonActiveBackground);
		public Color MessageBackgroundColor {
			get => messageColor;
			set {
				if (messageColor == value)
					return;
				messageColor = value;
				OnPropertyChanged (nameof (MessageBackgroundColor));
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

		bool canMessage;
		public bool CanMessage {
			get => canMessage;
			set {
				if (canMessage == value)
					return;
				canMessage = value;
				OnPropertyChanged (nameof (CanMessage));
			}
		}


		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged (string propertyName)
		{
			PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (propertyName));
		}

		static Color ColorFromPermission (Permission perm)
		{
			switch (perm) {
			case Permission.UnavailableNow:
				return Color.FromHex ("#c0c0c0");
			default:
				return Color.FromHex (kButtonActiveBackground);
			}
		}
	}
}

