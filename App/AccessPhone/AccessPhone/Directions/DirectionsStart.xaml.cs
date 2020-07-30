using System;
using System.Collections.Generic;
using AccessPhone.SpeechRecognition;
using Xamarin.Forms;

namespace AccessPhone.Directions {
	public partial class DirectionsStart : ContentPage {
		DirectionsActivity directionsActivity;
		TopLevelDataModel topLevelDataModel;
		ISpeechService speechService;
		ISpeechRecognizer recognizer;

		public DirectionsStart (DirectionsActivity directionsActivity, TopLevelDataModel topLevelDataModel)
		{
			this.directionsActivity = directionsActivity;
			this.topLevelDataModel = topLevelDataModel;
			InitializeComponent ();
			BindingContext = this;
			speechService = DependencyService.Get<ISpeechService> ();
		}

		public DirectionsViewModel ViewModel => topLevelDataModel.Directions;

		public void DestLocation_Changed (object sender, TextChangedEventArgs e)
		{
		}

		public void Picker_SelectionChanged (object sender, EventArgs args)
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

	}
}
