using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AccessPhone.SpeechRecognition;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace AccessPhone {
	public partial class TalkSpeechToText : ContentPage {
		ISpeechService speechService;
		ISpeechRecognizer recognizer;
		bool checkedForPermission;
		bool readPermission;

		public TalkSpeechToText ()
		{
			InitializeComponent ();

		}

		protected override async void OnAppearing ()
		{
			base.OnAppearing ();
			if (!checkedForPermission) {
				checkedForPermission = true;
				var waitBox = new WaitOnPredicateBox (Navigation, () => speechService.IsPermitted != PermissionRequest.Pending, 100, "Waiting on speech...");
				speechService.RequestPemission ();

				readPermission = await waitBox.ShowModal ();
			}

			if (readPermission && speechService.IsPermitted == PermissionRequest.Allowed) {
				Record.IsEnabled = true;
			}

		}

		void Record_Pressed (object sender, EventArgs e)
		{
			if (recognizer == null) {
				recognizer = speechService.CreateRecognizer ();
				recognizer.SpeechRecognized += SpeechReceived;
				recognizer.Start();
			}
		}

		void Record_Released (object sender, System.EventArgs e)
		{
			if (recognizer != null) {
				recognizer.Stop ();
				recognizer.SpeechRecognized -= SpeechReceived;
				recognizer = null;
				AddText (RecordedText.Text);
				RecordedText.Text = "";
			}
		}

		void RecordingThread ()
		{
			this.RecordedText.Text = "Starting";
			recognizer.Start ();
		}


		void SpeechReceived (object sender, SpeechRecognizedEvent e)
		{
			RecordedText.Text = e.Text;
		}

		void Clear_Clicked (object sender, EventArgs e)
		{
			FullText.Text = "";
		}

		void AddText (string textToAdd)
		{
			var text = new StringBuilder (FullText.Text);
			if (text.Length > 0 && !Char.IsWhiteSpace (text [text.Length - 1]))
				text.Append (' ');
			text.Append (textToAdd);
			FullText.Text = text.ToString ();
		}

		async void Read_Clicked (object sender, EventArgs e)
		{
			if (FullText.Text.Length <= 0)
				return;
			await TextToSpeech.SpeakAsync (FullText.Text);
		}

	}
}
