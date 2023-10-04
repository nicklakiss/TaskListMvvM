using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace TaskListMvvM
{
    public class ViewModel : VMBase
    {
        private TaskItemManager _taskItemManager;
        private ObservableCollection<TaskItem> _tasks;
        private RelayCommand _addCommand;
        private RelayCommand _deleteCommand;
        private RelayCommand _clearCommand;
        private RelayCommand _updateCommand;
        private RelayCommand _changeDescriptionCommand;
        private string _description;
        private TaskItem _selectedItem;

        public ObservableCollection<TaskItem> Tasks
        {
            get { return _tasks; }
            set
            {
                _tasks = value;
                OnPropertyChanged(nameof(Tasks));
            }
        }
        public TaskItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        public ICommand AddCommand => _addCommand;
        public ICommand DeleteCommand => _deleteCommand;
        public ICommand ClearCommand => _clearCommand;
        public ICommand UpdateCommand => _updateCommand;
        public ICommand ChangeDescriptionCommand => _changeDescriptionCommand;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public ViewModel()
        {
            _taskItemManager = new();
            Tasks = new(_taskItemManager.GetTasks());
            _addCommand = new(Add);
            _deleteCommand = new(Delete);
            _clearCommand = new(Clear);
            _updateCommand = new(Update);
            _changeDescriptionCommand = new(ChangeDescription);
        }

        private void Add(object o)
        {
            if (string.IsNullOrWhiteSpace(Description))
                return;
            _taskItemManager.AddTask(new TaskItem { Title = string.Join(" ", Description.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))});
            Tasks = new(_taskItemManager.GetTasks());
            Description = string.Empty;
        }

        private void Delete(object o)
        {
            _taskItemManager?.DeleteTask(SelectedItem);
            Tasks = new(_taskItemManager.GetTasks());
        }

        private void Clear(object o)
        {
            _taskItemManager.Clear();
            Tasks = new(_taskItemManager.GetTasks());
        }

        private void Update(object o)
        {
            _taskItemManager?.UpdateTask(SelectedItem);
            Tasks = new(_taskItemManager.GetTasks());
        }

        private void ChangeDescription(object o)
        {
            _taskItemManager?.ChangeTaskDescription(SelectedItem, Description);
            Description = string.Empty;
            Tasks = new(_taskItemManager.GetTasks());

        }
    }
}
