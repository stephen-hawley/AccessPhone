using System;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AccessPhone {
	public partial class App : Application {
		HttpClient client;
		public App ()
		{
			InitializeComponent ();

			MainPage = new NavigationPage (new MainPage ());
			client = new HttpClient ();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

		public HttpClient HttpClient {  get { return client; } }
	}
}
