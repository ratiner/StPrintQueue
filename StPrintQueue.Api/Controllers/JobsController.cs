using Microsoft.AspNetCore.Mvc;
using StPrintQueue.Db;
using StPrintQueue.Db.Entities;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StPrintQueue.Api.Controllers
{
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly QueueManager _queue;

        public JobsController(QueueManager queue)
        {
            _queue = queue;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Job> Get()
        {
            return _queue.Jobs;  
        }

        [HttpGet]
        [Route("cancel")]
        public void Cancel()
        {
            //cancel currently printing job
            if (_queue.Jobs.Count == 0)
                return; //no active printing job.

            var printingJob = _queue.Jobs[0];
            if (printingJob.Status == JobStatus.Printing)
            {
                //TODO: send a cancel command or something to the physical printer using a fancy API
                //in order to actually cancel a printing job.

                _queue.Remove(printingJob);

                //then something should send the second job in queue to the printer
                //and change it status. For now we do this forcefuly.
                if (_queue.Jobs.Count > 0)
                    _queue.Jobs[0].Status = JobStatus.Printing;
            }

        }

        // POST api/<controller>/5/SetOrder
        [HttpPost]
        [Route("{id}/setOrder")]
        public void SetOrder(int id, [FromBody]int position)
        {
            var job = _queue.GetJobById(id);
            if (job.Status == JobStatus.Printing)
                throw new Exception("Changing order for an active job is forbidden.");
            _queue.SetOrder(job, position);
        }


        // POST api/<controller>
        [HttpPost]
        public Job Post([FromBody]Job job)
        {
            _queue.Add(job);
            return job;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var job = _queue.GetJobById(id);
            if (job.Status == JobStatus.Printing)
                throw new Exception("Can not delete an active job. Concider canceling instead.");

            _queue.Remove(job);
        }
    }
}
