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
import { DeviceOverviewComponent } from './userPages/device-overview/device-overview.component';
import { DeviceRegistrationComponent } from './userPages/device-registration/device-registration.component';
import { ProfileInfoComponent } from './userPages/profile-info/profile-info.component';
import { ContactInfoComponent } from './userPages/contact-info/contact-info.component';
import { SupportComponent } from './userPages/support/support.component';
import { NotFoundComponent } from './userPages/not-found/not-found.component';
import { AdminLoginOverviewComponent } from './AdminPages/admin-login-overview/admin-login-overview.component';
import { LoginComponent } from './login/login.component';
import {MatInputModule} from "@angular/material/input";
import {FormsModule} from "@angular/forms";
import { RegisterComponent } from './register/register.component';
import { LoadingSpinnerComponent } from './shared/loading-spinner/loading-spinner.component';
import {MatSnackBar} from "@angular/material/snack-bar";
import {MatMenuModule} from "@angular/material/menu";
import {MatCardModule} from "@angular/material/card";
import {FlexLayoutModule} from "@angular/flex-layout"


// @ts-ignore
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
    LoginComponent,
    RegisterComponent,
    LoadingSpinnerComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatSidenavModule,
    MatDividerModule,
    AppRoutingModule,
    MatInputModule,
    FormsModule,
    MatIconModule,
    MatButtonModule,
    MatMenuModule,
    MatCardModule,
    FlexLayoutModule
  ],
  providers: [MatSnackBar],
  bootstrap: [AppComponent]
})
export class AppModule { }
