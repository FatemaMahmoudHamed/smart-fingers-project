import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DefautComponent } from './layout/defaut/defaut.component';
import { CustomersComponent } from './modules/customers/customers.component';
import { HomeComponent } from './modules/home/home.component';
import { SettingsComponent } from './modules/settings/settings.component';
import { MyprofileComponent } from './modules/myprofile/myprofile.component';
import { LoginComponent } from './modules/login/login.component';
import{AuthService} from 'src/app/Services/auth.service';

const routes: Routes = [
  { path: 'login', component: LoginComponent},
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  }, 
  {
    path: '', component: DefautComponent,
    canActivate:[AuthService],
    children: [
      { path: 'home',component: HomeComponent },
      { path: 'customers', component: CustomersComponent },
      { path: 'settings', component: SettingsComponent },
      { path: 'myprofile', component: MyprofileComponent },
      ]
  },

];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
