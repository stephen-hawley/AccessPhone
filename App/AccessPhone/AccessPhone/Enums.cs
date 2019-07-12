using System;

using Xamarin.Forms;

namespace AccessPhone {
	public enum Permission {
		Allowed, // you can do it
		Disallowed, // you can't do it and can't see it
		Unavailable, // you can't do it right now
	}
}

