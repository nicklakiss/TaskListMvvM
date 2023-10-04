using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskListMvvM
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public bool IsCompleted { get; set; }

        public override string ToString() => $"{Id}. {Title} {IsCompleted}";
    }
}
