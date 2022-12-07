import {Component, Inject, OnInit} from '@angular/core';
import {DeviceService} from "../../../services/device-service/device.service";
import {AdminDeviceRegistrationComponent} from "../admin-device-registration/admin-device-registration.component";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {UserService} from "../../../services/user-service/user.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {Device} from "../../../Models/Interfaces/device";

@Component({
  selector: 'app-edit-device',
  templateUrl: './edit-device.component.html',
  styleUrls: ['./edit-device.component.scss']
})
export class EditDeviceComponent implements OnInit{
  device: Device;
  users: any[] = [];


  editDevice = new FormGroup({
    id: new FormControl(this.data.device.id),
    deviceName: new FormControl(this.data.device.deviceName, [Validators.required]),
    serialNumber: new FormControl(this.data.device.serialNumber, [Validators.required]),
    status: new FormControl(this.data.device.status, [Validators.required]),
    userId: new FormControl(this.data.device.userId),
    dateOfIssue: new FormControl(this.data.device.dateOfIssue),
    dateOfTurnIn: new FormControl(this.data.device.dateOfTurnIn)
  })


  constructor(private deviceService: DeviceService, public userService: UserService, public dialogRef: MatDialogRef<AdminDeviceRegistrationComponent>, @Inject(MAT_DIALOG_DATA) public data : any, private _snackBar: MatSnackBar) {
  }

  async ngOnInit(){
    this.dialogRef.updateSize("700px", "700px");
    this.device = await this.deviceService.getDeviceById(this.data.device.id);
    this.editDevice.patchValue({
      id: this.device.id,
      deviceName: this.device.deviceName,
      serialNumber: this.device.serialNumber,
      status: this.device.status,
      userId: this.device.userId,
      dateOfIssue: this.device.dateOfIssue ,
      dateOfTurnIn: this.device.dateOfTurnIn
    });
    this.users = await this.userService.getUsersTypeUser();
  }

  async save() {
    const device = this.editDevice.value;
    let dto = {
      id: device.id,
      deviceName: device.deviceName,
      serialNumber: device.serialNumber,
      status: device.status,
      userId: device.userId,
      requestValue: new String('IkkeSendt'),
      dateOfIssue: new Date(new Date(device.dateOfIssue).setHours(24)).toISOString().slice(0,10),
      dateOfTurnIn: new Date(new Date(device.dateOfTurnIn).setHours(24)).toISOString().slice(0,10)
    }

    let d = await this.deviceService.updateDevice(dto, device.id)
    this.deviceService.devices.map(obj => {
      if (obj == device.id){
        obj = d;
        return obj;
      }
    });
    this.dialogRef.close();
    this._snackBar.open('Den valgte enhed blevet redigeret', 'Luk', {
      duration: 3000
    });
  }
}
