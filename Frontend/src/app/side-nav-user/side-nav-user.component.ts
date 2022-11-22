import {Component, ViewChild} from '@angular/core';
import {MatSidenav} from "@angular/material/sidenav";
import {Router} from "@angular/router";
import {BreakpointObserver} from "@angular/cdk/layout";

@Component({
  selector: 'app-side-nav-user',
  templateUrl: './side-nav-user.component.html',
  styleUrls: ['./side-nav-user.component.scss']
})
export class SideNavUserComponent {

  @ViewChild(MatSidenav)
  sidenav!: MatSidenav;

  constructor(private router: Router, private observer: BreakpointObserver) {}

  ngAfterViewInit(){
    this.observer.observe(['(max-width: 1500px)']).subscribe((res)=> {
      if(res.matches){
        this.sidenav.mode = 'over';
        this.sidenav.close();
      }else {
        this.sidenav.mode = 'side';
        this.sidenav.open();
      }
    });
  }

}
