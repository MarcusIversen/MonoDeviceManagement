import {Component, OnInit, ViewChild} from '@angular/core';
import {DeviceService} from "../../../services/device-service/device.service";
import {MatTableDataSource} from "@angular/material/table";
import {MatPaginator} from "@angular/material/paginator";
import {MatSort} from "@angular/material/sort";
import {UserService} from "../../../services/user-service/user.service";
import {MatDialog} from "@angular/material/dialog";
import {EditDeviceComponent} from "../edit-device/edit-device.component";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-admin-device-overview',
  templateUrl: './admin-device-overview.component.html',
  styleUrls: ['./admin-device-overview.component.scss']
})
export class AdminDeviceOverviewComponent implements OnInit{
  displayedColumns: string[] = ['id', 'deviceName', 'serialNumber', 'status', 'user', 'dateOfIssue', 'dateOfTurnIn', 'rediger'];
  dataSource: MatTableDataSource<Device>;
  user: any;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private deviceService: DeviceService, public userService: UserService, private popup: MatDialog, private _snackBar: MatSnackBar) {
  }

  async ngOnInit(){
    const devices = await this.deviceService.getDevices();
    this.dataSource = new MatTableDataSource(devices);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.user = await this.userService.getUserById(devices.userId); //TODO doesn't work
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  editDevice(row: any) {
    const data = this.popup.open(EditDeviceComponent, {
      data : {
        device : row
      }
    });
    data.afterClosed().subscribe(()=>{
       this.deviceService.getDevices().then(() => {
         this.dataSource.data = this.deviceService.devices;

         return this.dataSource.data;
       });
    });

  }

  async deleteDevice(row: any) {
    if (confirm('Vil du slette ' + row.deviceName + ' ' + row.serialNumber + '? Denne handling kan ikke fortrydes')) {
      const device = await this.deviceService.deleteDevice(row.id);
      this.dataSource.data = this.dataSource.data.filter(d => d.id != device.id);
      this._snackBar.open(row.deviceName + ' er blevet slettet', 'Luk', {
        duration: 3000
      });
    }
  }

  async getUserOnDevice(row: any){
    return await this.userService.getUserById(row.userId);
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
