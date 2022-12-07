import {Component, Inject, OnInit} from '@angular/core';
import {User} from "../../../Models/Interfaces/user";
import {UserService} from "../../../services/user-service/user.service";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {DeviceService} from "../../../services/device-service/device.service";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {MatSnackBar} from "@angular/material/snack-bar";
import {UserOverviewComponent} from "../user-overview/user-overview.component";
import {CreateUserComponent} from "../create-user/create-user.component";

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.scss']
})
export class EditUserComponent implements OnInit {

  user: User;
  isLoading: boolean;

  editUser = new FormGroup({
    id: new FormControl(this.data.user.id),
    email: new FormControl(this.data.user.email),
    firstName: new FormControl(this.data.user.firstName),
    lastName: new FormControl(this.data.user.lastName),
    workNumber: new FormControl(this.data.user.workNumber),
    privateNumber: new FormControl(this.data.user.privateNumber),
    privateMail: new FormControl(this.data.user.privateMail)
  })

  constructor(private deviceService: DeviceService,
              public userService: UserService,
              public dialogRef: MatDialogRef<CreateUserComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any,
              private _snackBar: MatSnackBar) {
  }

  async ngOnInit() {
    this.dialogRef.updateSize("550px", "522px");
    this.user = await this.userService.getUserById(this.data.user.id);
    this.editUser.patchValue({
      id: this.user.id,
      email: this.user.email,
      firstName: this.user.firstName,
      lastName: this.user.lastName,
      workNumber: this.user.workNumber,
      privateMail: this.user.privateMail,
      privateNumber: this.user.privateNumber
    });
    this.isLoading = false;
  }

  async save() {
    const user = this.editUser.value;
    let dto = {
      id: user.id,
      email: user.email,
      firstName: user.firstName,
      lastName: user.lastName,
      workNumber: user.workNumber,
      privateMail: user.privateMail,
      privateNumber: user.privateNumber
    }

    await this.userService.updateUser(user.id, dto)
      .then(() => {
        this.isLoading = true;
        this.dialogRef.close();
      }).catch(error => {
        this._snackBar.open('Redigering af bruger fejlede', 'Luk', {
          duration: 3000
        });
      })
    this._snackBar.open('Den valgte bruger er nu redigeret', 'Luk', {
      duration: 3000
    });
  }
}
