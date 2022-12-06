import {Component, Input, OnInit} from '@angular/core';
import jwtDecode from "jwt-decode";
import {UserService} from "../../../services/user-service/user.service";
import {DeviceService} from "../../../services/device-service/device.service";

@Component({
  selector: 'app-requests',
  templateUrl: './requests.component.html',
  styleUrls: ['./requests.component.scss']
})
export class RequestsComponent implements OnInit {

  constructor() {
  }

  async ngOnInit() {

  }
}


export interface Device{
  id: number;
  deviceName: string,
  serialNumber: string,
  status: string,
  user?: string;
  userId?: number;
  dateOfIssue?: Date;
  dateOfTurnIn?: Date;
}

export interface User {
  id: number,
  firstName: string,
  lastName: string,
  email: string,
  password: string,
  workNumber: string,
  privateMail: string,
  privateNumber: string,
  profilePicture: string
}
