import {Component, OnInit, ViewChild} from '@angular/core';
import {DeviceService} from "../../../services/device-service/device.service";
import {MatTable, MatTableDataSource} from "@angular/material/table";
import {MatPaginator} from "@angular/material/paginator";
import {MatSort} from "@angular/material/sort";
import {UserService} from "../../../services/user-service/user.service";

@Component({
  selector: 'app-admin-device-overview',
  templateUrl: './admin-device-overview.component.html',
  styleUrls: ['./admin-device-overview.component.scss']
})
export class AdminDeviceOverviewComponent implements OnInit{
  displayedColumns: string[] = ['id', 'deviceName', 'serialNumber', 'status', 'user', 'dateOfIssue', 'dateOfTurnIn', 'rediger'];
  dataSource: MatTableDataSource<Device>;
  table = new MatTableDataSource<Device>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private deviceService: DeviceService, public userService: UserService) {
  }

  async ngOnInit(){
    const devices = await this.deviceService.getDevices();
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

  editDevice(row: any) {


  }

  async deleteDevice(row: any) {
    const device = await this.deviceService.deleteDevice(row.id);
    this.dataSource.data = this.dataSource.data.filter(d => d.id != device.id);
    return this.dataSource.data;
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
