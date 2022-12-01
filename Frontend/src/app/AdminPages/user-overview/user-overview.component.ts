import {Component, OnInit, ViewChild} from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {MatPaginator} from "@angular/material/paginator";
import {MatSort} from "@angular/material/sort";
import {UserService} from "../../../services/user-service/user.service";
import {animate, state, style, transition, trigger} from "@angular/animations";
import {DeviceService} from "../../../services/device-service/device.service";

@Component({
  selector: 'app-user-overview',
  templateUrl: './user-overview.component.html',
  styleUrls: ['./user-overview.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class UserOverviewComponent implements OnInit{
  displayedColumns: string[] = ['id', 'email', 'firstName', 'lastName', 'role', 'workNumber', 'privateNumber', 'privateMail'];
  dataSource: MatTableDataSource<User>;
  columnsToDisplayWithExpand = [...this.displayedColumns, 'expand'];
  expandedUser: User;
  assignedDevices: any[] = [];

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private userService: UserService, public deviceService: DeviceService) {
  }

  async ngOnInit(){
    const users = await this.userService.getUsersTypeUser();
    this.dataSource = new MatTableDataSource(users);
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

  async getDeviceOnUser(id: number){
    return await this.deviceService.getDeviceOnUser(id);
  }

  //TODO style enheder
  async onSelect(row: any) {
    this.assignedDevices = await this.getDeviceOnUser(row.id);
    return this.assignedDevices;
  }
}

//Der skal også være mat dialog

export interface User{
  id: number,
  email: string,
  firstName: string,
  lastName: string,
  role: string,
  workNumber: string,
  privateNumber?: string,
  privateMail?: string
}
