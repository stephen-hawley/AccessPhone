using System;

using Xamarin.Forms;

namespace AccessPhone {
	public enum Permission {
		Allowed, // you can do it
		Disallowed, // you can't do it and can't see it
		UnavailableNow, // you can't do it right now
	}

	public enum PermissionRequest {
		Pending,
		Allowed,
		Disallowed
	}
}

