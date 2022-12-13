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
export class ErrorHandlingComponent implements OnInit {
  displayedColumns: string[] = ['id', 'deviceName', 'serialNumber', 'status', 'user', 'viewReport', 'changeStatus', 'statusIcon'];
  dataSource: MatTableDataSource<Device>;


  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private deviceService: DeviceService,
              private userService: UserService,
              private popup: MatDialog,
              private _snackBar: MatSnackBar) {
  }



  async ngOnInit() {
    const devices = await this.deviceService.getDevicesWithStatusMalfunctioned();
    console.log(devices);
    this.dataSource = new MatTableDataSource(devices);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  /**
   * Method for searching in the error-report table.
   */
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }


  /**
   * Method for viewReport button, opens component for report.
   * @param row
   */
  viewReport(row) {
    this.popup.open(ViewReportComponent, {
      data: {
        device: row
      }
    });
  }

  /**
   * Method for changing status of a reported error.
   * @param row
   * @param text
   */
  async changeStatus(row: any, text: string) {
    row.status = text;
    let device = await this.deviceService.getDeviceById(row.id);
    let dto = {
      id: device.id,
      deviceName: device.deviceName,
      serialNumber: device.serialNumber,
      status: text,
      userId: device.userId,
      requestValue: device.requestValue,
      dateOfTurnIn: device.dateOfTurnIn,
      dateOfIssue: device.dateOfIssue,
      errorSubject: device.errorSubject,
      errorDescription: device.errorDescription
    }

    if(dto.status == "I brug" || dto.status == "På lager"){
      dto.errorSubject = '';
      dto.errorDescription = '';
    }

    await this.deviceService.updateDevice(dto, device.id);
  }

  /**
   * Method for reloading page, which updates table.
   */
  reload() {
    window.location.reload();
  }
}
