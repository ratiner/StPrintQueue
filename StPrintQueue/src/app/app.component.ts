import { Component } from '@angular/core';
import { Job } from './models/Job';
import { JobsService } from './jobs.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'StPrintQueue';
  jobList: Observable<Job[]>;

  newJob: Job = new Job();

  constructor(private jobService: JobsService) { }

  ngOnInit() {
    this.refresh();
  }

  refresh() {
    this.jobList = this.jobService.getJobs();
  }

  addJob() {
    if (this.newJob.name.length == 0) {
      //show some warning
    }
    else if (this.newJob.duration == 0) {
      //show some warning
    }
    else {
      this.jobService.addJob(this.newJob).subscribe(r => {
        this.newJob = new Job();
        this.refresh();
      });
    }
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
