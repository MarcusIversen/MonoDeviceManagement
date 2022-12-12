import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from "@angular/router";
import {Observable} from "rxjs";
import jwtDecode from "jwt-decode";
import {Token} from "../../Models/Token";

@Injectable({
  providedIn: 'root'
})

export class UserAuthGuardService implements CanActivate{

  constructor(private router: Router) {
  }

  /**
   * Method for checking token for Role.
   * If you are User, you will have access to User functions.
   * @param route
   * @param state
   */
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    let token = localStorage.getItem('token');
    if(token) {
      let decodedToken = jwtDecode(token) as Token;
      let currentDate = new Date();
      if(decodedToken.exp) {
        let expiry = new Date(decodedToken.exp*1000);
        if(currentDate < expiry && decodedToken.role=='User') {
          return true;
        }
      }
    }
    this.router.navigate(['administrator']);
    return false;
  }
}
