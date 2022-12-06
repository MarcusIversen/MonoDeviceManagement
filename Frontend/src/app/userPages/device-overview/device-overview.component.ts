import {Component, EventEmitter, ViewChild} from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {MatPaginator} from "@angular/material/paginator";
import {MatSort} from "@angular/material/sort";
import {DeviceService} from "../../../services/device-service/device.service";
import {UserService} from "../../../services/user-service/user.service";
import jwtDecode from "jwt-decode";

@Component({
  selector: 'app-device-overview',
  templateUrl: './device-overview.component.html',
  styleUrls: ['./device-overview.component.scss']
})
export class DeviceOverviewComponent {
  displayedColumns: string[] = ['id', 'deviceName', 'serialNumber', 'status', 'request'];
  dataSource: MatTableDataSource<Device>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private deviceService: DeviceService, public userService: UserService) {
  }

  async ngOnInit() {
    const devices = await this.deviceService.getNotAssignedDevices();
    this.dataSource = new MatTableDataSource(devices);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  async request(row: any) {
    if (confirm('Vil du lave en forespørgsel om tildeling af: ' + row.deviceName)) {
      sessionStorage.setItem('device', row.id);
      let token = localStorage.getItem("token");
      if (token) {
        let decodedToken = jwtDecode(token) as Token;
        let user = await this.userService.getUserByEmail(decodedToken.email);
        sessionStorage.setItem('user', user.id);
      }
    }
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

class Token {
  email?: string;
}
