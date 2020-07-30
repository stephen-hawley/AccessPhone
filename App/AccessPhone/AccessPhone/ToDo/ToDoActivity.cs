using System;
using System.ComponentModel;
using Xamarin.Forms;
using AccessPhone.ToDo;

namespace AccessPhone.ToDo {
	public class ToDoActivity : ITopLevelActivity {
		IToDoService toDoService;

		public ToDoActivity ()
		{
			toDoService = DependencyService.Get<IToDoService> ();
			toDoService.CreateService ();
			enabled = toDoService.Available;
		}

		public string Name => "ToDo";

		public TopLevelDataModel TopLevelDataModel { get; set; }

		public Page GetPage ()
		{
			return new ToDoPage (this, TopLevelDataModel, toDoService);
		}

		bool allowed = true;
		public bool IsAllowed {
			get { return allowed; }
			set {
				if (allowed != value) {
					allowed = value;
					OnPropertyChanged (nameof (IsAllowed));
				}
			}
		}

		bool enabled = true;
		public bool IsEnabled {
			get { return enabled; }
			set {
				if (enabled != value) {
					enabled = value;
					OnPropertyChanged (nameof (IsEnabled));
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };

		protected virtual void OnPropertyChanged (string propertyName)
		{
			PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (propertyName));
		}
	}
}
