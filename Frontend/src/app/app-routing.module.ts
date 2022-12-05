import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {SideNavAdminComponent} from "./side-nav-admin/side-nav-admin.component";
import {RouterModule, Routes} from "@angular/router";
import {SideNavUserComponent} from "./side-nav-user/side-nav-user.component";
import {DeviceOverviewComponent} from "./userPages/device-overview/device-overview.component";
import {ProfileInfoComponent} from "./userPages/profile-info/profile-info.component";
import {SupportComponent} from "./userPages/support/support.component";
import {NotFoundComponent} from "./userPages/not-found/not-found.component";
import {AdminDeviceOverviewComponent} from "./AdminPages/admin-device-overview/admin-device-overview.component";
import {AdminDeviceRegistrationComponent} from "./AdminPages/admin-device-registration/admin-device-registration.component";
import {UserOverviewComponent} from "./AdminPages/user-overview/user-overview.component";
import {LoginComponent} from "./login/login.component";
import {AdminAuthGuardService} from "../services/user-service/admin-auth-guard.service";
import {UserAuthGuardService} from "../services/user-service/user-auth-guard.service";
import {RegisterComponent} from "./register/register.component";
import {MyDevicesComponent} from "./userPages/my-devices/my-devices.component";
import {RequestsComponent} from "./AdminPages/requests/requests.component";


const routes: Routes=[
  {path: '', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'administrator', component: SideNavAdminComponent, canActivate: [AdminAuthGuardService], children:[
      {path: 'enheder', component: AdminDeviceOverviewComponent, },
      {path: 'enheds-registrering', component: AdminDeviceRegistrationComponent},
      {path: 'brugere', component: UserOverviewComponent},
      {path: 'forespørgsler', component: RequestsComponent},
    ]
  },
  {path: 'bruger', component: SideNavUserComponent, canActivate: [UserAuthGuardService], children:[
      {path: 'enheder', component: DeviceOverviewComponent}, // When you click enheder as user
      {path: 'mine-enheder', component: MyDevicesComponent}, // When you click my devices as user
      {path: 'profil-information', component: ProfileInfoComponent}, //When you click profil info as user
      {path: 'hjaelp', component: SupportComponent}, //When you click support as user
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
