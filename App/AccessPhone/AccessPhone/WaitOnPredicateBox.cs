using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AccessPhone {
	public class WaitOnPredicateBox {


		object completeLock = new object ();
		INavigation navigation;
		TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool> ();
		Func<bool> predicate;
		bool shouldStop, stopped;
		int waitTime;
		string message;

		public WaitOnPredicateBox (INavigation navigation, Func<bool> predicate, int waitTime, string message)
		{
			this.navigation = navigation;
			this.predicate = predicate;
			this.waitTime = waitTime;
			this.message = message;
		}

		void BusyWait ()
		{
			while (true) {
				Thread.Sleep (waitTime);

				lock (completeLock) {
					if (shouldStop)
						break;
					if (predicate ()) {
						stopped = true;
						tcs.SetResult (true);
						Device.BeginInvokeOnMainThread (() => navigation.PopModalAsync());
						break;
					}
				}
			}
		}

		public Task<bool> ShowModal ()
		{
			var lblMessage = new Label { Text = message };
			var btnCancel = new Button {
				Text = "Cancel",
				WidthRequest = 100,
				BackgroundColor = Color.FromRgb (0.8, 0.8, 0.8),

			};

			btnCancel.Clicked += async (s, e) => {
				lock (completeLock) {
					if (stopped)
						return;
					shouldStop = true;
					tcs.SetResult (false);
					navigation.PopModalAsync ().Wait();
				}
			};

			var layout = new StackLayout {
				Padding = new Thickness (0, 40, 0, 0),
				VerticalOptions = LayoutOptions.StartAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Orientation = StackOrientation.Vertical,
				Children = { lblMessage, btnCancel },
			};

			// create and show page
			var page = new ContentPage ();
			page.Content = layout;

			if (predicate ()) {
				tcs.SetResult (true);
				return tcs.Task;
			}


			navigation.PushModalAsync (page);

			var checkThread = new Thread (BusyWait);
			checkThread.Start ();

			// code is waiting her, until result is passed with tcs.SetResult() in btn-Clicked
			// then proc returns the result
			return tcs.Task;
		}
	}
}
