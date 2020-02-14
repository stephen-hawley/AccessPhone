using System;
using System.Collections.Generic;
using System.Linq;

namespace AccessPhone.HelpMe {
	public class HelpMeDatabase : Dictionary <string, SimpleChoiceViewModel> {
		public HelpMeDatabase ()
			: base ()
		{
			var hurt = new SimpleChoiceViewModel () {
				Name = "Hurt",
				Text = "Are you hurt?",
			};
			hurt.Responses.Add (new Response () { Text = "Yes", UniqueID = "Yes", ResponseAction = ResponseActionType.FurtherQuestion, ResponseParameter = "Bleed" });
			hurt.Responses.Add (new Response () { Text = "No", UniqueID = "No", ResponseAction = ResponseActionType.FurtherQuestion, ResponseParameter = "Lost" });
			Add (hurt);
			StartingPoint = hurt.Name;

			var bleed = new SimpleChoiceViewModel () {
				Name = "Bleed",
				Text = "Are you bleeding?",
			};
			bleed.Responses.Add (new Response () { Text = "Yes", UniqueID = "Yes", ResponseAction = ResponseActionType.EndCase, ResponseParameter = "Try putting on a bandage." });
			bleed.Responses.Add (new Response () { Text = "No", UniqueID = "No", ResponseAction = ResponseActionType.EndCase, ResponseParameter = "I don't know what else to say." });
			Add (bleed);

			var lost = new SimpleChoiceViewModel () {
				Name = "Lost",
				Text = "Are you lost?",
			};
			lost.Responses.Add (new Response () { Text = "Yes", UniqueID = "Yes", ResponseAction = ResponseActionType.EndCase, ResponseParameter = "I don't know what to do. Sorry." });
			lost.Responses.Add (new Response () { Text = "No", UniqueID = "no", ResponseAction = ResponseActionType.EndCase, ResponseParameter = "I don't know how to help you. Sorry." });
			Add (lost);
		}

		void Add (SimpleChoiceViewModel model)
		{
			Add (model.Name, model);
		}



		public string StartingPoint { get; private set; }
	}
}
