using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POE_CyberBuddy
{
    public class TaskItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReminderDate { get; set; }
        public bool IsCompleted { get; set; } = false;

        public override string ToString()
        {
            var reminderText = ReminderDate.HasValue
                ? $" (Reminder: {ReminderDate.Value.ToShortDateString()})"
                : ""; // (Microsoft Learn, 2023)
            var status = IsCompleted ? "[Completed]" : "[Pending]"; // (Microsoft Learn, 2023)
            return $"{status} {Title} - {Description}{reminderText}";
        }
    }
    public class TaskManager
    {
        private List<TaskItem> tasks = new List<TaskItem>();

        public void AddTask(string title, string description, DateTime? reminderDate = null)
        {
            tasks.Add(new TaskItem
            {
                Title = title,
                Description = description,
                ReminderDate = reminderDate
            });
        }

        public List<TaskItem> GetAllTasks() => tasks;

        public bool CompleteTask(string title)
        {
            var task = tasks.Find(t => t.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (task != null)
            {
                task.IsCompleted = true;
                return true;
            }
            return false;
        }

        public bool DeleteTask(string title)
        {
            var task = tasks.Find(t => t.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (task != null)
            {
                tasks.Remove(task);
                return true;
            }
            return false;
        }
    }
}

/*Refrence List:
    
    Microsoft Learn, 2023. ?: operator - the ternary conditional operator. [online] Available at: 
    https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/conditional-operator
    (Accessed: 22 June 2025).
 
 */