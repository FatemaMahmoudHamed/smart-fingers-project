import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { DefautComponent} from './defaut.component';
import { CustomersModule} from 'src/app/modules/customers/customers.module';
import { RouterModule } from '@angular/router';
import { HomeModule} from 'src/app/modules/home/home.module';
import { SharedModule } from 'src/app/shared/shared.module';
import{MatSidenavModule} from '@angular/material/sidenav';
import { FlexLayoutModule } from '@angular/flex-layout';
import { SettingsModule } from 'src/app/modules/settings/settings.module';
import {  MyprofileModule } from 'src/app/modules/myprofile/myprofile.module';

import {MatDividerModule} from '@angular/material/divider';
import {MatCardModule} from '@angular/material/card';
import {MatPaginatorModule} from '@angular/material/paginator';
import {MatTableModule} from '@angular/material/table';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { FullCalendarModule } from '@fullcalendar/angular';
import {MatButtonModule} from '@angular/material/button';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatIconModule} from '@angular/material/icon';
import {MatBadgeModule} from '@angular/material/badge';
import {MatListModule} from '@angular/material/list';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatSelectModule} from '@angular/material/select';
import {MatRadioModule} from '@angular/material/radio';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatChipsModule} from '@angular/material/chips';
import {MatTooltipModule} from '@angular/material/tooltip';
import { ReactiveFormsModule } from '@angular/forms';


import {NgbPaginationModule, NgbAlertModule,NgbDatepickerModule,NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { MyprofileComponent } from 'src/app/modules/myprofile/myprofile.component';


@NgModule({
  declarations: [
    DefautComponent,
  ],
  imports: [
    CommonModule,
    CustomersModule,
    RouterModule,
    SharedModule,
    MatSidenavModule,
    FlexLayoutModule,
    MatDividerModule,
    MatCardModule,
    MatPaginatorModule,
    MatTableModule,
    HttpClientModule,
    FullCalendarModule,
    BrowserModule,
    MatButtonModule,
    MatToolbarModule,
    MatIconModule,
    MatSidenavModule,
    MatBadgeModule,
    MatListModule,
    MatGridListModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatRadioModule,
    MatDatepickerModule,
    MatChipsModule,
    MatTooltipModule,
    MatTableModule,
    MatPaginatorModule,
    ReactiveFormsModule,
    NgbDatepickerModule,
      MatButtonModule,
      MatToolbarModule,
      MatIconModule,
      MatSidenavModule,
      MatBadgeModule,
      MatListModule,
      MatGridListModule,
      MatInputModule,
      MatFormFieldModule,
      MatSelectModule,
      MatRadioModule,
      MatDatepickerModule,
      MatChipsModule,
      MatTooltipModule,
      MatTableModule,
      MatPaginatorModule,
      SettingsModule,
      MyprofileModule,
      HomeModule      

    ],
    providers: [
      MatDatepickerModule,
      DatePipe
   ]
})
export class DefautModule { }
