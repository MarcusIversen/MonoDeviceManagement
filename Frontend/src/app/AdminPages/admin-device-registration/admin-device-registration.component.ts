import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl, Validators} from '@angular/forms';
import {UserService} from "../../../services/user-service/user.service";
import {DeviceService} from "../../../services/device-service/device.service";
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


  constructor(private formBuilder: FormBuilder, private userService: UserService, private deviceService: DeviceService, private _snackBar: MatSnackBar) {

  }

  async ngOnInit() {
    this.users = await this.userService.getUsersTypeUser();
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
      requestValue: null,
      dateOfIssue: null,
      dateOfTurnIn: null
    }

    if(devicePartThree.dateOfIssueControl && devicePartThree.dateOfTurnInControl != null ){
      dto.dateOfIssue = new Date(new Date(devicePartThree.dateOfIssueControl).setHours(24)).toISOString().slice(0,10);
      dto.dateOfTurnIn =  new Date(new Date(devicePartThree.dateOfTurnInControl).setHours(24)).toISOString().slice(0,10);
    }

    if (dto.status == "I brug"){
      dto.requestValue = "Accepteret";
    }

    if (dto.status == "På lager"){
      dto.userId = null;
      dto.requestValue = "IkkeSendt";
    }

    await this.deviceService.createDevice(dto);
    this._snackBar.open('Enhed oprettet', 'Luk', {
      duration: 3000
    });
  }

  restartRegistration() {
    window.location.reload();
  }
}
