using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AccessPhone {
	// Learn more about making custom code visible in the Xamarin.Forms previewer
	// by visiting https://aka.ms/xamarinforms-previewer
	[DesignTimeVisible (true)]
	public partial class MainPage : ContentPage {
		List<ITopLevelActivity> activities;
		public MainPage ()
		{
			InitializeComponent ();
			activities = GatherActivities ();
			AddButtons (activities);
		}

		List<ITopLevelActivity> GatherActivities ()
		{
			var assembly = Assembly.GetExecutingAssembly ();
			var activityTypes = assembly.GetTypes ().Where (t => t.GetInterfaces ().Contains (typeof (ITopLevelActivity)));
			var activities = new List<ITopLevelActivity> ();
			foreach (var act in activityTypes) {
				var ctor = act.GetConstructor (new Type [0]);
				if (ctor == null)
					continue;
				try {
					var instance = ctor.Invoke (null) as ITopLevelActivity;
					if (instance != null)
						activities.Add (instance);
				} catch { }
			}
			return activities;
		}

		void AddButtons (IEnumerable<ITopLevelActivity> activities)
		{
			var buttons = activities.Select (a => a.Button);

			int i = 0;
			foreach (Button b in buttons) {
				if ((i & 1) == 0) {
					Grid.RowDefinitions.Add (new RowDefinition { Height = new GridLength (1, GridUnitType.Star) });
				}
				Grid.Children.Add (b, (i & 1), i / 2);
				i++;
			}

		}
	}
}
