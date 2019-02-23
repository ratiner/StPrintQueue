using StPrintQueue.Db.Entities;
using StPrintQueue.Db.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StPrintQueue.Db
{
    /// <summary>
    /// 
    /// </summary>
    public class QueueManager
    {
        private int _seq = 0;
        private IList<Job> _jobs { get; set; }
        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        public QueueManager()
        {
            _jobs = new List<Job>();
        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public Job GetJobById(int jobId)
        {
            var job = _jobs.FirstOrDefault(x => x.Id == jobId);
            if (job == null)
                throw new Exception($"Job with Id {jobId} was not found in queue");
            return job;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newJob"></param>
        public void Add(Job newJob)
        {
            if (newJob != null)
            {
                newJob.Status = _jobs.Count == 0 ? JobStatus.Printing : JobStatus.Queued;
                newJob.Id = _seq++;
                _jobs.Add(newJob);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="job"></param>
        public void Remove(Job job)
        {
            _jobs.Remove(job);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="job"></param>
        /// <param name="position"></param>
        public void SetOrder(Job job, int position)
        {
            if (position < 0 || position > _jobs.Count - 1)
                throw new Exception($"Position is out of bounds. Current range is 0-{_jobs.Count-1}");

            _jobs.Remove(job);
            _jobs.Insert(position, job);
        }

        public void WriteTo(string filePath)
        {
            var json = new FileStore(_seq, _jobs).AsString();
           
            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                writer.Write(json);
            }
        }

        public void LoadFrom(string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                string json = System.IO.File.ReadAllText(filePath);
                FileStore fs = FileStore.FromJson(json);
                _jobs = fs.Jobs;
                _seq = fs.Sequence;
            }
        }
    }
}
