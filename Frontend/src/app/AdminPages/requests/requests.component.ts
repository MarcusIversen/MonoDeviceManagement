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
export class RequestsComponent implements OnInit {
  requests: Device[] = [];
  Users: User[] = [];


  constructor(private deviceService: DeviceService,
              private userService: UserService) {
  }

  async ngOnInit() {
    this.requests = await this.deviceService.getSendtRequestValue();
    await this.showRequester();
  }

  /**
   * Method for showing id of requester.
   */
  async showRequester() {
    for (const r of this.requests) {
      this.Users.push(await this.userService.getUserById(r.requesterId))
    }
  }

  /**
   * Method for declining a device request.
   * @param r
   */
  async decline(r: Device) {
    let device = await this.deviceService.getDeviceById(r.id);
    let dto = {
      id: device.id,
      deviceName: device.deviceName,
      serialNumber: device.serialNumber,
      status: device.status,
      userId: device.userId,
      requestValue: 'IkkeSendt',
      dateOfIssue: device.dateOfIssue,
      dateOfTurnIn: device.dateOfTurnIn,
      requesterId: null,
      errorSubject: device.errorSubject,
      errorDescription: device.errorDescription
    }

    await this.deviceService.updateDevice(dto, device.id);
    this.requests = this.requests.filter(d => d.id != device.id);
    window.location.reload();
    return this.requests;
  }

  /**
   * Method for accepting request for device.
   * @param r
   */
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
      dateOfIssue: new Date(new Date().setHours(24)).toISOString().slice(0,10),
      dateOfTurnIn: device.dateOfTurnIn,
      requesterId: null,
      errorSubject: device.errorSubject,
      errorDescription: device.errorDescription
    }

    await this.deviceService.updateDevice(dto, device.id);
    this.requests = this.requests.filter(d => d.id != device.id);
    window.location.reload();
    return this.requests;
  }
}
