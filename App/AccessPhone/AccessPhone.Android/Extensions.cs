using System;
using Android.Database;
using System.IO;

namespace AccessPhone.Droid {
	public static class Extensions {
		public static string StringFromKey (this ICursor cursor, string key)
		{
			if (cursor == null)
				throw new ArgumentNullException (nameof (cursor));
			if (key == null)
				throw new ArgumentNullException (nameof (key));
			return cursor.GetString (cursor.GetColumnIndex (key));
		}
	}
}
