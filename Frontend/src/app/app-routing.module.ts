import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {SideNavComponent} from "./side-nav/side-nav.component";
import {RouterModule, Routes} from "@angular/router";

const routes: Routes=[
  {path: '', component: SideNavComponent}
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
