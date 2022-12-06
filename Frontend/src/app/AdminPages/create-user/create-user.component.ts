import {Component, Inject, OnInit} from '@angular/core';
import {FormControl, FormGroup} from "@angular/forms";
import {UserService} from "../../../services/user-service/user.service";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {UserOverviewComponent} from "../user-overview/user-overview.component";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.scss']
})
export class CreateUserComponent implements OnInit{
  firstName: string = '';
  lastName: string = '';
  email: string = '';
  password: string = '';
  workNumber: string = '';

  userForm = new FormGroup({
    firstNameForm: new FormControl(''),
    lastNameForm: new FormControl(''),
    emailForm: new FormControl(''),
    passwordForm: new FormControl(''),
    workNumberForm: new FormControl(''),
  });

  constructor(private userService: UserService, private _snackBar: MatSnackBar,  public dialogRef: MatDialogRef<UserOverviewComponent>, @Inject(MAT_DIALOG_DATA) public data : any) {
  }

  async ngOnInit() {

  }

  async createUserAsAdmin() {
    const user = this.userForm.value;
    let dto = {
      firstName: user.firstNameForm,
      lastName: user.lastNameForm,
      role: 'User',
      email: user.emailForm,
      password: user.passwordForm,
      workNumber: user.workNumberForm
    }
    await this.userService.createUserAsAdmin(dto)
    this._snackBar.open('Bruger oprettet', 'Luk', {
      duration: 3000
    });
    this.dialogRef.close();
  }

}

export interface User {
  firstName: string,
  lastName: string,
  email: string,
  password: string,
  workNumber: string
}
