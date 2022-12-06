import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {AppComponent} from './app.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatToolbarModule} from "@angular/material/toolbar";
import {MatSidenavModule} from "@angular/material/sidenav";
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {MatDividerModule} from "@angular/material/divider";
import {SideNavAdminComponent} from './side-nav-admin/side-nav-admin.component';
import {AppRoutingModule} from './app-routing.module';
import {SideNavUserComponent} from './side-nav-user/side-nav-user.component';
import {DeviceOverviewComponent} from './userPages/device-overview/device-overview.component';
import {ProfileInfoComponent} from './userPages/profile-info/profile-info.component';
import {DeviceRegistrationComponent} from './userPages/device-registration/device-registration.component';
import {UserProfileInfoComponent} from './userPages/profile-info/user-profile-info.component';
import {ContactInfoComponent} from './userPages/contact-info/contact-info.component';
import {SupportComponent} from './userPages/support/support.component';
import {NotFoundComponent} from './userPages/not-found/not-found.component';
import {LoginComponent} from './login/login.component';
import {MatInputModule} from "@angular/material/input";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {RegisterComponent} from './register/register.component';
import {MatSnackBar} from "@angular/material/snack-bar";
import {MatMenuModule} from "@angular/material/menu";
import {FlexLayoutModule} from "@angular/flex-layout"
import {AdminDeviceOverviewComponent} from "./AdminPages/admin-device-overview/admin-device-overview.component";
import {AdminDeviceRegistrationComponent} from "./AdminPages/admin-device-registration/admin-device-registration.component";
import {UserOverviewComponent} from "./AdminPages/user-overview/user-overview.component";
import {MatTableModule} from "@angular/material/table";
import {MatPaginatorModule} from "@angular/material/paginator";
import {MatSortModule} from "@angular/material/sort";
import {MatCardModule} from "@angular/material/card";
import {MatStepperModule} from "@angular/material/stepper";
import {MatSelectModule} from "@angular/material/select";
import {MatDatepickerModule} from "@angular/material/datepicker";
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatNativeDateModule, MAT_DATE_LOCALE} from '@angular/material/core';
import {MatListModule} from "@angular/material/list";
import {EditDeviceComponent} from './AdminPages/edit-device/edit-device.component';
import {SendMailComponent} from './AdminPages/send-mail/send-mail.component';
import {MatDialogModule} from "@angular/material/dialog";
import {CreateUserComponent} from './AdminPages/create-user/create-user.component';
import { MyDevicesComponent } from './userPages/my-devices/my-devices.component';
import { RequestsComponent } from './AdminPages/requests/requests.component';
import {LoadingSpinnerComponent} from "./shared/loading-spinner/loading-spinner.component";
import {AdminProfileInfoComponent} from "./AdminPages/admin-profile-info/admin-profile-info.component";

// @ts-ignore
@NgModule({
  declarations: [
    AppComponent,
    SideNavAdminComponent,
    SideNavUserComponent,
    DeviceOverviewComponent,
    ProfileInfoComponent,
    DeviceRegistrationComponent,
    UserProfileInfoComponent,
    ContactInfoComponent,
    SupportComponent,
    NotFoundComponent,
    LoginComponent,
    RegisterComponent,
    AdminDeviceOverviewComponent,
    AdminDeviceRegistrationComponent,
    UserOverviewComponent,
    EditDeviceComponent,
    SendMailComponent,
    CreateUserComponent,
    AppComponent,
    SideNavAdminComponent,
    SideNavUserComponent,
    DeviceOverviewComponent,
    DeviceRegistrationComponent,
    UserProfileInfoComponent,
    ContactInfoComponent,
    SupportComponent,
    NotFoundComponent,
    LoginComponent,
    RegisterComponent,
    LoadingSpinnerComponent,
    AdminDeviceOverviewComponent,
    AdminDeviceRegistrationComponent,
    UserOverviewComponent,
    EditDeviceComponent,
    SendMailComponent,
    CreateUserComponent,
    MyDevicesComponent,
    RequestsComponent
    LoadingSpinnerComponent,
    AdminProfileInfoComponent
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
    FormsModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatCardModule,
    MatStepperModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatNativeDateModule,
    MatListModule,
    MatDialogModule,
    FlexLayoutModule,
    MatMenuModule
  ],
  providers: [
    {provide: MAT_DATE_LOCALE, useValue: 'en-GB'},
    MatSnackBar
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
