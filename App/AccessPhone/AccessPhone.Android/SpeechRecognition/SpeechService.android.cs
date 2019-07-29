using System;
using AccessPhone.SpeechRecognition;
using Android.Speech;
using Xamarin.Forms;

[assembly: Dependency (typeof (AccessPhone.Droid.SpeechRecognition.SpeechService))]
namespace AccessPhone.Droid.SpeechRecognition {
	public class SpeechService : ISpeechService {
		SpeechRecognizer speechRecognizer;

		public SpeechService ()
		{
		}

		public PermissionRequest IsPermitted {
			get {
				var rec = global::Android.Content.PM.PackageManager.FeatureMicrophone;
				if (rec != "android.hardware.microphone")
					return PermissionRequest.Disallowed;
				if (!SpeechRecognizer.IsRecognitionAvailable (Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity))
					return PermissionRequest.Disallowed;
				return PermissionRequest.Allowed;
			}
		}

		public ISpeechRecognizer CreateRecognizer ()
		{
			return new SpeechRecognizerWrapper ();
		}

		public void ReleaseRecognizer (ISpeechRecognizer recognizer)
		{
		}

		public void RequestPemission ()
		{
		}
	}
}
