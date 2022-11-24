import {Component} from '@angular/core';
import {HttpService} from "../../services/http.service";
import {Router} from "@angular/router";
import jwtDecode from "jwt-decode";


class Token {
  role?: string;
}

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
      let decodedToken = jwtDecode(token) as Token;
      if (decodedToken.role == 'admin') {
        this.router.navigate(['/administrator']);
      } else if (decodedToken.role == 'teacher') {
        this.router.navigate(['/bruger']);
      }else if (decodedToken.role != 'teacher' || 'admin'){
        this.router.navigate(['/**'])
      }
    })

  }


}
