using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace AccessPhone.Directions {
	public class DirectionsViewModel : INotifyPropertyChanged {
		ObservableCollection<Destination> recentDestinations;

		public DirectionsViewModel ()
		{
			selectedRecent = -1;
			recentDestinations = new ObservableCollection<Destination> ();
			recentDestinations.Add (new Destination () { Name = "Work", Address = "367 Russel St, Hadley, MA" });
			recentDestinations.Add (new Destination () { Name = "Home", Address = "92 Moser St, Northampton, MA" });
			ArrivalTime = HalfAnHourFromNow ();
		}

		TimeSpan arrivalTime;
		public TimeSpan ArrivalTime {
			get => arrivalTime;
			set {
				if (arrivalTime == value)
					return;
				arrivalTime = value;
				OnPropertyChanged (nameof (ArrivalTime));
			}
		}

		bool isAnyTime;
		public bool IsAnyTime {
			get => isAnyTime;
			set {
				if (isAnyTime == value)
					return;
				isAnyTime = value;
				OnPropertyChanged (nameof (IsAnyTime));
			}
		}

		public ObservableCollection<Destination> RecentDestinations {
			get => recentDestinations;
			set {
				if (recentDestinations == value)
					return;
				recentDestinations = value;
				OnPropertyChanged (nameof (RecentDestinations));
			}

		}

		int selectedRecent;
		public int SelectedRecent {
			get => selectedRecent;
			set {
				if (selectedRecent == value)
					return;
				selectedRecent = value;
				OnPropertyChanged (nameof (SelectedRecent));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged (string propertyName)
		{
			PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (propertyName));
		}


		public static TimeSpan HalfAnHourFromNow ()
		{
			var now = DateTime.Now;
			return new TimeSpan (now.Hour, now.Minute, now.Second) + new TimeSpan (0, 30, 0);
		}
	}
}
