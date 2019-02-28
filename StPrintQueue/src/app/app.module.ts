import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { JobsService } from './jobs.service';
import { QueueTableComponent } from './queue-table/queue-table.component';
import { DurationToTimePipe } from './queue-table/duration-to-time.pipe';
import { NewJobComponent } from './queue-table/new-job/new-job.component';

@NgModule({
  declarations: [
    AppComponent,
    QueueTableComponent,
    DurationToTimePipe,
    NewJobComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [JobsService],
  bootstrap: [AppComponent]
})
export class AppModule { }
