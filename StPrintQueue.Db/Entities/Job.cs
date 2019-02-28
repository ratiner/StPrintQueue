using System;
using System.Collections.Generic;
using System.Text;

namespace StPrintQueue.Db.Entities
{
    public class Job
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public DateTime? StartTime { get; set; }
        public JobStatus Status { get; set; }
    }
}
