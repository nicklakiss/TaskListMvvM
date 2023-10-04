using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TaskListMvvM
{
    public class TaskItemManager
    {
        public List<TaskItem> GetTasks()
        {
            using (var context = new TaskListContext())
            {
                return context.Tasks.ToList();
            }
        }

        public void AddTask(TaskItem task)
        {
            using (var context = new TaskListContext())
            {
                context.Tasks.Add(task);
                context.SaveChanges();
            }
        }

        public void UpdateTask(TaskItem taskItem)
        {
            using (var context = new TaskListContext())
            {
                if (taskItem != null)
                {
                    var task = context.Tasks.Find(taskItem.Id);
                    if (task != null)
                    {
                        if (task.IsCompleted)
                            task.IsCompleted = false;
                        else
                            task.IsCompleted = true;
                        context.SaveChanges();
                    }
                }
            }
        }

        public bool DeleteTask(TaskItem task)
        {
            using (var context = new TaskListContext())
            {
                if (task is not null)
                {
                    context.Tasks.Remove(task);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public void Clear()
        {
            using (var context = new TaskListContext())
            {
                context.Database.EnsureDeleted();
            }
        }

        public void ChangeTaskDescription(TaskItem taskItem, string title)
        {
            using (var context = new TaskListContext())
            {
                if (taskItem is not null)
                {
                    var task = context.Tasks.Find(taskItem.Id);
                    if (task is not null)
                    {
                        task.Title = title;
                        context.SaveChanges();
                    }

                }
            }
        }
    }
}
