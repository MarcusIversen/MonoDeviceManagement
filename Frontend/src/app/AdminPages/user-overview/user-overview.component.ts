import {Component, OnInit, ViewChild} from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {MatPaginator} from "@angular/material/paginator";
import {MatSort} from "@angular/material/sort";
import {UserService} from "../../../services/user-service/user.service";
import {animate, state, style, transition, trigger} from "@angular/animations";
import {DeviceService} from "../../../services/device-service/device.service";
import {MatDialog} from "@angular/material/dialog";
import {SendMailComponent} from "../send-mail/send-mail.component";
import {CreateUserComponent} from "../create-user/create-user.component";
import {MatSnackBar} from "@angular/material/snack-bar";

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
  displayedColumns: string[] = ['id', 'email', 'firstName', 'lastName', 'role', 'workNumber', 'privateNumber', 'privateMail', 'rediger'];
  dataSource: MatTableDataSource<User>;
  columnsToDisplayWithExpand = [...this.displayedColumns, 'expand'];
  expandedUser: User;
  assignedDevices: any[] = [];

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private userService: UserService, public deviceService: DeviceService, private popup: MatDialog, private _snackBar: MatSnackBar) {
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

  editUser(row: any) {

    
  }

  async sendMail(row: any) {
    this.popup.open(SendMailComponent,{
      data : {
        user : row
      }
    });
  }

  async deleteUser(row: any) {
    if (confirm('Vil du slette ' + row.firstName + ' ' + row.lastName + '? Denne handling kan ikke fortrydes')) {
      const user = await this.userService.deleteUser(row.id);
      this.dataSource.data = this.dataSource.data.filter(u => u.id != user.id);
      this._snackBar.open('Bruger slettet', 'Luk', {
        duration: 3000
      });
    }
  }

  async createUser() {
    const data = this.popup.open(CreateUserComponent);
    data.afterClosed().subscribe(()=>{
      this.userService.getUsersTypeUser().then(() => {
        this.dataSource.data = this.userService.getRoleUsers;
        this._snackBar.open('Bruger oprettet', 'Luk', {
          duration: 3000
        });
        return this.dataSource.data;
      });
    });
  }

}

export interface User {
  id: number,
  email: string,
  firstName: string,
  lastName: string,
  role: string,
  workNumber: string,
  privateNumber?: string,
  privateMail?: string
}
