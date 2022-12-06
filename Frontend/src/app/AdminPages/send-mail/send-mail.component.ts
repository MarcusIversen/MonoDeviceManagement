import {Component, Inject, OnInit} from '@angular/core';
import {UserService} from "../../../services/user-service/user.service";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {UserOverviewComponent} from "../user-overview/user-overview.component";
import {FormControl, FormGroup} from "@angular/forms";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-send-mail',
  templateUrl: './send-mail.component.html',
  styleUrls: ['./send-mail.component.scss']
})
export class SendMailComponent implements OnInit{
  user: User;

  mailForm = new FormGroup({
    emailForm: new FormControl(this.data.user.email),
    subjectForm: new FormControl(''),
    bodyForm: new FormControl('')
  });


  constructor(private userService: UserService,  public dialogRef: MatDialogRef<UserOverviewComponent>, @Inject(MAT_DIALOG_DATA) public data : any, private _snackBar: MatSnackBar) {
  }

  async ngOnInit() {
    this.dialogRef.updateSize('50%', '80%');
    this.user = await this.userService.getUserById(this.data.user.id);
  }

  async sendMail() {
    const email = this.mailForm.value;
    let dto = {
      email: email.emailForm,
      subject: email.subjectForm,
      body: email.bodyForm
    }
    console.log(dto);
    await this.userService.sendMail(dto)
    this.dialogRef.close();
    this._snackBar.open('Mail sendt til: ' + dto.email, 'Luk', {
      duration: 3000
    });
  }



}

export interface User{
  email: string
}
