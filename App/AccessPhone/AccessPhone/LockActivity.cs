﻿using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace AccessPhone {
	public class LockActivity : ITopLevelActivity {
		public LockActivity ()
		{
		}
		public string Name => "Lock";

		public TopLevelDataModel TopLevelDataModel { get; set; }

		public Page GetPage ()
		{
			throw new NotImplementedException ();
		}

		bool allowed = true;
		public bool IsAllowed {
			get { return allowed; }
			set {
				if (allowed != value) {
					allowed = value;
					OnPropertyChanged (nameof (IsAllowed));
				}
			}
		}

		bool enabled = true;
		public bool IsEnabled {
			get { return enabled; }
			set {
				if (enabled != value) {
					enabled = value;
					OnPropertyChanged (nameof (IsEnabled));
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };

		protected virtual void OnPropertyChanged (string propertyName)
		{
			PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (propertyName));
		}
	}
}
