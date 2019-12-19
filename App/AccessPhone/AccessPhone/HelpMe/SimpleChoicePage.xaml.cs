using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace AccessPhone.HelpMe {
	public partial class SimpleChoicePage : ContentPage {
		TopLevelDataModel topLevelDataModel;
		public SimpleChoicePage (TopLevelDataModel topLevelDataModel, SimpleChoiceViewModel model)
		{
			InitializeComponent ();
			this.topLevelDataModel = topLevelDataModel;
			Model = model;
			BindingContext = Model;
		}

		public SimpleChoiceViewModel Model {
			get; set;
		}

		public void PerformResponse (Response response)
		{
			switch (response.ResponseAction) {
			case ResponseActionType.FurtherQuestion:
				GoToNextPage (response.ResponseParameter as string);
				break;
			case ResponseActionType.EndCase:
				GoToEndCase (response.ResponseParameter as string);
				break;
			default:
				break;
			}
		}

		void GoToNextPage (string nextPageKey)
		{
			SimpleChoiceViewModel nextModel;
			if (topLevelDataModel.HelpMeDatabase.TryGetValue (nextPageKey, out nextModel)) {
				var nextPage = new SimpleChoicePage (topLevelDataModel, nextModel);
				Navigation.PushAsync (nextPage);
			}
		}

		void GoToEndCase (string label)
		{
			var endPage = new EndCasePage (label ?? "");
			Navigation.PushAsync (endPage);
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			Model.SimpleChoicePage = this;
		}

		protected override void OnDisappearing ()
		{
			base.OnDisappearing ();
			Model.SimpleChoicePage = null;
		}
	}
}
