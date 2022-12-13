import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {UserOverviewComponent} from "../../AdminPages/user-overview/user-overview.component";
import {MatSnackBar} from "@angular/material/snack-bar";
import {DeviceService} from "../../../services/device-service/device.service";
import {FormControl, FormGroup} from "@angular/forms";

@Component({
  selector: 'app-report-error',
  templateUrl: './report-error.component.html',
  styleUrls: ['./report-error.component.scss']
})
export class ReportErrorComponent implements OnInit{

  errorReportForm = new FormGroup({
    IdForm: new FormControl(this.data.device.id),
    nameForm: new FormControl(this.data.device.deviceName),
    subjectForm: new FormControl(),
    bodyForm: new FormControl()
  });


  constructor(private deviceService: DeviceService,  public dialogRef: MatDialogRef<UserOverviewComponent>, @Inject(MAT_DIALOG_DATA) public data : any, private _snackBar: MatSnackBar) {
  }

  async ngOnInit() {
    this.dialogRef.updateSize("500px", "624px");
  }

  /**
   * Method for sending an error report for a device.
   */
  async sendErrorReport() {
    let device = await this.deviceService.getDeviceById(this.errorReportForm.value.IdForm);
    let dto = {
      id: device.id,
      deviceName: device.deviceName,
      serialNumber: device.serialNumber,
      status: 'Defekt',
      userId: device.userId,
      requestValue: device.requestValue,
      dateOfTurnIn: device.dateOfTurnIn,
      dateOfIssue: device.dateOfIssue,
      errorSubject: this.errorReportForm.value.subjectForm,
      errorDescription: this.errorReportForm.value.bodyForm
    }
    await this.deviceService.updateDevice(dto, device.id);

    this.dialogRef.close();
    this._snackBar.open('Administrationen har modtaget din fejlmelding', 'Luk', {
      duration: 3000
    });
  }

}
