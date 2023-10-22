﻿using System;
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
        #region Private Fields

        private ObservableCollection<TaskItem> _tasks;
        private RelayCommand _addCommand;
        private RelayCommand _deleteCommand;
        private RelayCommand _clearCommand;
        private RelayCommand _updateCommand;
        private RelayCommand _changeDescriptionCommand;
        private string _description;
        private TaskItem _selectedItem;

        #endregion
        #region Properties

        public ObservableCollection<TaskItem> Tasks
        {
            get => _tasks; 
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
        #endregion
        #region Private Methods

        private void Add(object o)
        {
            if (string.IsNullOrWhiteSpace(Description))
                return;
            TaskItemManager.AddTask(new TaskItem { Title = string.Join(" ", Description.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)) });
            Tasks = new(TaskItemManager.GetTasks());
            Description = string.Empty;
        }

        private void Delete(object o)
        {
            TaskItemManager.DeleteTask(SelectedItem);
            Tasks = new(TaskItemManager.GetTasks());
        }

        private void Clear(object o)
        {
            TaskItemManager.Clear();
            Tasks = new(TaskItemManager.GetTasks());
        }

        private void Update(object o)
        {
            TaskItemManager.UpdateTask(SelectedItem);
            Tasks = new(TaskItemManager.GetTasks());
        }

        private void ChangeDescription(object o)
        {
            if (string.IsNullOrWhiteSpace(Description)) return;
            Description = string.Join(" ", Description.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
            TaskItemManager.ChangeTaskDescription(_selectedItem, Description);
            Description = string.Empty;
            Tasks = new(TaskItemManager.GetTasks());
        }

        private void Init()
        {
            Tasks = new(TaskItemManager.GetTasks());
            _addCommand = new(Add);
            _deleteCommand = new(Delete);
            _clearCommand = new(Clear);
            _updateCommand = new(Update);
            _changeDescriptionCommand = new(ChangeDescription);
		_enterCommand = new(Enter);
        }
        #endregion
        #region Commands

        public ICommand AddCommand => _addCommand;
        public ICommand DeleteCommand => _deleteCommand;
        public ICommand ClearCommand => _clearCommand;
        public ICommand UpdateCommand => _updateCommand;
        public ICommand ChangeDescriptionCommand => _changeDescriptionCommand;

        #endregion
        
        public ViewModel()
        {
            Init();
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
