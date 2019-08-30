using System;
namespace AccessPhone.ToDo {
	public interface IToDoItem : IEquatable<IToDoItem> {
		string Text { get; set; }
		bool Completed { get; set; }
	}
}
