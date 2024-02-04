import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-nav-bar',
  templateUrl: './admin-nav-bar.component.html',
  styleUrls: ['./admin-nav-bar.component.css']
})
export class AdminNavBarComponent implements OnInit{

  constructor(private router:Router) {}

  ngOnInit(): void {
    if(window.localStorage.getItem("Vista.BergrippenLijst.Token.Admin") === ""){
      this.router.navigateByUrl("");
    }
  }
  sidebarState: string = 'out';

  toggleSidebar() {
    console.log(this.sidebarState)
    this.sidebarState = this.sidebarState === 'out' ? 'in' : 'out';
  }

}
