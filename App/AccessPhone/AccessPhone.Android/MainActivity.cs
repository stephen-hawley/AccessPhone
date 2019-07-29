using System;
using Android;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.App;
using ImageCircle.Forms.Plugin.Droid;
using Android.Content;

namespace AccessPhone.Droid {
	[Activity (Label = "AccessPhone", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity {
		protected override void OnCreate (Bundle savedInstanceState)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate (savedInstanceState);

			Xamarin.Essentials.Platform.Init (this, savedInstanceState);
			global::Xamarin.Forms.Forms.Init (this, savedInstanceState);
			ImageCircleRenderer.Init ();
			LoadApplication (new App ());


			RequestVariousPermissions ();
		}

		public override void OnRequestPermissionsResult (int requestCode, string [] permissions, [GeneratedEnum] global::Android.Content.PM.Permission [] grantResults)
		{
			base.OnRequestPermissionsResult (requestCode, permissions, grantResults);
		}

		void RequestVariousPermissions ()
		{
			var perms = new string [] {
				Manifest.Permission.ReadContacts, Manifest.Permission.ReadExternalStorage,
				Manifest.Permission.WriteExternalStorage
			};
			ActivityCompat.RequestPermissions (this, perms, 1);
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);
		}
	}
}