import {Component, OnInit, ViewChild} from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {Device} from "../../../Models/Interfaces/device";
import {MatPaginator} from "@angular/material/paginator";
import {MatSort} from "@angular/material/sort";
import {DeviceService} from "../../../services/device-service/device.service";
import {UserService} from "../../../services/user-service/user.service";
import {MatDialog} from "@angular/material/dialog";
import {MatSnackBar} from "@angular/material/snack-bar";
import {ViewReportComponent} from "../view-report/view-report.component";

@Component({
  selector: 'app-error-handling',
  templateUrl: './error-handling.component.html',
  styleUrls: ['./error-handling.component.scss']
})
export class ErrorHandlingComponent implements OnInit{
  displayedColumns: string[] = ['id', 'deviceName', 'serialNumber', 'status', 'user', 'viewReport'];
  dataSource: MatTableDataSource<Device>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private deviceService: DeviceService, public userService: UserService, private popup: MatDialog, private _snackBar: MatSnackBar) {
  }


  async ngOnInit() {
    const devices = await this.deviceService.getDevicesWithStatusMalfunctioned();
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


  viewReport(row) {
    this.popup.open(ViewReportComponent,{
      data : {
        device : row
      }
    });
  }
}
