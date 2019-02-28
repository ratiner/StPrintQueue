using StPrintQueue.Db;
using StPrintQueue.Db.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StPrintQueue.Print
{
    public class PrintService
    {
        private static QueueManager _queue;
        private static Timer _timer;


        public static Task BackgroundService(ref QueueManager queue, CancellationToken token)
        {
            /*
             Simulating a printing delay.
             When there are jobs in a queue, The first jobs will be picked.
             [StartTime] will be set so we can calculate later the remaining time left for the job to complete. (Will be used in UI)
             Timer will be set for the duration of the job. By the end, it will remove the job from the list.
             In case of application shutdown, CancellationToken will be sent and notify the simulator to exit.
             */


            _queue = queue;
            _timer = null;

            return Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    if(_timer == null && _queue.HasJobs)
                    {
                        var job = _queue.Jobs[0];
                        job.Status = JobStatus.Printing;
                        job.StartTime = DateTime.Now;

                        _timer = new Timer((state)=> 
                        {
                            _queue.Remove(job);
                            _timer = null;
                        }, null, (int)job.Duration*1000, Timeout.Infinite); //in milliseconds
                    }
                }


                //Instead of killing job task
                //Maybe a better solution would be to just delay the shutdown by the remaining time left for the job to complete
                //It can be calculated by the formula [startTime.Value.AddSeconds(duration) - DateTime.Now]

                CancelPrinting();
            });
        }

        public static void CancelPrinting()
        {
            if(_timer != null)
            {
                _timer.Dispose();
                _timer = null;
            }
        }
    }


}
