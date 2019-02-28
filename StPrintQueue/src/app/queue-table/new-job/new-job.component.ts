import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { Job } from 'src/app/models/Job';

@Component({
  selector: 'app-new-job',
  templateUrl: './new-job.component.html',
  styleUrls: ['./new-job.component.scss']
})
export class NewJobComponent implements OnInit {

  @Output() OnAdd = new EventEmitter<Job>();

  newJob: Job = new Job();
  
  constructor() { }

  ngOnInit() {
  }

  addJob() {
    if (this.newJob.name.length == 0) {
      //show some warning
    }
    else if (this.newJob.duration < 1) {
      //show some warning
    }
    else {
      this.OnAdd.emit(this.newJob);
      this.newJob = new Job();
    }
    

  }
}
