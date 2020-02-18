using System;
using System.Collections.Generic;
using System.Linq;

namespace AccessPhone.HelpMe {
	public class StaticHelpTree {
		public StaticHelpTree ()
		{
			var bleed = new SimpleChoiceViewModel () {
				Name = "Bleed",
				Text = "Are you bleeding?",
			};
			bleed.Responses.Add (new Response () { Text = "Yes", UniqueID = "Yes", ResponseAction = ResponseActionType.EndCase, ResponseParameter = "Try putting on a bandage." });
			bleed.Responses.Add (new Response () { Text = "No", UniqueID = "No", ResponseAction = ResponseActionType.EndCase, ResponseParameter = "I don't know what else to say." });

			var lost = new SimpleChoiceViewModel () {
				Name = "Lost",
				Text = "Are you lost?",
			};
			lost.Responses.Add (new Response () { Text = "Yes", UniqueID = "Yes", ResponseAction = ResponseActionType.EndCase, ResponseParameter = "I don't know what to do. Sorry." });
			lost.Responses.Add (new Response () { Text = "No", UniqueID = "No", ResponseAction = ResponseActionType.EndCase, ResponseParameter = "I don't know how to help you. Sorry." });


			var hurt = new SimpleChoiceViewModel () {
				Name = "Hurt",
				Text = "Are you hurt?",
			};

			var hurtYes = new Response () { Text = "Yes", UniqueID = "Yes", ResponseAction = ResponseActionType.FurtherQuestion, ResponseParameter = "Bleed", FurtherQuestion = bleed };
			hurt.Responses.Add (hurtYes);
			hurt.Responses.Add (new Response () { Text = "No", UniqueID = "No", ResponseAction = ResponseActionType.FurtherQuestion, ResponseParameter = "Lost", FurtherQuestion = lost });

			Start = hurt;
		}

		public SimpleChoiceViewModel Start { get; private set; }
	}
}
