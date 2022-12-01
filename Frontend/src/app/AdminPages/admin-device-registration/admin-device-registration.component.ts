import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl, Validators} from '@angular/forms';
import {UserService} from "../../../services/user-service/user.service";
import {customAxios, DeviceService} from "../../../services/device-service/device.service";
import {MatSnackBar} from "@angular/material/snack-bar";


@Component({
  selector: 'app-admin-device-registration',
  templateUrl: './admin-device-registration.component.html',
  styleUrls: ['./admin-device-registration.component.scss']
})
export class AdminDeviceRegistrationComponent implements OnInit{
  users: any[] = [];

  deviceNameControl = new FormControl('', [Validators.required]);
  serialNumberControl = new FormControl('', [Validators.required]);
  statusControl = new FormControl('', [Validators.required]);

  chosenUserControl = new FormControl();

  dateOfIssueControl = new FormControl()
  dateOfTurnInControl = new FormControl()

  firstFormGroup = this.formBuilder.group({
    deviceNameControl: this.deviceNameControl,
    serialNumberControl: this.serialNumberControl,
    statusControl: this.statusControl
  })

  secondFormGroup = this.formBuilder.group({
    chosenValueControl: this.chosenUserControl
  })

  thirdFormGroup = this.formBuilder.group({
    dateOfIssueControl: this.dateOfIssueControl,
    dateOfTurnInControl: this.dateOfTurnInControl
  })


  constructor(private formBuilder: FormBuilder, private userService: UserService, private deviceService: DeviceService) {

  }

  async ngOnInit() {
    this.users = await this.userService.getUsers();
  }

  //TODO IsoToString cannot be null fix pls
  async createDevice() {
    const devicePartOne = this.firstFormGroup.value;
    const devicePartTwo = this.secondFormGroup.value;
    const devicePartThree = this.thirdFormGroup.value;
    let dto = {
      deviceName: devicePartOne.deviceNameControl,
      serialNumber: devicePartOne.serialNumberControl,
      status: devicePartOne.statusControl,
      userId: devicePartTwo.chosenValueControl,
      dateOfIssue: new Date(new Date(devicePartThree.dateOfIssueControl).setHours(24)).toISOString().slice(0,10),
      dateOfTurnIn: new Date(new Date(devicePartThree.dateOfTurnInControl).setHours(24)).toISOString().slice(0,10)
    }
    await this.deviceService.createDevice(dto);

  }
}
