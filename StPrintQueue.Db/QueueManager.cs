using StPrintQueue.Db.Entities;
using StPrintQueue.Db.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StPrintQueue.Db
{
    public class QueueManager
    {
        private int _seq = 0;
        private IList<Job> _jobs { get; set; }
        /// <summary>
        /// Returns list of current jobs in queue
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
        /// Returns wether the current queue has jobs
        /// </summary>
        public bool HasJobs
        {
            get
            {
                return Jobs.Count > 0;
            }
        }

        public QueueManager()
        {
            _jobs = new List<Job>();
        }

       
        /// <summary>
        /// Finds and returns job by identifier
        /// </summary>
        /// <param name="jobId">Queue job id</param>
        /// <returns>Job object</returns>
        public Job GetJobById(int jobId)
        {
            var job = _jobs.FirstOrDefault(x => x.Id == jobId);
            if (job == null)
                throw new Exception($"Job with Id {jobId} was not found in queue");
            return job;
        }

        /// <summary>
        /// Appends a new job to the queue
        /// </summary>
        /// <param name="newJob">Retuns job object with id and status</param>
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
        /// Removes a job from a queue
        /// </summary>
        /// <param name="job">Job object to remove</param>
        public void Remove(Job job)
        {
            _jobs.Remove(job);
        }

        /// <summary>
        /// Change a job position in a queue
        /// </summary>
        /// <param name="job">Job object to move</param>
        /// <param name="position">New position of object</param>
        public void SetOrder(Job job, int position)
        {
            if (position < 0 || position > _jobs.Count - 1)
                throw new Exception($"Position is out of bounds. Current range is 0-{_jobs.Count-1}");

            _jobs.Remove(job);
            _jobs.Insert(position, job);
        }
        
        /// <summary>
        /// Saves the current queue state to a file in json format
        /// </summary>
        /// <param name="filePath">Target file path</param>
        public void WriteTo(string filePath)
        {
            var json = new FileStore(_seq, _jobs).AsString();
           
            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                writer.Write(json);
            }
        }

        /// <summary>
        /// Restores queue state from json file
        /// </summary>
        /// <param name="filePath">Source file path</param>
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
