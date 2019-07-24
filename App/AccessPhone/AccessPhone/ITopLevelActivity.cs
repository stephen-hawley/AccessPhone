using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace AccessPhone {
	public interface ITopLevelActivity : INotifyPropertyChanged {
		string Name { get; }
		bool IsAllowed { get; set; }
		bool IsEnabled { get; set; }
		Page GetPage();
		TopLevelDataModel TopLevelDataModel { get; set; }
	}
}
