import {Component, Input, OnInit} from '@angular/core';
import {User} from "../../../Models/Interfaces/user";
import {Device} from "../../../Models/Interfaces/device";
import {DeviceService} from "../../../services/device-service/device.service";

@Component({
  selector: 'app-requests',
  templateUrl: './requests.component.html',
  styleUrls: ['./requests.component.scss']
})
export class RequestsComponent implements OnInit{
  @Input() user: User;
  requests: Device[] = []


  constructor(private deviceService: DeviceService) {
  }

  async ngOnInit(){
    this.requests = await this.deviceService.getSendtRequestValue();
  }

}
