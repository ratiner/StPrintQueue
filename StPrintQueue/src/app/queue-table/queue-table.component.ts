import { Component, OnInit  } from '@angular/core';
import { Job } from '../models/Job';
import { JobsService } from '../jobs.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-queue-table',
  templateUrl: './queue-table.component.html',
  styleUrls: ['./queue-table.component.scss']
})
export class QueueTableComponent implements OnInit {
  refreshTimer: any;
  jobList: Array<Job>;


  constructor(private jobService: JobsService) { }

  ngOnInit() {
    this.refresh();
  }

  refresh() {
    this.jobService.getJobs().subscribe((jobs) => {
      this.jobList = jobs;

      if(this.refreshTimer)
        clearTimeout(this.refreshTimer);

      if(jobs.length > 0 && jobs[0].startTime) {
        let miliseconds = (Job.getRemainingSeconds(jobs[0])+1) * 1000;
        this.refreshTimer = setTimeout(()=>this.refresh(), miliseconds);
      }
    });
  }

  addJob(newJob: Job) {
    debugger;
    this.jobService.addJob(newJob).subscribe(r => {
      this.refresh();
    });
  }

  move(job: Job, position: number) {
    this.jobService.setOrder(job, position).subscribe(r => {
      this.refresh();
    });
  }


  deleteJob(job: Job) {
    this.jobService.deleteJob(job).subscribe(r => {
      this.refresh();
    });
  }

  cancelJob(job: Job) {
    this.jobService.cancelJob().subscribe(r => {
      this.refresh();
    });
  }
}
