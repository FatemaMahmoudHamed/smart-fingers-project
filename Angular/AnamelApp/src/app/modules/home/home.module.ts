import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from 'src/app/modules/home/home.component'
import { FullCalendarModule } from '@fullcalendar/angular';
import dayGridPlugin from '@fullcalendar/daygrid';
import interactionPlugin from '@fullcalendar/interaction';

FullCalendarModule.registerPlugins([ 
  dayGridPlugin,
  interactionPlugin,
]);


@NgModule({
  declarations: [
    HomeComponent
  ],
  imports: [
    CommonModule,
    FullCalendarModule
  ]
})
export class HomeModule { }
