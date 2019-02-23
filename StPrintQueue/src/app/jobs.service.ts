import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Job } from './models/Job';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class JobsService {

  constructor(private http:HttpClient) { }

  getJobs(): Observable<Job[]> {
    return this.http.get<Job[]>('/api/jobs');
  }

  addJob(newJob: Job): Observable<Job> {
    return this.http.post<Job>('/api/jobs', newJob);
  }

  setOrder(job: Job, position: number){
    return this.http.post('/api/jobs/'+ job.id + '/setOrder', position);
  }

  deleteJob(job:Job) {
    return this.http.delete('/api/jobs/'+ job.id);
  }

  cancelJob() {
    return this.http.get('/api/jobs/cancel');
  }
}
