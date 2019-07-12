using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace AccessPhone {
	public class TalkActivity : ITopLevelActivity {
		bool allowed = true;
		bool enabled = true;

		public TalkActivity ()
		{
		}

		public string Name => "Talk";

		public bool IsAllowed {
			get { return allowed; }
			set {
				if (allowed != value) {
					allowed = value;
					OnPropertyChanged (nameof(IsAllowed));
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

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged (string propertyName)
		{
			PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (propertyName));
		}

		public Page GetPage ()
		{
			return null;
		}
	}
}
