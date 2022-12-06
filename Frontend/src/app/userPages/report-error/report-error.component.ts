import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {UserOverviewComponent} from "../../AdminPages/user-overview/user-overview.component";
import {MatSnackBar} from "@angular/material/snack-bar";
import {DeviceService} from "../../../services/device-service/device.service";
import {FormControl, FormGroup} from "@angular/forms";
import {Device} from "../../../Models/Interfaces/device";

@Component({
  selector: 'app-report-error',
  templateUrl: './report-error.component.html',
  styleUrls: ['./report-error.component.scss']
})
export class ReportErrorComponent implements OnInit{
  device: Device;
  subject: string = '';
  body: string = '';

  errorReportForm = new FormGroup({
    nameForm: new FormControl(this.data.device.deviceName),
    subjectForm: new FormControl(''),
    bodyForm: new FormControl(''),
  });


  constructor(private deviceService: DeviceService,  public dialogRef: MatDialogRef<UserOverviewComponent>, @Inject(MAT_DIALOG_DATA) public data : any, private _snackBar: MatSnackBar) {
  }

  ngOnInit(): void {
    this.dialogRef.updateSize('50%', '80%');
  }

  async sendErrorReport() {
    const error = this.errorReportForm.value;
    let dto = {
      deviceName: error.nameForm,
      subject: error.subjectForm,
      body: error.bodyForm
    }
    await this.deviceService.sendError(dto)
    this.dialogRef.close();
    this._snackBar.open('Administrationen har modtaget din fejlmelding ', 'Luk', {
      duration: 3000
    });
  }

}
