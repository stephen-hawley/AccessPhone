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
		SFSpeechAudioBufferRecognitionRequest liveRequest = new SFSpeechAudioBufferRecognitionRequest ();
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


			liveRequest = new SFSpeechAudioBufferRecognitionRequest {
				ShouldReportPartialResults = true
			};

			var node = audioEngine.InputNode;
			var recordingFormat = node.GetBusOutputFormat (0);
			node.InstallTapOnBus (0, 1024, recordingFormat, (AVAudioPcmBuffer buffer, AVAudioTime when) => {
				// Append buffer to recognition request
				liveRequest.Append (buffer);
			});

			recognitionTask = speechRecognizer.GetRecognitionTask (liveRequest, (SFSpeechRecognitionResult result, NSError error) => {

				var isFinal = false;
				if (result != null) {
					recognizedText = result.BestTranscription.FormattedString;
					OnSpeechRecognized (new SpeechRecognizedEvent (recognizedText));
					isFinal = result.Final;
				}

				if (error != null || isFinal) {
					audioSession.SetCategory (AVAudioSessionCategory.Playback);
					audioSession.SetMode (AVAudioSession.ModeDefault, out err);
					node.RemoveTapOnBus (0);
					audioEngine.Dispose ();
					liveRequest.Dispose ();
					recognitionTask.Dispose ();
					liveRequest = null;
					recognitionTask = null;
				}
			});

			audioEngine.Prepare ();
			audioEngine.StartAndReturnError (out err);
		}

		public string Stop ()
		{
			audioEngine.Stop ();
			recognitionTask.Finish ();
			liveRequest.EndAudio ();
			return recognizedText;
		}

		public event EventHandler<SpeechRecognizedEvent> SpeechRecognized;
		protected void OnSpeechRecognized (SpeechRecognizedEvent e)
		{
			SpeechRecognized?.Invoke (this, e);
		}
	}
}
