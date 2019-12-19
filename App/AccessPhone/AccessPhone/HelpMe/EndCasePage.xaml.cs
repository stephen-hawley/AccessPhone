using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace AccessPhone.HelpMe {
	public partial class EndCasePage : ContentPage {
		public EndCasePage (string text)
		{
			Text = text;
			InitializeComponent ();
			BindingContext = this;
		}
		void OKClicked (object sender, EventArgs e)
		{
			Navigation.PopToRootAsync ();
		}

		public string Text { get; private set; }
	}
}
