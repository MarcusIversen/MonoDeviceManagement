import {Component} from '@angular/core';
import {Router} from "@angular/router";
import jwtDecode from "jwt-decode";
import {UserService} from "../../services/user-service/user.service";
import {Token} from "../../Models/Token";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  email: any;
  password: any;
  firstName: any;
  lastName: any;
  isLoading: boolean | undefined;
  showErrorMessage: boolean | undefined;


  constructor(private userService: UserService, private router: Router) {
  }

  /**
   * Method for logging in.
   */
  async login() {
    this.isLoading = true;

    let dto = {
      email: this.email,
      password: this.password,
      firstName: this.firstName,
      lastName: this.lastName
    }

    this.userService.login(dto).then(token => {
        this.showErrorMessage = false;
        console.log(token);
        localStorage.setItem('token', token)
        let decodedToken = jwtDecode(token) as Token;
        if (decodedToken.role == 'Admin') {
          this.router.navigate(['/administrator/enheder']);
          this.isLoading = false;
        } else if (decodedToken.role == 'User') {
          this.router.navigate(['/bruger/enheder']);
          this.isLoading = false;
        } else if (decodedToken.role != 'User' || 'Admin') {
          this.router.navigate(['/**'])
          this.isLoading = false;
        }
      }, (error) => {
        this.showErrorMessage = true;
        this.isLoading = false;
      }
    )
  }

  async NavigateToRegister(){
    this.isLoading = true;
    await this.router.navigate(['/registrer'])
  }


}
