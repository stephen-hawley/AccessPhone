using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Contexts;
using AccessPhone.Contacts;
using Android.Database;
using Android.Provider;
using Xamarin.Forms;
using Android.App;
using Android.OS;
using Android;
using Android.Support.V4.Content;
using Android.Support.Design.Widget;
using System.Threading.Tasks;
using Android.Support.V4.App;
using AccessPhone.Droid;
using Android.Content;

[assembly: Dependency (typeof (AccessPhone.Android.Contacts.ContactService))]
namespace AccessPhone.Android.Contacts {
	public class ContactService : IContactService {
		public ContactService ()
		{
		}

		public IEnumerable<Contact> GetContacts ()
		{
			var uri = ContactsContract.Contacts.ContentUri;
			var ctx = Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity;
			ICursor cursor = null;
			try {
				var cursortask = TryGetContactsAsync (ctx, uri);
				cursortask.Wait ();
				cursor = cursortask.Result;
			} catch (Exception e) {
				Console.WriteLine ("error here");
			}
			if (cursor.Count == 0) yield break;

			while (cursor.MoveToNext ()) {
				var contact = CreateContact (cursor, ctx);

				if (!string.IsNullOrWhiteSpace (contact.FullName))
					yield return contact;
			}
		}

		Contact CreateContact (ICursor cursor, Activity ctx)
		{
			var contactId = cursor.StringFromKey (ContactsContract.Contacts.InterfaceConsts.Id);
			var numbers = GetNumbers (ctx, contactId);
			var emails = GetEmails (ctx, contactId);
			var fullName = cursor.StringFromKey (ContactsContract.Contacts.InterfaceConsts.DisplayName);
			var parts = fullName.Split (' ');
			var firstName = GetFirstName (parts);
			var lastName = GetLastName (parts);
			var photoURI = cursor.StringFromKey (ContactsContract.Contacts.InterfaceConsts.PhotoUri) ?? "";
			var imageUriStr = GetRealPathFromURI (ctx, photoURI);
			var thumbUri = cursor.StringFromKey (ContactsContract.Contacts.InterfaceConsts.PhotoThumbnailUri) ?? "";
			var thumbUriStr = GetRealPathFromURI (ctx, thumbUri);

			var contact = new Contact () {
				FullName = fullName,
				FirstName = firstName,
				LastName = lastName,
				ThumbnailPath = thumbUriStr,
				ImagePath = imageUriStr
			};
			contact.Emails.AddRange (emails);


			return contact;
		}


		static string GetFirstName (string [] parts)
		{
			if (parts.Length == 0)
				return "";
			if (parts.Length == 1)
				return parts [0];

			for (int i = 0; i < parts.Length - 1; i++) {
				if (IsOneOf (parts[i], "Mr.", "Mrs.", "Ms.", "Mr", "Mrs", "Ms", "Dr.", "Dr")) {
					continue;
				}
				return parts [i];
			}
			return "";
		}

		static string GetLastName (string [] parts)
		{
			for (int i = parts.Length - 1; i > 1; i--) {
				if (IsOneOf ("Sr", "Sr.", "Jr.", "Jr", "II", "III", "IV")) {
					continue;
				}
				return parts [i];
			}
			return "";
		}

		static bool IsOneOf (string target, params string[] set)
		{
			foreach (var comp in set) {
				if (target.Equals (comp, StringComparison.OrdinalIgnoreCase))
					return true;
			}
			return false;
		}

		private static IEnumerable<string> GetNumbers (Activity ctx, string contactId)
		{
			var key = ContactsContract.CommonDataKinds.Phone.Number;

			var cursor = ctx.ApplicationContext.ContentResolver.Query (
			    ContactsContract.CommonDataKinds.Phone.ContentUri,
			    null,
			    ContactsContract.CommonDataKinds.Phone.InterfaceConsts.ContactId + " = ?",
			    new [] { contactId },
			    null
			);

			return ReadCursorItems (cursor, key);
		}

		private static IEnumerable<EmailAddress> GetEmails (Activity ctx, string contactId)
		{
			var key = ContactsContract.CommonDataKinds.Email.InterfaceConsts.Data;

			var cursor = ctx.ApplicationContext.ContentResolver.Query (
			    ContactsContract.CommonDataKinds.Email.ContentUri,
			    null,
			    ContactsContract.CommonDataKinds.Email.InterfaceConsts.ContactId + " = ?",
			    new [] { contactId },
			    null);

			foreach (var item in ReadCursorItems (cursor, key)) {
				yield return new EmailAddress () {
					Address = item
				};
			}
		}

		private static IEnumerable<string> ReadCursorItems (ICursor cursor, string key)
		{
			while (cursor.MoveToNext ()) {
				var value = cursor.StringFromKey (key);
				yield return value;
			}
			cursor.Close ();
		}


		async Task<ICursor> TryGetContactsAsync (Activity ctx, global::Android.Net.Uri uri)
		{
			if ((int)Build.VERSION.SdkInt < 23) {
				return await GetContactsAsync (ctx, uri);
			}

			return await GetContactsPermissionAsync (ctx, uri);
		}

		readonly string [] PermissionsContacts =
		{
			Manifest.Permission.ReadContacts
		};

		const int RequestContactsId = 0;

		async Task<ICursor> GetContactsPermissionAsync (Activity ctx, global::Android.Net.Uri uri)
		{
			//Check to see if any permission in our group is available, if one, then all are
			const string permission = Manifest.Permission.ReadContacts;
			if (ContextCompat.CheckSelfPermission (ctx, permission) == global::Android.Content.PM.Permission.Granted) {
				return await GetContactsAsync (ctx, uri);
			} else {

			}
			if (ActivityCompat.ShouldShowRequestPermissionRationale (ctx, Manifest.Permission.AccessFineLocation)) {
				var requiredPermissions = new String [] { Manifest.Permission.AccessFineLocation };
				Snackbar.Make (ctx.CurrentFocus,
					       "This app needs to read contacts to send messages or call people.",
					       Snackbar.LengthIndefinite)
					.SetAction ("OK",
						   (v) => {
							   ActivityCompat.RequestPermissions (ctx, PermissionsContacts, RequestContactsId);
						   }
					).Show ();
			} else {
				ActivityCompat.RequestPermissions (ctx, PermissionsContacts, RequestContactsId);
			}
			return await GetContactsAsync (ctx, uri);
		}

		async Task<ICursor> GetContactsAsync (Activity ctx, global::Android.Net.Uri uri)
		{
			return await Task.Run<ICursor> (() => ctx.ApplicationContext.ContentResolver.Query (uri, null, null, null, null));
		}

		string GetRealPathFromURI (Activity ctx, string uri)
		{
			//return "http://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg";
				return GetRealPathFromURI (ctx, global::Android.Net.Uri.Parse (uri));
		}

		string GetRealPathFromURI (Activity ctx, global::Android.Net.Uri uri)
		{
			try {
				using (var src = ctx.Application.ContentResolver.OpenInputStream (uri)) {
					var tempFileName = Path.GetTempFileName ();
					using (var dst = new FileStream (tempFileName, FileMode.Create, FileAccess.ReadWrite)) {
						src.CopyTo (dst);
					}
					return tempFileName;
				}
				
			} catch (Exception e0) {
				return null;
			}
		}
	}
}

