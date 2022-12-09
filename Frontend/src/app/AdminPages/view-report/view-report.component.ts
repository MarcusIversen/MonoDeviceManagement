import {Component, Inject, OnInit} from '@angular/core';
import {DeviceService} from "../../../services/device-service/device.service";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {UserOverviewComponent} from "../user-overview/user-overview.component";
import {MatSnackBar} from "@angular/material/snack-bar";
import {Device} from "../../../Models/Interfaces/device";
import {FormControl, FormGroup} from "@angular/forms";

@Component({
  selector: 'app-view-report',
  templateUrl: './view-report.component.html',
  styleUrls: ['./view-report.component.scss']
})
export class ViewReportComponent implements OnInit {
  device: Device;

  viewReportForm = new FormGroup({
    nameForm: new FormControl(this.data.device.deviceName),
    subjectForm: new FormControl(this.data.device.errorSubject),
    bodyForm: new FormControl(this.data.device.errorDescription),
  });

  constructor(private deviceService: DeviceService,
              private dialogRef: MatDialogRef<UserOverviewComponent>, @Inject(MAT_DIALOG_DATA) public data: any,
              private _snackBar: MatSnackBar) {
  }

  async ngOnInit() {
    this.dialogRef.updateSize('50%', '80%');
  }

  changeStatus() {
    //TODO
  }

}
