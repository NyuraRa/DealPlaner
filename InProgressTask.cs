using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealPlaner
{
    public class InProgressTask : Task
    {

        public InProgressTask(int id, string title, string description, DateTime dueDate, int priority)
            : base(id, title, description, dueDate, priority)
        {
            Status = "В процессе";
        }
    }
}
