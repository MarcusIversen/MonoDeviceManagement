import {Component, OnInit, ViewChild} from '@angular/core';
import {DeviceService} from "../../../services/device-service/device.service";
import {UserService} from "../../../services/user-service/user.service";
import {MatTableDataSource} from "@angular/material/table";
import {Device} from "../../AdminPages/admin-device-overview/admin-device-overview.component";
import {MatPaginator} from "@angular/material/paginator";
import {MatSort} from "@angular/material/sort";
import jwtDecode from "jwt-decode";

@Component({
  selector: 'app-request-device-overview',
  templateUrl: './request-device-overview.component.html',
  styleUrls: ['./request-device-overview.component.scss']
})
export class RequestDeviceOverviewComponent implements OnInit{
  displayedColumns: string[] = ['id', 'deviceName', 'serialNumber', 'status', 'dateOfIssue', 'dateOfTurnIn', 'request'];
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
      const devices = await this.deviceService.getNotAssignedDevices();
      this.dataSource = new MatTableDataSource(devices);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    }
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  requestDevice(row) {

  }
}

class Token {
  email?: string;
}
