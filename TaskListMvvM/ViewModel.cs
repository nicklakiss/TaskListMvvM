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
        private RelayCommand _deleteCommand;
        private RelayCommand _clearCommand;
        private RelayCommand _updateCommand;
        private RelayCommand _enterCommand;
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
            get 
            {
                Description = _selectedItem?.Title;
                return _selectedItem; 
            }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        public ICommand DeleteCommand => _deleteCommand;
        public ICommand ClearCommand => _clearCommand;
        public ICommand UpdateCommand => _updateCommand;
        public ICommand EnterCommand => _enterCommand;
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
            _deleteCommand = new(Delete);
            _clearCommand = new(Clear);
            _updateCommand = new(Update);
            _enterCommand = new(Enter);
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
            Tasks = new(_taskItemManager.GetTasks());
        }

        private void ChangeDescription(object o)
        {
            if (string.IsNullOrWhiteSpace(Description)) return;
            Description = string.Join(" ", Description.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
            _taskItemManager?.ChangeTaskDescription(_selectedItem, Description);
            Description = string.Empty;
            Tasks = new(_taskItemManager.GetTasks());
        }

        private void Enter(object o)
        {
            if (o is not null)
            {
                if (SelectedItem != null) 
                {
                    Description = o as string;
                    ChangeDescription(o);
                }
                else
                {
                    Description = o as string;
                    Add(o);
                }
            }
            
        }
    }
}
