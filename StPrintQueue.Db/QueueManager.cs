using StPrintQueue.Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StPrintQueue.Db
{
    public class QueueManager
    {
        private int _seq = 0;
        private IList<Job> _jobs { get; set; }
        public IList<Job> Jobs
        {
            get
            {
                return _jobs;
            }
            set
            {
                _jobs = value;
            }
        }

        public QueueManager()
        {
            _jobs = new List<Job>();
        }

       

        public Job GetJobById(int jobId)
        {
            var job = _jobs.FirstOrDefault(x => x.Id == jobId);
            if (job == null)
                throw new Exception($"Job with Id {jobId} was not found in queue");
            return job;
        }

        public void Add(Job newJob)
        {
            newJob.Status = _jobs.Count == 0 ? JobStatus.Printing : JobStatus.Queued;
            newJob.Id = _seq++;
            _jobs.Add(newJob);
        }

        public void Remove(Job job)
        {
            _jobs.Remove(job);
        }

        public void SetOrder(Job job, int position)
        {
            _jobs.Remove(job);
            _jobs.Insert(position, job);
        }
    }
}
