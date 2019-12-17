using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace AccessPhone.HelpMe {
	public class SimpleChoiceViewModel : IHelpItem, INotifyPropertyChanged {
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

		HelpItemType type;
		public HelpItemType Type {
			get => type;
			set {
				if (type == value)
					return;
				type = value;
				OnPropertyChanged (nameof (Type));
			}
		}

		string text;
		public string Text {
			get => text;
			set {
				if (text == value)
					return;
				text = value;
				OnPropertyChanged (nameof (Text));
			}
		}

		ObservableCollection<Response> responses = new ObservableCollection<Response> ();
		public ObservableCollection<Response> Responses => responses;

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged (string propertyName)
		{
			PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (propertyName));
		}
	}
}
