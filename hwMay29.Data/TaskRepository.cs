using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hwMay29.Data
{
    public class TaskRepository
    {
        private readonly string _connectionString;
        public TaskRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddTask(TaskItem task)
        {
            var context = new TaskDataContext(_connectionString);
            context.Tasks.Add(task);
            context.SaveChanges();
        }

        public List<TaskItem> GetAllTasks()
        {
            var context = new TaskDataContext(_connectionString);
            return context.Tasks.Include(t => t.UserDoingTask).ToList();
        }

        public void AddUserToTask(User user, int taskId)
        {
            var context = new TaskDataContext(_connectionString);

            var taskItem = context.Tasks.Find(taskId);
            if (taskItem != null)
            {
                taskItem.UserDoingTask = user;
                context.SaveChanges();
            }
        }

        public void DeleteTask(TaskItem task)
        {
            var context = new TaskDataContext(_connectionString);
            context.Remove(task);
            context.SaveChanges();
        }

    }
}
