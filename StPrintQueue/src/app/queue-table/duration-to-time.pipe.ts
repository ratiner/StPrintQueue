import { Pipe, PipeTransform } from '@angular/core';
import { Job } from '../models/Job';
import * as moment from 'moment';

@Pipe({
  name: 'durationToTime'
})
export class DurationToTimePipe implements PipeTransform {

  transform(jobs: Job[], args?: any): any {
    //this pipe will set "startTime" and "entTime" properties for each job
    //the mission is to calculate the estimated startTime and endTime for each job.
    //the base idea is that each job startTime is delayed by the total duration of all jobs ahead of it.

    //we can estimate each job start and end time by interating through all jobs in queue,
    //each time increasing the counter value with the current job duration.
    //counter inital value is the current timestamp or the active job actual startTime.

    if (jobs && jobs.length > 0) {
      //for the first job: startTime is already set by the server with the real time the job started.
      let counter = jobs[0].startTime ? moment(jobs[0].startTime) : moment();

      jobs.forEach((job, i) => {
        job.startTime = counter.toDate();
        counter = counter.add(job.duration, 's');
        job.endTime = counter.toDate();
      });
    }
    return jobs;
  }

}
