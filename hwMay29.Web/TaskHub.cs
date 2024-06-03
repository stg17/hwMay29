using hwMay29.Data;
using Microsoft.AspNetCore.SignalR;

namespace hwMay29.Web
{
    public class TaskHub : Hub
    {
        private readonly string _connectionString;
        public TaskHub(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public void GetTasks()
        {
            var repo = new TaskRepository(_connectionString);
            Clients.All.SendAsync("getTasks", repo.GetAllTasks());
        }

        public void TakeTask(TaskItem task)
        {
            Console.WriteLine(task.TaskName);
            var taskRepo = new TaskRepository(_connectionString);
            var userRepo = new UserRepository(_connectionString);
            var user = userRepo.GetByEmail(Context.User.Identity.Name);
            taskRepo.AddUserToTask(user, task.Id);
            GetTasks();
        }

        public void AddTask(TaskItem task)
        {
            var taskRepo = new TaskRepository(_connectionString);
            taskRepo.AddTask(task);
            GetTasks();
        }

        public void RemoveTask(TaskItem task)
        {
            var taskRepo = new TaskRepository(_connectionString);
            taskRepo.DeleteTask(task);
            GetTasks();
        }
    }
}
