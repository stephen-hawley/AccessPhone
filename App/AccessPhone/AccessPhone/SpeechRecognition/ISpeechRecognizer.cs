using System;
namespace AccessPhone.SpeechRecognition {
	public interface ISpeechRecognizer {


		bool AutoFinish { get; }
		string Recognize ();

		void Start ();
		event EventHandler<SpeechRecognizedEvent> SpeechRecognized;
		string Stop ();
	}
}
