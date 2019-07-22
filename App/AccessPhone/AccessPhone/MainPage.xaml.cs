using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AccessPhone.Contacts;
using Xamarin.Forms;

namespace AccessPhone {
	// Learn more about making custom code visible in the Xamarin.Forms previewer
	// by visiting https://aka.ms/xamarinforms-previewer
	[DesignTimeVisible (true)]
	public partial class MainPage : ContentPage {
		public List<ITopLevelActivity> Activities;
		public MainPage ()
		{
			Activities = GatherActivities ();
			InitializeComponent ();
			NavigationPage.SetHasNavigationBar (this, false);

			foreach (var activity in Activities) {
				var matchingFrame = Flex.FindByName<Frame> (activity.Name + "Frame");
				if (matchingFrame != null) {
					var binding = new Binding ("IsAllowed");
					binding.Source = activity;
					matchingFrame.SetBinding (Frame.IsVisibleProperty, binding);
					var subButton = matchingFrame.FindByName<ImageButton> (activity.Name);
					if (subButton != null) {
						binding = new Binding ("IsEnabled");
						binding.Source = activity;
						subButton.SetBinding (ImageButton.IsEnabledProperty, binding);
					}
				}
			}

			Identifier.Text = Xamarin.Essentials.DeviceInfo.Name;
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

		void ButtonClicked (string activityName)
		{
			var activity = FindActivityByName (activityName);
			if (activity == null)
				return;
			var page = activity.GetPage ();
			if (page != null) {
				Navigation.PushAsync (page);
			}
		}

		void Talk_Clicked (object sender, System.EventArgs e)
		{
			ButtonClicked (nameof(Talk));
		}
		void Pay_Clicked (object sender, System.EventArgs e)
		{
			ButtonClicked (nameof(Pay));
		}
		void People_Clicked (object sender, System.EventArgs e)
		{
			ButtonClicked (nameof(People));
		}
		void Lock_Clicked (object sender, System.EventArgs e)
		{
			ButtonClicked (nameof(Lock));
		}
		void ToDo_Clicked (object sender, System.EventArgs e)
		{
			ButtonClicked (nameof(ToDo));
		}
		void Dates_Clicked (object sender, System.EventArgs e)
		{
			ButtonClicked (nameof(Dates));
		}
		void Emergency_Clicked (object sender, System.EventArgs e)
		{
			ButtonClicked (nameof(Emergency));
		}

		ITopLevelActivity FindActivityByName (string name)
		{
			return Activities.FirstOrDefault (act => act.Name == name);
		}
	}
}
