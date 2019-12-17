using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AccessPhone.HelpMe {
	public interface IHelpItem {
		string Name { get; set; }
		HelpItemType Type { get; set; }
		string Text { get; set; }
		ObservableCollection<Response> Responses { get; }
	}
}
