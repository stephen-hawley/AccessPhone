using System;
namespace AccessPhone.SpeechRecognition {
	public interface ISpeechService {
		void RequestPemission ();
		PermissionRequest IsPermitted { get; }
		ISpeechRecognizer CreateRecognizer ();
		void ReleaseRecognizer (ISpeechRecognizer recognizer);
	}
}
