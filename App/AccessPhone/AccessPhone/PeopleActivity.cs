using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using AccessPhone.Contacts;
using Xamarin.Forms;

namespace AccessPhone {
	public class PeopleActivity : ITopLevelActivity {
		bool allowed = true;
		bool enabled = true;

		public PeopleActivity ()
		{
		}

		public string Name => "People";

		public bool IsAllowed {
			get { return allowed; }
			set {
				if (allowed != value) {
					allowed = value;
					OnPropertyChanged (nameof (IsAllowed));
				}
			}
		}

		public bool IsEnabled {
			get { return enabled; }
			set {
				if (enabled != value) {
					enabled = value;
					OnPropertyChanged (nameof (IsEnabled));
				}
			}
		}

		public TopLevelDataModel TopLevelDataModel { get; set; }

		public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };

		protected virtual void OnPropertyChanged (string propertyName)
		{
			PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (propertyName));
		}

		public Page GetPage ()
		{
			var page = new PeoplePage (this, TopLevelDataModel);
			return page;
		}
	}
}
