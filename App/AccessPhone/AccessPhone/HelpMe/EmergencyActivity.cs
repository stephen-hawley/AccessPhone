﻿using System;
using System.ComponentModel;
using AccessPhone.HelpMe;
using Xamarin.Forms;

namespace AccessPhone.HelpMe {
	public class EmergencyActivity : ITopLevelActivity {
		public EmergencyActivity ()
		{
		}

		public string Name => "Emergency";

		public TopLevelDataModel TopLevelDataModel { get; set; }

		public Page GetPage ()
		{
			if (TopLevelDataModel.HelpMeDatabase.Start != null) {
				return new SimpleChoicePage (TopLevelDataModel, TopLevelDataModel.HelpMeDatabase.Start);
			}
			return null; // FIXME - this is a fatal error
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
