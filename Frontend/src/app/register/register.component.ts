import { Component } from '@angular/core';
import {HttpService} from "../../services/http.service";
import {Router} from "@angular/router";
import jwtDecode from "jwt-decode";

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
      let decodedToken = jwtDecode(token) as Token;
      if (decodedToken.role == 'admin') {
        this.router.navigate(['/administrator/enheder']);
      } else if (decodedToken.role == 'teacher') {
        this.router.navigate(['/bruger/enheder']);
      }else if (decodedToken.role != 'teacher' || 'admin'){
        this.router.navigate(['/**'])
      }
    })

  }
}
