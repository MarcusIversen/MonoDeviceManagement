import {Component, OnInit, ViewChild} from '@angular/core';
import {Router} from "@angular/router";
import {MatSidenav} from "@angular/material/sidenav";
import {BreakpointObserver} from "@angular/cdk/layout";
import jwtDecode from "jwt-decode";
import {UserService} from "../../services/user-service/user.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {DeviceService} from "../../services/device-service/device.service";

@Component({
  selector: 'app-side-nav-admin',
  templateUrl: './side-nav-admin.component.html',
  styleUrls: ['./side-nav-admin.component.scss']
})
export class SideNavAdminComponent implements OnInit {

  profilePicture: any;
  private user: any;

  requests: any[] = [];

  @ViewChild(MatSidenav)
  sidenav!: MatSidenav;

  constructor(private router: Router, private observer: BreakpointObserver, public http: UserService, private snackBar: MatSnackBar, public deviceService: DeviceService) {
    let t = localStorage.getItem('token')
    if (t) {
      let decoded = jwtDecode(t) as any;
      this.http.firstName = decoded.firstName;
      this.http.lastName = decoded.lastName;
      if (decoded.role == "User") {
        this.http.role = "Lærer"
      } else {
        this.http.role = decoded.role;
      }
    }
  }

  async ngOnInit() {
    let t = localStorage.getItem('token')
    if (t) {
      let decoded = jwtDecode(t) as any;
      {
        this.user = await this.http.getUserByEmail(decoded.email)
        this.profilePicture = this.user.profilePicture;
      }
    }
    this.requests = await this.deviceService.getSendtRequestValue();
  }

  ngAfterViewInit() {
    this.observer.observe(['(max-width: 1500px)']).subscribe((res) => {
      if (res.matches) {
        this.sidenav.mode = 'over';
        this.sidenav.close();
      } else {
        this.sidenav.mode = 'side';
        this.sidenav.open();
      }
    });
  }

  logOut() {
    this.router.navigate(['']).then(() => {
      this.snackBar.open('Du er hermed logget ud', undefined, {duration: 3000})
      localStorage.clear();
      this.http.firstName = undefined;
      this.http.lastName = undefined;
      this.http.role = undefined;
    })
  }
}
