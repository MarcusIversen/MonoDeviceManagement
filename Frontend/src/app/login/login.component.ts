import { Component } from '@angular/core';
import {HttpService} from "../../services/http.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  email: any;
  password: any;


  constructor(private http: HttpService) {
  }

  async login() {
    let dto = {
      email: this.email,
      password: this.password
    }
    var token = await this.http.login(dto)
    localStorage.setItem('token', token)
  }
}
