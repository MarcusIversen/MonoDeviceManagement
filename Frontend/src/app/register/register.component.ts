import {Component} from '@angular/core';
import {HttpService} from "../../services/http.service";
import {Router} from "@angular/router";
import jwtDecode from "jwt-decode";
import {error} from "@angular/compiler-cli/src/transformers/util";

class Token {
  role?: string;
}

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


  constructor(private http: HttpService, private router: Router) {
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

    this.http.register(dto).then(token => {
      this.isLoading = false
      this.showErrorMessage = false;
      this.showSuccessMessage = true;
      console.log(token);
      localStorage.setItem('token', token)
    },(error) =>{
      this.isLoading = false;
      this.showErrorMessage = true;
      this.showSuccessMessage = false;
    })
  }
}
