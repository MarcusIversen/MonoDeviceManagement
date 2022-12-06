import {Component, OnInit} from '@angular/core';
import {UserService} from "../../../services/user-service/user.service";
import jwtDecode from "jwt-decode";

@Component({
  selector: 'app-profile-info',
  templateUrl: './user-profile-info.component.html',
  styleUrls: ['./user-profile-info.component.scss']
})
export class UserProfileInfoComponent implements OnInit {
  profilePicture: any;
  email: string = "";
  firstName: string = "";
  lastName: string = "";
  workNumber: string = "";
  privateNumber: string = "";
  privateMail: string = "";
  id: number;
  user: any;
  saveChangesLoading: boolean | undefined;
  newPasswordLoading: boolean | undefined;
  showErrorMessage: boolean | undefined;
  newPassword: string = "";
  confirmPassword: string = "";
  showPasswordErrorMismatch: boolean;
  showPasswordErrorValidate: boolean;
  showPasswordChangeSuccess: boolean;

  constructor(private userService: UserService) {
  }

  async ngOnInit() {
    let token = localStorage.getItem("token");
    if (token) {
      let decodedToken = jwtDecode(token) as Token;
      this.user = await this.userService.getUserByEmail(decodedToken.email);
      this.id = this.user.id;
      this.firstName = this.user.firstName;
      this.lastName = this.user.lastName;
      this.workNumber = this.user.workNumber;
      this.email = this.user.email;
      this.privateMail = this.user.privateMail;
      this.privateNumber = this.user.privateNumber;
      this.profilePicture = this.user.profilePicture;
      console.log(this.user);
    }
  }

  selectFile({event}: { event: any }) {
    if (event.target.files) {
      var reader = new FileReader();
      reader.readAsDataURL(event.target.files[0]);
      reader.onload = (event: any) => {
        this.profilePicture = event.target.result;
      }
    }
  }


  async updatePassword() {
    if (this.newPassword == this.confirmPassword) {
      let dto = {
        id: this.id,
        firstName: this.firstName,
        lastName: this.lastName,
        workNumber: this.workNumber,
        profilePicture: this.profilePicture,
        email: this.email,
        privateNumber: this.privateNumber,
        privateMail: this.privateMail,
        password: this.confirmPassword
      }
      this.newPasswordLoading = true;
      this.showPasswordErrorMismatch = false;
      this.showPasswordErrorValidate = false;
      this.showPasswordChangeSuccess = false;

      await this.userService.updatePassword(this.id, dto)
        .then(() =>{
            this.newPasswordLoading = false;
            this.showPasswordChangeSuccess = true;
        }).catch(error => {
          this.showPasswordErrorValidate = true;
          this.newPasswordLoading = false;
        })
    } else {
      this.showPasswordErrorMismatch = true;
    }
  }

  async saveChanges() {
    let dto = {
      id: this.id,
      firstName: this.firstName,
      lastName: this.lastName,
      workNumber: this.workNumber,
      profilePicture: this.profilePicture,
      email: this.email,
      privateNumber: this.privateNumber,
      privateMail: this.privateMail
    }
    this.saveChangesLoading = true;

    await this.userService.updateUser(this.id, dto).then(() =>
      this.saveChangesLoading = false,
    ).catch(error => {
      this.showErrorMessage = true;
      this.saveChangesLoading = false;
    })
    window.location.reload();
  }
}

class Token {
  email?: string;
}
