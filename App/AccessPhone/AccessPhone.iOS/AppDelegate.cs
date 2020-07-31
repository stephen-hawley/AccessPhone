using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using ImageCircle.Forms.Plugin.iOS;
using Google.Maps;

namespace AccessPhone.iOS {
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate {
		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		// 42.312952,-72.650308

		// never commit with the key here.
		static string kMapsApiKey = "#insert-google-api-key#";
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			MapServices.ProvideAPIKey (kMapsApiKey);
			global::Xamarin.Forms.Forms.Init ();
			ImageCircleRenderer.Init ();
			LoadApplication (new App ());

			return base.FinishedLaunching (app, options);
		}
	}
}
