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
			toDoView.ItemTapped += OnToDoItemTapped;
			DoneView.ItemTapped += OnDoneItemTapped;
		}

		protected override void OnAppearing ()
		{
			toDoService.LoadAndMergeTodaysItems (ToDo, ToDone);
		}

		protected void OnToDoChecked (object sender, EventArgs args)
		{
			var item = FindAssociatedToDoItem (sender as Element, ToDo);
			if (item != null) {
				item.Completed = true;
				ToDo.Remove (item);
				ToDone.Add (item);
			}
		}

		protected void OnDoneUnchecked (object sender, EventArgs args)
		{
			var item = FindAssociatedToDoItem (sender as Element, ToDone);
			if (item != null) {
				item.Completed = false;
				ToDone.Remove (item);
				ToDo.Add (item);
			}
		}

		void OnToDoItemTapped (object sender, ItemTappedEventArgs e)
		{
			var item = ToDo [e.ItemIndex];
			item.Completed = true;
			ToDo.Remove (item);
			ToDone.Add (item);
		}

		void OnDoneItemTapped (object sender, ItemTappedEventArgs e)
		{
			var item = ToDone [e.ItemIndex];
			item.Completed = false;
			ToDone.Remove (item);
			ToDo.Add (item);
		}

		public ObservableCollection<IToDoItem> ToDo { get; private set; }
		public ObservableCollection<IToDoItem> ToDone { get; private set; }

		static IToDoItem FindAssociatedToDoItem (Element hostView, ObservableCollection<IToDoItem> items)
		{
			while (true) {
				if (hostView == null)
					return null;
				if (hostView.BindingContext is IToDoItem item) {
					if (items.Contains (item))
						return item;
					return null;
				} else {
					hostView = hostView.Parent;
				}
			}
		}
	}
}
