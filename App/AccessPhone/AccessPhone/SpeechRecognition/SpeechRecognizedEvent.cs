using System;

using Xamarin.Forms;

namespace AccessPhone.SpeechRecognition {
	public class SpeechRecognizedEvent : EventArgs {
		public SpeechRecognizedEvent (string text)
		{
			Text = text;
		}
		public string Text { get; private set; }
	}
}

