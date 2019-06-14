using System;
using Xamarin.Forms;

namespace AccessPhone {
	public interface ITopLevelActivity {
		string Name { get; }
		Button Button { get; }
	}
}
