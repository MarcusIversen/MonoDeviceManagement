import {Component, OnInit, ViewChild} from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {MatPaginator} from "@angular/material/paginator";
import {MatSort} from "@angular/material/sort";
import {DeviceService} from "../../../services/device-service/device.service";
import {UserService} from "../../../services/user-service/user.service";
import {MatDialog} from "@angular/material/dialog";
import jwtDecode from "jwt-decode";
import {ReportErrorComponent} from "../report-error/report-error.component";
import {Token} from "../../../Models/Token";
import {Device} from "../../../Models/Interfaces/device";


@Component({
  selector: 'app-admin-device-overview',
  templateUrl: './device-overview.component.html',
  styleUrls: ['./device-overview.component.scss']
})
export class DeviceOverviewComponent implements OnInit{
  displayedColumns: string[] = ['id', 'deviceName', 'serialNumber', 'status', 'user', 'dateOfIssue', 'dateOfTurnIn', 'reportError'];
  dataSource: MatTableDataSource<Device>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  id: number;
  user: any;

  constructor(private deviceService: DeviceService, public userService: UserService, private popup: MatDialog) {
  }

  async ngOnInit() {
    let token = localStorage.getItem("token");
    if (token) {
      let decodedToken = jwtDecode(token) as Token;
      this.user = await this.userService.getUserByEmail(decodedToken.email);
      const devices = await this.deviceService.getDeviceOnUser(this.user.id);
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

  reportError(row) {
    this.popup.open(ReportErrorComponent,{
      data : {
        device : row
      }
    });
  }
}
