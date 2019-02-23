import { Pipe, PipeTransform } from '@angular/core';
import { Job } from './models/Job';
import * as moment from 'moment';

@Pipe({
  name: 'durationToTime'
})
export class DurationToTimePipe implements PipeTransform {

  transform(jobs: Job[], args?: any): any {
    if (jobs) {
      var counter = moment();

      jobs.forEach((job, i) => {
        if(i > 0)
          job.startTime = counter.toDate();
        counter = counter.add(job.duration, 'm');
        job.endTime = counter.toDate();
      });
      //  console.log(jobs);
    }
    return jobs;
  }

}
