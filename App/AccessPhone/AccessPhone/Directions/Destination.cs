using System;
using Xamarin.Essentials;

namespace AccessPhone.Directions {
	public class Destination {
		public string Name { get; set; }
		public string Address { get; set; }
		public Location GeoLocation { get; set; }
	}
}
