using System;
using System.Collections.ObjectModel;

namespace AccessPhone.ToDo {
	public interface IToDoService {
		void CreateService ();
		bool Available { get; }
		void LoadAndMergeTodaysItems (ObservableCollection<IToDoItem> toDo, ObservableCollection<IToDoItem> toDone);
	}
}
