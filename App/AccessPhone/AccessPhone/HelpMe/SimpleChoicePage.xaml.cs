using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace AccessPhone.HelpMe {
	public partial class SimpleChoicePage : ContentPage {
		public SimpleChoicePage ()
		{
			InitializeComponent ();
			Model = new SimpleChoiceViewModel ();
			Model.Text = "Are you hurt?";
			Model.Responses.Add (new Response () { Text = "Yes" });
			Model.Responses.Add (new Response () { Text = "No" });
			MainText.BindingContext = Model;
			Choices.BindingContext = Model.Responses;
		}

		public SimpleChoiceViewModel Model {
			get; set;
		}
		public string SomeText { get => "Some Text"; }
	}
}
