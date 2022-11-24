import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatToolbarModule} from "@angular/material/toolbar";
import {MatSidenavModule} from "@angular/material/sidenav";
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {MatDividerModule} from "@angular/material/divider";
import { SideNavAdminComponent } from './side-nav-admin/side-nav-admin.component';
import { AppRoutingModule } from './app-routing.module';
import { SideNavUserComponent } from './side-nav-user/side-nav-user.component';
import { DeviceOverviewComponent } from './UserPages/device-overview/device-overview.component';
import { DeviceRegistrationComponent } from './UserPages/device-registration/device-registration.component';
import { ProfileInfoComponent } from './UserPages/profile-info/profile-info.component';
import { ContactInfoComponent } from './UserPages/contact-info/contact-info.component';
import { SupportComponent } from './UserPages/support/support.component';
import { NotFoundComponent } from './UserPages/not-found/not-found.component';
import { AdminLoginOverviewComponent } from './AdminPages/admin-login-overview/admin-login-overview.component';
import { LoginComponent } from './login/login.component';
import {MatInputModule} from "@angular/material/input";
import {FormsModule} from "@angular/forms";


@NgModule({
  declarations: [
    AppComponent,
    SideNavAdminComponent,
    SideNavUserComponent,
    DeviceOverviewComponent,
    DeviceRegistrationComponent,
    ProfileInfoComponent,
    ContactInfoComponent,
    SupportComponent,
    NotFoundComponent,
    AdminLoginOverviewComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatSidenavModule,
    MatButtonModule,
    MatIconModule,
    MatDividerModule,
    AppRoutingModule,
    MatInputModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
