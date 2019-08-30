using System;

using Xamarin.Forms;
using AccessPhone.Droid.ToDo;
using AccessPhone.ToDo;
using System.Collections.ObjectModel;
using Android.Provider;
using Android.Content;
using Android.Database;
using Java.Util;
using System.Text;
using Android.Text.Format;
using System.Threading.Tasks;

[assembly: Dependency (typeof (ToDoService))]
namespace AccessPhone.Droid.ToDo {

	using AndUri = global::Android.Net.Uri;


	public class ToDoItem : IToDoItem {
		long calendarId;
		long eventId;

		public ToDoItem (long calendarId, long eventId, string text)
		{
			this.calendarId = calendarId;
			this.eventId = eventId;
			Text = text;
		}

		public string Text { get; set; }
		public bool Completed { get; set; }

		public bool Equals (IToDoItem other)
		{
			var item = other as ToDoItem;
			if (item == null)
				return false;
			return item.calendarId == calendarId &&
				item.eventId == eventId;
		}
	}


	public class ToDoService : IToDoService {
		const string kToDoPrefix = "TODO:";
		public bool Available => true;


		static string [] calendarProjection = {
			CalendarContract.Calendars.InterfaceConsts.Id,
			CalendarContract.Calendars.InterfaceConsts.CalendarDisplayName,
			CalendarContract.Calendars.InterfaceConsts.AccountName,
			CalendarContract.Calendars.InterfaceConsts.OwnerAccount,
		};

		static string [] eventProjection = {
			CalendarContract.Instances.EventId,
			CalendarContract.Instances.InterfaceConsts.Title,
		};

		public void CreateService ()
		{
		}

		public void LoadAndMergeTodaysItems (ObservableCollection<IToDoItem> toDo, ObservableCollection<IToDoItem> toDone)
		{

			var loader = new CursorLoader (Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity,
				CalendarContract.Calendars.ContentUri, calendarProjection, null, null, null);
			var cursor = (ICursor)loader.LoadInBackground ();


			if (!cursor.MoveToFirst ())
				return;

			do {
				var calID = cursor.GetLong (0);
				var calName = cursor.GetString (1);
				var accName = cursor.GetString (2);
				var ownerAcct = cursor.GetString (3);

				if (!(calName == accName && calName == ownerAcct))
					continue;


				// query today
				var startTime = Calendar.Instance;
				var now = DateTime.Now;
				startTime.Set (now.Year, now.Month - 1, now.Day, 0, 0, 0);

				var endTime = Calendar.Instance;
				endTime.Set (now.Year, now.Month - 1, now.Day, 23, 59, 59);

				var contentUriStr = $"content://com.android.calendar/instances/when/{startTime.TimeInMillis}/{endTime.TimeInMillis}/";
				var contentUri = AndUri.Parse (contentUriStr);
				var eventLoader = new CursorLoader (Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity,
					contentUri, eventProjection, null, null, null
					);

				var eventCursor = (ICursor)eventLoader.LoadInBackground ();
				if (!eventCursor.MoveToFirst ())
					return;
				do {
					var eventId = eventCursor.GetLong (0);
					var eventTitle = eventCursor.GetString (1);
					if (eventTitle.StartsWith (kToDoPrefix, StringComparison.Ordinal))
						AddOrMerge (new ToDoItem (calID, eventId, TrimmedTitle (eventTitle)), toDo, toDone);

				} while (eventCursor.MoveToNext ());

			} while (cursor.MoveToNext ());
		}


		static void AddOrMerge (IToDoItem item, ObservableCollection<IToDoItem> toDo, ObservableCollection<IToDoItem> toDone)
		{
			var toDoIndex = toDo.IndexOf (item);
			var toDoneIndex = toDone.IndexOf (item);

			if (toDoIndex < 0 && toDoneIndex < 0) {
				if (item.Completed)
					toDone.Add (item);
				else
					toDo.Add (item);
				return;
			}

			if (toDoneIndex >= 0) {
				if (toDone [toDoneIndex].Text != item.Text) {
					toDone [toDoneIndex].Text = item.Text;
					toDone [toDoneIndex] = toDone [toDoneIndex]; // force change
				}
				return;
			}

			if (toDo [toDoIndex].Text != item.Text) {
				toDo [toDoIndex].Text = item.Text;
				toDo [toDoIndex] = toDo [toDoIndex];
			}
		}


		static string TrimmedTitle (string title)
		{
			return title.Substring (kToDoPrefix.Length).TrimStart ();
		}
	}
}

