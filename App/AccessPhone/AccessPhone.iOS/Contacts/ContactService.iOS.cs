using System;
using System.Collections.Generic;
using System.IO;
using AccessPhone.Contacts;
using Contacts;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency (typeof (AccessPhone.iOS.Contacts.ContactService))]
namespace AccessPhone.iOS.Contacts {
	public class ContactService : IContactService
	{
		public ContactService ()
		{
		}

		public IEnumerable<Contact> GetContacts ()
		{
			var keys = CNContactFormatter.GetDescriptorForRequiredKeys (CNContactFormatterStyle.FullName);
			var keysToFetch = new [] {
				CNContactKey.Identifier, CNContactKey.GivenName, CNContactKey.FamilyName,
				CNContactKey.EmailAddresses, CNContactKey.PhoneNumbers, CNContactKey.ImageData,
				CNContactKey.ImageDataAvailable, CNContactKey.ThumbnailImageData,
				
			};
			NSError error;

			//var containerId = new CNContactStore().DefaultContainerIdentifier;
			// using the container id of null to get all containers.
			// If you want to get contacts for only a single container type, you can specify that here
			var contactList = new List<CNContact> ();
			using (var store = new CNContactStore ()) {
				var allContainers = store.GetContainers (null, out error);
				foreach (var container in allContainers) {
					try {
						using (var predicate = CNContact.GetPredicateForContactsInContainer (container.Identifier)) {
							var containerResults = store.GetUnifiedContacts (predicate, keysToFetch, out error);
							contactList.AddRange (containerResults);
						}
					} catch (Exception ex) {
						continue;
					}
				}
				foreach (var item in contactList) {
					if (item.GivenName == null) continue;
					var image = item.ImageDataAvailable ? CacheImage (item.ImageData) : null;
					var thumb = CacheImage (item.ThumbnailImageData);
					var formatterKey = new [] { CNContactFormatter.GetDescriptorForRequiredKeys (CNContactFormatterStyle.FullName) };
					var altItem = store.GetUnifiedContact<ICNKeyDescriptor> (item.Identifier, formatterKey, out error);
					var contact = new Contact () {
						FirstName = item.GivenName,
						LastName = item.FamilyName,
						FullName = CNContactFormatter.GetStringFrom (altItem, CNContactFormatterStyle.FullName),
						ImagePath = image,
						ThumbnailPath = thumb
					};
					contact.Numbers.AddRange (ToPhoneNumbers (item.PhoneNumbers));

					yield return contact;
				}
			}
		}


		IEnumerable<PhoneNumber> ToPhoneNumbers (CNLabeledValue<CNPhoneNumber>[] numbers)
		{
			foreach (var number in numbers) {
				var myNumber = new PhoneNumber () {
					Number = number.Value.StringValue,
					CanText = IsTextable (number.Label)
				};
				yield return myNumber;
			}
		}

		static bool IsTextable (string label)
		{
			return label == CNLabelPhoneNumberKey.iPhone ||
				label == CNLabelPhoneNumberKey.Mobile;
		}


		string CacheImage (NSData imageData)
		{
			if (imageData == null)
				return null;
			var image = new UIImage (imageData);
			var path = Path.GetTempFileName () + ".png";
			using (var stm = new FileStream (path, FileMode.Create, FileAccess.ReadWrite)) {
				using (var pngData = image.AsPNG ()) {
					var pngArray = pngData.ToArray ();
					stm.Write (pngArray, 0, pngArray.Length);
				}
			}
			return path;
		}
	}
}
