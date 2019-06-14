using System;
using Xamarin.Forms;

namespace AccessPhone {
	public class TalkActivity : ITopLevelActivity {
		Button button;
		string name;
		public TalkActivity ()
		{
			name = "Talk";
			button = new Button () { Text = "Talk" };

		}

		public string Name => name;

		public Button Button => button;
	}
}
