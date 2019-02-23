import { JobStatus } from './JobStatus';
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
}