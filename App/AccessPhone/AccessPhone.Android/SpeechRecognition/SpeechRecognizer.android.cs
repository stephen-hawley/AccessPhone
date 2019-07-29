using System;
using System.Text;
using AccessPhone.SpeechRecognition;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Speech;

namespace AccessPhone.Droid.SpeechRecognition {
	internal class SpeechRecognizerWrapper : Java.Lang.Object, ISpeechRecognizer, IRecognitionListener {
		Intent voiceIntent;
		SpeechRecognizer speechRecognizer;
		public SpeechRecognizerWrapper ()
		{
		}


		public bool AutoFinish => false;

		public event EventHandler<SpeechRecognizedEvent> SpeechRecognized;
		protected void OnSpeechRecognized (SpeechRecognizedEvent e)
		{
			SpeechRecognized?.Invoke (this, e);
		}

		public void OnBeginningOfSpeech ()
		{
		}

		public void OnBufferReceived (byte [] buffer)
		{
		}

		public void OnEndOfSpeech ()
		{
		}

		public void OnError ([GeneratedEnum] SpeechRecognizerError error)
		{
		}

		public void OnEvent (int eventType, Bundle @params)
		{
		}

		public void OnPartialResults (Bundle partialResults)
		{
			OnResults (partialResults);
		}

		public void OnReadyForSpeech (Bundle @params)
		{
		}

		public void OnResults (Bundle results)
		{
			var contents = results.GetStringArrayList (SpeechRecognizer.ResultsRecognition);
			var sb = new StringBuilder ();
			foreach (var s in contents) {
				sb.Append (s).Append (" ");
			}
			OnSpeechRecognized (new SpeechRecognizedEvent (sb.ToString ()));
		}

		public void OnRmsChanged (float rmsdB)
		{
		}



		public string Recognize ()
		{
			throw new NotImplementedException ();
		}

		public void Start ()
		{
			speechRecognizer = SpeechRecognizer.CreateSpeechRecognizer (Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
			speechRecognizer.SetRecognitionListener (this);
			var intent = new Intent (RecognizerIntent.ActionRecognizeSpeech);
			intent.PutExtra (RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);
			intent.PutExtra (RecognizerIntent.ExtraMaxResults, 1);
			intent.PutExtra (RecognizerIntent.ExtraPartialResults, true);
			intent.PutExtra (RecognizerIntent.ExtraCallingPackage, Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.PackageName);
			intent.PutExtra (RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 1500);
			intent.PutExtra (RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 1500);
			intent.PutExtra (RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 15000);
			speechRecognizer.StartListening (intent);
		}

		public string Stop ()
		{
			speechRecognizer.StopListening ();
			return "";
		}
	}
}
