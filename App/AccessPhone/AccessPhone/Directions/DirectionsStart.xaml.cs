using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AccessPhone.SpeechRecognition;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AccessPhone.Directions {
	public partial class DirectionsStart : ContentPage {
		DirectionsActivity directionsActivity;
		TopLevelDataModel topLevelDataModel;
		ISpeechService speechService;
		ISpeechRecognizer recognizer;
		Location location;
		bool locationIsValid;
		HttpClient client;

		public DirectionsStart (DirectionsActivity directionsActivity, TopLevelDataModel topLevelDataModel)
		{
			this.directionsActivity = directionsActivity;
			this.topLevelDataModel = topLevelDataModel;
			InitializeComponent ();
			BindingContext = this;
			speechService = DependencyService.Get<ISpeechService> ();
			client = ((AccessPhone.App)App.Current).HttpClient;
		}

		protected async override void OnAppearing ()
		{
			await Task.Run (() => GetLocation ());
		}

		public DirectionsViewModel ViewModel => topLevelDataModel.Directions;

		public async void DestLocation_Changed (object sender, TextChangedEventArgs e)
		{
			if (!locationIsValid)
				return;
			if (e.NewTextValue.Length < 4) {
				PossibleChoices.IsVisible = false;
				TheRestOfTheControls.IsVisible = true;
				return;
			} else {
				TheRestOfTheControls.IsVisible = false;
				PossibleChoices.IsVisible = true;
				var searcher = new LocationAPISimple (APIKeys.GoogleMapAPIKey.kAPIKey, client, 2500);
				var source = new CancellationTokenSource ();
				try {
					var matches = await searcher.FindNearestAsync (e.NewTextValue, location, source.Token);
					PossibleChoices.ItemsSource = matches;
				} catch (OperationCanceledException) {
					source.Dispose ();
					Console.WriteLine ("Canceled");
				}
			}
		} 

		public async void Picker_SelectionChanged (object sender, EventArgs args)
		{
			if (topLevelDataModel.Directions.SelectedRecent < 0)
				return;
			var oldDest = topLevelDataModel.Directions.RecentDestinations [topLevelDataModel.Directions.SelectedRecent];
			DestText.Text = oldDest.Address;
		}

		void Record_Pressed (object sender, EventArgs e)
		{
			if (recognizer == null) {
				recognizer = speechService.CreateRecognizer ();
				recognizer.SpeechRecognized += SpeechReceived;
				recognizer.Start ();
			}
		}

		void Record_Released (object sender, System.EventArgs e)
		{
			if (recognizer != null) {
				recognizer.Stop ();
				recognizer.SpeechRecognized -= SpeechReceived;
				recognizer = null;
			}
		}

		void RecordingThread ()
		{
			recognizer.Start ();
		}


		void SpeechReceived (object sender, SpeechRecognizedEvent e)
		{
			DestText.Text = e.Text;
		}


		async Task GetLocation ()
		{
			try {
				var loc = await Geolocation.GetLastKnownLocationAsync ();
				if (loc != null) {
					location = loc;
					locationIsValid = true;
				}
			} catch {

			}
		}

		async Task GetNearestPlace (string searchString)
		{

		}
	}
}
