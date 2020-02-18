using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AccessPhone.HelpMe {
	public class Response {
		public string Text { get; set; }
		public string UniqueID { get; set; }
		public ResponseActionType ResponseAction { get; set; }
		public object ResponseParameter { get; set; }
		public IHelpItem FurtherQuestion { get; set; }
	}
}
