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
import {UserProfileInfoComponent} from './userPages/profile-info/user-profile-info.component';
import {LoginComponent} from './login/login.component';
import {MatInputModule} from "@angular/material/input";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {RegisterComponent} from './register/register.component';
import {MatSnackBar} from "@angular/material/snack-bar";
import {MatMenuModule} from "@angular/material/menu";
import {FlexLayoutModule} from "@angular/flex-layout"
import {AdminDeviceOverviewComponent} from "./AdminPages/admin-device-overview/admin-device-overview.component";
import {
  AdminDeviceRegistrationComponent
} from "./AdminPages/admin-device-registration/admin-device-registration.component";
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
import {LoadingSpinnerComponent} from "./shared/loading-spinner/loading-spinner.component";
import {AdminProfileInfoComponent} from "./AdminPages/admin-profile-info/admin-profile-info.component";
import { RequestDeviceOverviewComponent } from './userPages/request-device-overview/request-device-overview.component';
import { ReportErrorComponent } from './userPages/report-error/report-error.component';
import { RequestsComponent } from './AdminPages/requests/requests.component';
import { EditUserComponent } from './AdminPages/edit-user/edit-user.component';
import { ErrorHandlingComponent } from './AdminPages/error-handling/error-handling.component';
import { ViewReportComponent } from './AdminPages/view-report/view-report.component';
import {MatBadgeModule} from "@angular/material/badge";

// @ts-ignore
@NgModule({
  bootstrap: [AppComponent],
  declarations: [
    AppComponent,
    SideNavAdminComponent,
    SideNavUserComponent,
    DeviceOverviewComponent,
    UserProfileInfoComponent,
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
    UserProfileInfoComponent,
    LoginComponent,
    RegisterComponent,
    LoadingSpinnerComponent,
    AdminDeviceOverviewComponent,
    AdminDeviceRegistrationComponent,
    UserOverviewComponent,
    EditDeviceComponent,
    SendMailComponent,
    LoadingSpinnerComponent,
    AdminProfileInfoComponent,
    RequestDeviceOverviewComponent,
    ReportErrorComponent,
    EditUserComponent,
    ErrorHandlingComponent,
    ViewReportComponent,
    RequestsComponent,
    EditUserComponent
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
    MatMenuModule,
    MatBadgeModule
  ],
  providers: [
    {provide: MAT_DATE_LOCALE, useValue: 'en-GB'},
    MatSnackBar
  ]
})
export class AppModule {
}
