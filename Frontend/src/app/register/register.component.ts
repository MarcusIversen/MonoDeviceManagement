import {Component} from '@angular/core';
import {Router} from "@angular/router";
import {UserService} from "../../services/user-service/user.service";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  email: any;
  password: any;
  firstName: any;
  lastName: any;
  workNumber: any;

  showErrorMessage: boolean | undefined;
  isLoading: boolean | undefined;

  showSuccessMessage: boolean | undefined;


  constructor(private userService: UserService) {
  }

  async register() {
    this.isLoading = true;
    let dto = {
      firstName: this.firstName,
      lastName: this.lastName,
      workNumber: this.workNumber,
      email: this.email,
      password: this.password,
      role: 'User'
    }

    this.userService.register(dto).then(token => {
      this.isLoading = false
      this.showErrorMessage = false;
      this.showSuccessMessage = true;
      console.log(token);
      localStorage.setItem('token', token)
    }, (error) => {
      this.isLoading = false;
      this.showErrorMessage = true;
      this.showSuccessMessage = false;
    })
  }
}
