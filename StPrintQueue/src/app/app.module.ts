import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { JobsService } from './jobs.service';
import { DurationToTimePipe } from './duration-to-time.pipe';

@NgModule({
  declarations: [
    AppComponent,
    DurationToTimePipe
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
