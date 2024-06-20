using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ProyectoGrupo2.Models;
using ProyectoGrupo2.Data;
using Microsoft.Maui.Controls;
using System.IO;
using System.Threading.Tasks;

namespace ProyectoGrupo2.ViewModels
{
    public class TodoViewModel : BindableObject
    {
        private Database _database;
        private string _newTodoTitle;
        public ObservableCollection<TodoItem> TodoItems { get; set; }

        public string NewTodoTitle
        {
            get => _newTodoTitle;
            set
            {
                _newTodoTitle = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddTodoCommand { get; }
        public ICommand DeleteTodoCommand { get; }

        public TodoViewModel()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TodoListApp.db3");
            _database = new Database(dbPath);
            TodoItems = new ObservableCollection<TodoItem>();
            AddTodoCommand = new Command(OnAddTodo);
            DeleteTodoCommand = new Command<TodoItem>(OnDeleteTodo);

            LoadTodoItems();
        }

        private async void LoadTodoItems()
        {
            var items = await _database.GetTodoItemsAsync();
            foreach (var item in items)
            {
                TodoItems.Add(item);
            }
        }

        private async void OnAddTodo()
        {
            if (string.IsNullOrWhiteSpace(NewTodoTitle))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Title cannot be empty.", "OK");
                return;
            }

            var newItem = new TodoItem { Title = NewTodoTitle, Status = TodoStatus.PorHacer };
            await _database.SaveTodoItemAsync(newItem);
            TodoItems.Add(newItem);
            NewTodoTitle = string.Empty;
        }

        private async void OnDeleteTodo(TodoItem item)
        {
            await _database.DeleteTodoItemAsync(item);
            TodoItems.Remove(item);
        }
    }
}