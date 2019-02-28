import { JobStatus } from './JobStatus';
import * as moment from 'moment';

export class Job
{
    id: number;
    name: string;
    duration: number;
    status: JobStatus;

    startTime: Date;
    endTime: Date;
    constructor() {

    }

    static getRemainingSeconds(job: Job) : number {
        //startTime + duration(s) = estimated endTime
        //endTime - currentTime = actual timestamp in milliseconds. /1000 to get it in seconds.
        if(job.startTime) {
            let actualSecondsLeft  = moment(job.startTime).add(job.duration, 's').diff(moment()) /1000;
            return actualSecondsLeft;
        }
    }
}