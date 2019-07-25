using System;
using UIKit;
using Xamarin.Forms;
using AccessPhone.SpeechRecognition;
using Speech;

[assembly: Dependency (typeof (AccessPhone.iOS.SpeechRecognition.SpeechService))]
namespace AccessPhone.iOS.SpeechRecognition {
	public class SpeechService : ISpeechService {
		public SpeechService ()
		{
		}

		public ISpeechRecognizer CreateRecognizer ()
		{
			return new SpeechRecognizer ();
		}

		public void ReleaseRecognizer (ISpeechRecognizer recognizer)
		{
			var iosRecognizer = recognizer as SpeechRecognizer;
			if (iosRecognizer == null)
				throw new ArgumentOutOfRangeException (nameof (recognizer), "not an iOS speech reconizer");
		}

		public void RequestPemission ()
		{
			request = PermissionRequest.Pending;
			SFSpeechRecognizer.RequestAuthorization ((SFSpeechRecognizerAuthorizationStatus status) => {
				switch (status) {
				case SFSpeechRecognizerAuthorizationStatus.Authorized:
					request = PermissionRequest.Allowed;
					break;
				case SFSpeechRecognizerAuthorizationStatus.NotDetermined:
					request = PermissionRequest.Pending;
					break;
				}
			});
		}

		PermissionRequest request = PermissionRequest.Pending;

		public PermissionRequest IsPermitted {
			get { return request; }
		}

	}
}
