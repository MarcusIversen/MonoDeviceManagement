import { Component } from '@angular/core';
import {HttpService} from "../../services/http.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  email: any;
  password: any;


  constructor(private http: HttpService, private router: Router) {
  }

  async login() {
    let dto = {
      email: this.email,
      password: this.password
    }
    this.http.login(dto).then(token => {
      console.log(token);
      localStorage.setItem('token', token)
      this.router.navigate(['/administrator']);
    })

  }
}
