using System;
using AccessPhone.ToDo;
using AccessPhone.iOS.ToDo;
using Xamarin.Forms;
using System.Collections.Generic;
using EventKit;
using Foundation;
using System.Collections.ObjectModel;

[assembly: Dependency (typeof (ToDoService))]
namespace AccessPhone.iOS.ToDo {
	internal class ToDoItem : IToDoItem {

		public ToDoItem (EKReminder reminder)
		{
			Completed = reminder.Completed;
			Text = reminder.Title;
			identifier = reminder.UUID;
		}

		private string identifier;

		public string Text { get; set; }
		public bool Completed { get; set; }

		public bool Equals (IToDoItem other)
		{
			var item = other as ToDoItem;
			if (item == null)
				return false;
			return item.identifier == identifier;
		}
	}


	public class ToDoService : IToDoService {

		EKEventStore eventStore = null;
		bool available = false;

		public ToDoService ()
		{
		}

		public void CreateService ()
		{
			if (eventStore == null) {
				eventStore = new EKEventStore ();
				eventStore.RequestAccess (EKEntityType.Reminder, (granted, err) => {
					available = granted;
				});
			}
		}

		public bool Available => available;

		public void LoadAndMergeTodaysItems (ObservableCollection<IToDoItem> toDo, ObservableCollection<IToDoItem> toDone)
		{
			var items = new List<IToDoItem> ();

			var predicate = eventStore.PredicateForReminders (null);


			eventStore.FetchReminders (predicate, (EKReminder [] reminders) => {
				foreach (var reminder in reminders) {
					if (reminder.Alarms == null)
						continue;
					foreach (var alarm in reminder.Alarms) {
						var alarmDate = (DateTime)alarm.AbsoluteDate;
						if (!IsToday (alarmDate))
							continue;
						AddOrMerge (new ToDoItem (reminder), toDo, toDone);
					}
				}
			});
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

		static bool IsToday (DateTime t)
		{
			var today = DateTime.Today;
			return t.Year == today.Year && t.Month == today.Month && t.Day == today.Day;
		}
	}
}
