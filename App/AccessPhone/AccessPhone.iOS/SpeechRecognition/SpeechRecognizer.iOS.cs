using System;
using System.Threading.Tasks;
using AccessPhone.SpeechRecognition;
using AVFoundation;
using Foundation;
using Speech;
using System.Text;

namespace AccessPhone.iOS.SpeechRecognition {
	internal class SpeechRecognizer : ISpeechRecognizer {
		AVAudioEngine audioEngine = new AVAudioEngine ();
		SFSpeechRecognizer speechRecognizer = new SFSpeechRecognizer ();
		SFSpeechAudioBufferRecognitionRequest recognitionRequest = new SFSpeechAudioBufferRecognitionRequest ();
		SFSpeechRecognitionTask recognitionTask;

		string recognizedText = "";
		
		public SpeechRecognizer ()
		{
		}

		public bool AutoFinish => false;

		public string Recognize ()
		{
			throw new NotImplementedException ();
		}

		public void Start ()
		{
			recognitionTask?.Cancel ();
			recognitionTask = null;
			recognizedText = "";

			var audioSession = AVAudioSession.SharedInstance ();
			NSError err;
			err = audioSession.SetCategory (AVAudioSessionCategory.Record);
			audioSession.SetMode (AVAudioSession.ModeMeasurement, out err);
			err = audioSession.SetActive (true, AVAudioSessionSetActiveOptions.NotifyOthersOnDeactivation);

			// Configure request so that results are returned before audio recording is finished
			recognitionRequest = new SFSpeechAudioBufferRecognitionRequest {
				ShouldReportPartialResults = true
			};

			var inputNode = audioEngine.InputNode;
			if (inputNode == null)
				throw new InvalidProgramException ("Audio engine has no input node");

			// A recognition task represents a speech recognition session.
			// We keep a reference to the task so that it can be cancelled.
			recognitionTask = speechRecognizer.GetRecognitionTask (recognitionRequest, (result, error) => {
				var isFinal = false;
				if (result != null) {
					recognizedText = result.BestTranscription.FormattedString;
					OnSpeechRecognized (new SpeechRecognizedEvent(recognizedText));
					isFinal = result.Final;
				}

				if (error != null || isFinal) {
					audioEngine.Stop ();
					inputNode.RemoveTapOnBus (0);
					recognitionRequest = null;
					recognitionTask = null;
				}
			});

			var recordingFormat = inputNode.GetBusOutputFormat (0);
			inputNode.InstallTapOnBus (0, 1024, recordingFormat, (buffer, when) => {
				recognitionRequest?.Append (buffer);
			});

			audioEngine.Prepare ();
			audioEngine.StartAndReturnError (out err);
		}

		public string Stop ()
		{
			audioEngine.Stop ();
			return recognizedText;
		}

		public event EventHandler<SpeechRecognizedEvent> SpeechRecognized;
		protected void OnSpeechRecognized (SpeechRecognizedEvent e)
		{
			SpeechRecognized?.Invoke (this, e);
		}
	}
}
