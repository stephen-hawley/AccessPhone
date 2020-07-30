using System;

using Xamarin.Forms;

namespace AccessPhone.Directions {
	public class DirectionsStart : ContentPage {
		public DirectionsStart ()
		{
			Content = new StackLayout {
				Children = {
					new Label { Text = "Hello ContentPage" }
				}
			};
		}
	}
}

