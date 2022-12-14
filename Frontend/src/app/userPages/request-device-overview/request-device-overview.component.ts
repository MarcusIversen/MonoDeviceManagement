import {Component, OnInit, ViewChild} from '@angular/core';
import {DeviceService} from "../../../services/device-service/device.service";
import {UserService} from "../../../services/user-service/user.service";
import {MatTableDataSource} from "@angular/material/table";
import {MatPaginator} from "@angular/material/paginator";
import {MatSort} from "@angular/material/sort";
import jwtDecode from "jwt-decode";
import {Device} from "../../../Models/Interfaces/device";
import {Token} from "../../../Models/Token";

@Component({
  selector: 'app-request-device-overview',
  templateUrl: './request-device-overview.component.html',
  styleUrls: ['./request-device-overview.component.scss']
})
export class RequestDeviceOverviewComponent implements OnInit{
  displayedColumns: string[] = ['id', 'deviceName', 'serialNumber', 'status', 'request'];
  dataSource: MatTableDataSource<Device>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  id: number;
  user: any;

  constructor(private deviceService: DeviceService, public userService: UserService) {
  }


  async ngOnInit() {
    let token = localStorage.getItem("token");
    if (token) {
      let decodedToken = jwtDecode(token) as Token;
      this.user = await this.userService.getUserByEmail(decodedToken.email);
      const devices = await this.deviceService.getIkkeSendtRequestValue();
      this.dataSource = new MatTableDataSource(devices);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    }
  }

  /**
   * Method for searching in the request-device table.
   * @param event
   */
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  /**
   * Method for request button, making a request for a device.
   * @param row
   */
  async requestDevice(row: any) {
    if (confirm('Vil du forespørge' + row.deviceName)) {
      let device = await this.deviceService.getDeviceById(row.id);

        let dto = {
          id: device.id,
          deviceName: device.deviceName,
          serialNumber: device.serialNumber,
          status: device.status,
          userId: device.userId,
          requestValue: new String('Sendt'),
          requesterId: this.user.id,
          dateOfIssue: device.dateOfIssue,
          dateOfTurnIn: device.dateOfTurnIn,
          errorSubject: device.errorSubject,
          errorDescription: device.errorDescription
        }

        console.log(dto.requesterId);

      await this.deviceService.updateDevice(dto, device.id);
      const devices = await this.deviceService.getIkkeSendtRequestValue();
      this.dataSource = new MatTableDataSource(devices);
    }
  }
}
