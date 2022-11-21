import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {SideNavAdminComponent} from "./side-nav-admin/side-nav-admin.component";
import {RouterModule, Routes} from "@angular/router";
import {SideNavUserComponent} from "./side-nav-user/side-nav-user.component";
import {DeviceOverviewComponent} from "./UserPages/device-overview/device-overview.component";
import {DeviceRegistrationComponent} from "./UserPages/device-registration/device-registration.component";
import {ProfileInfoComponent} from "./UserPages/profile-info/profile-info.component";
import {ContactInfoComponent} from "./UserPages/contact-info/contact-info.component";
import {SupportComponent} from "./UserPages/support/support.component";
import {NotFoundComponent} from "./UserPages/not-found/not-found.component";



const routes: Routes=[
  {path: 'admin', component: SideNavAdminComponent}, //Admin login
  {path: 'user', component: SideNavUserComponent}, //User login
  {path: 'user', component: SideNavUserComponent, children:[
      {path: 'devices', component: DeviceOverviewComponent}, // When you click devices as user
      {path: 'deviceRegistration', component: DeviceRegistrationComponent}, //When you click registrering as user
      {path: 'profileInfo', component: ProfileInfoComponent}, //When you click profil info as user
      {path: 'contactInfo', component: ContactInfoComponent}, //When you click kontakt info as user
      {path: 'support', component: SupportComponent}, //When you click support as user
      {path: '**', component: NotFoundComponent} //When you write a non-existing earl after /user/..
    ]
  },
  {path: '**', component: NotFoundComponent}


];


@NgModule({
  declarations: [],
  imports: [CommonModule,
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})

export class AppRoutingModule { }
