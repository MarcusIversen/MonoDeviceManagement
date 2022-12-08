import {Component, OnInit} from '@angular/core';
import {User} from "../../../Models/Interfaces/user";
import {Device} from "../../../Models/Interfaces/device";
import {DeviceService} from "../../../services/device-service/device.service";
import {UserService} from "../../../services/user-service/user.service";

@Component({
  selector: 'app-requests',
  templateUrl: './requests.component.html',
  styleUrls: ['./requests.component.scss']
})
export class RequestsComponent implements OnInit{
  requests: Device[] = [];
  Users: User[] = [];



  constructor(private deviceService: DeviceService, private userService: UserService) {

  }

  async ngOnInit(){
    this.requests = await this.deviceService.getSendtRequestValue();
    this.showRequester();
  }

  async showRequester(){
    for (const r of this.requests) {
      this.Users.push(await this.userService.getUserById(r.requesterId))
    }
  }


  async decline(r: Device) {
    let device = await this.deviceService.getDeviceById(r.id);
    let dto = {
      id: device.id,
      deviceName: device.deviceName,
      serialNumber: device.serialNumber,
      status: device.status,
      userId: device.userId,
      requestValue: 'IkkeSendt',
      dateOfIssue: new Date(new Date(device.dateOfIssue).setHours(24)).toISOString().slice(0, 10),
      dateOfTurnIn: new Date(new Date(device.dateOfTurnIn).setHours(24)).toISOString().slice(0, 10),
      requesterId: null
    }

    await this.deviceService.updateDevice(dto, device.id);
    this.requests = this.requests.filter(d => d.id != device.id);
    return this.requests;
  }

  async accept(r: Device) {
    let device = await this.deviceService.getDeviceById(r.id);
    let requester = await this.userService.getUserById(r.requesterId);
    let dto = {
      id: device.id,
      deviceName: device.deviceName,
      serialNumber: device.serialNumber,
      status: device.status,
      userId: requester.id,
      requestValue: 'Accepteret',
      dateOfIssue: new Date(new Date(device.dateOfIssue).setHours(24)).toISOString().slice(0, 10),
      dateOfTurnIn: new Date(new Date(device.dateOfTurnIn).setHours(24)).toISOString().slice(0, 10),
      requesterId: null
    }

    await this.deviceService.updateDevice(dto, device.id);
    this.requests = this.requests.filter(d => d.id != device.id);
    return this.requests;
  }
}
