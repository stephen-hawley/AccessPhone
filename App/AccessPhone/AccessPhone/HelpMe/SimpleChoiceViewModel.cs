using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace AccessPhone.HelpMe {
	public class SimpleChoiceViewModel : IHelpItem, INotifyPropertyChanged {
		public SimpleChoiceViewModel ()
		{
			ResponseCommand = new Command<string> (
				(id) => PerformCommand (id));
		}

		string name;
		public string Name {
			get => name;
			set => SetProperty (ref name, value, nameof (Name));
		}

		string text;
		public string Text {
			get => text;
			set => SetProperty (ref text, value, nameof (Text));
		}


		ObservableCollection<Response> responses = new ObservableCollection<Response> ();
		public ObservableCollection<Response> Responses => responses;

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged (string propertyName)
		{
			PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (propertyName));
		}

		bool SetProperty<T> (ref T storage, T value, [CallerMemberName] string propertyName = null)
		{
			if (Object.Equals (storage, value))
				return false;

			storage = value;
			OnPropertyChanged (propertyName);
			return true;
		}

		void PerformCommand (string id)
		{
			var response = Responses.FirstOrDefault (resp => resp.UniqueID == id);
			if (response == null)
				return;
			if (SimpleChoicePage != null)
				SimpleChoicePage.PerformResponse (response);
		}

		public ICommand ResponseCommand { get; private set; }

		public SimpleChoicePage SimpleChoicePage { get; set; }
	}
}
