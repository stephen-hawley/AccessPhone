using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AccessPhone.ToDo;
using Xamarin.Forms;

namespace AccessPhone {
	public partial class ToDoPage : ContentPage {
		ToDoActivity toDoActivity;
		TopLevelDataModel topLevelDataModel;
		IToDoService toDoService;

		public ToDoPage (ToDoActivity toDoActivity, TopLevelDataModel topLevelDataModel, IToDoService service)
		{
			toDoService = service;
			this.toDoActivity = toDoActivity;
			this.topLevelDataModel = topLevelDataModel;
			ToDo = topLevelDataModel.ToDo;
			ToDone = topLevelDataModel.ToDone;
			InitializeComponent ();
			toDoView.BindingContext = ToDo;
			DoneView.BindingContext = ToDone;
		}

		protected override void OnAppearing ()
		{
			toDoService.LoadAndMergeTodaysItems (ToDo, ToDone);
		}

		public void OnSwipedToDo (object sender, SwipedEventArgs args)
		{
			Console.WriteLine ("doing something");
		}

		public void OnDoneDelete (object sender, EventArgs e)
		{
			var mi = ((MenuItem)sender);
		}

		public void OnToDoDelete (object sender, EventArgs e)
		{
			var mi = ((MenuItem)sender);
		}


		public ObservableCollection<IToDoItem> ToDo { get; private set; }
		public ObservableCollection<IToDoItem> ToDone { get; private set; }
	}
}
