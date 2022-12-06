import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {SideNavAdminComponent} from "./side-nav-admin/side-nav-admin.component";
import {RouterModule, Routes} from "@angular/router";
import {SideNavUserComponent} from "./side-nav-user/side-nav-user.component";
import {DeviceOverviewComponent} from "./userPages/device-overview/device-overview.component";
import {UserProfileInfoComponent} from "./userPages/profile-info/user-profile-info.component";
import {NotFoundComponent} from "./userPages/not-found/not-found.component";
import {AdminDeviceOverviewComponent} from "./AdminPages/admin-device-overview/admin-device-overview.component";
import {AdminDeviceRegistrationComponent} from "./AdminPages/admin-device-registration/admin-device-registration.component";
import {UserOverviewComponent} from "./AdminPages/user-overview/user-overview.component";
import {LoginComponent} from "./login/login.component";
import {AdminAuthGuardService} from "../services/user-service/admin-auth-guard.service";
import {UserAuthGuardService} from "../services/user-service/user-auth-guard.service";
import {RegisterComponent} from "./register/register.component";
import {AdminProfileInfoComponent} from "./AdminPages/admin-profile-info/admin-profile-info.component";
import {RequestDeviceOverviewComponent} from "./userPages/request-device-overview/request-device-overview.component";

const routes: Routes=[
  {path: '', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'administrator', component: SideNavAdminComponent, canActivate: [AdminAuthGuardService], children:[
      {path: 'enheder', component: AdminDeviceOverviewComponent, },
      {path: 'enheds-registrering', component: AdminDeviceRegistrationComponent},
      {path: 'brugere', component: UserOverviewComponent},
      {path: 'profil-information', component: AdminProfileInfoComponent}
    ]
  },
  {path: 'bruger', component: SideNavUserComponent, canActivate: [UserAuthGuardService], children:[
      {path: 'mine-enheder', component: DeviceOverviewComponent}, // When you click mine enheder as user
      {path: 'forespørg-enhed', component: RequestDeviceOverviewComponent}, // When you click forespørg enheder as user
      {path: 'profil-information', component: UserProfileInfoComponent}, //When you click profil info as user
      {path: '**', component: NotFoundComponent} //When you write a non-existing url after /user/..
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
