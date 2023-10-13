using System.Collections.Generic;
using System.Linq;


namespace TaskListMvvM
{
    public static class TaskItemManager
    {
        public static List<TaskItem> GetTasks()
        {
            using var context = new TaskListContext();
            return context.Tasks.ToList();
        }

        public static void AddTask(TaskItem task)
        {
            using var context = new TaskListContext();
            context.Tasks.Add(task);
            context.SaveChanges();
        }

        public static void UpdateTask(TaskItem taskItem)
        {
            using var context = new TaskListContext();
            var task = context.Tasks.Find(taskItem?.Id);
            if (task == null) return;
            task.IsCompleted = !task.IsCompleted;
            context.SaveChanges();
        }

        public static bool DeleteTask(TaskItem task)
        {
            using var context = new TaskListContext();
            if (task is null) return false;
            context.Tasks.Remove(task);
            context.SaveChanges();
            return true;
        }

        public static void Clear()
        {
            using var context = new TaskListContext();
            context.Database.EnsureDeleted();
        }

        public static void ChangeTaskDescription(TaskItem taskItem, string title)
        {
            using var context = new TaskListContext();
            if (taskItem is null) return;
            var task = context.Tasks.Find(taskItem.Id);
            if (task is null) return;
            task.Title = title;
            context.SaveChanges();
        }
    }
}
