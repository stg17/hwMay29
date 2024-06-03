using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hwMay29.Data
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public User UserDoingTask { get; set; }
    }
}
