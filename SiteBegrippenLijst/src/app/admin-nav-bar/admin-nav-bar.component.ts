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
    // when the page is loaded it checks if the user is logged in
    // the user is not logged in when the token is equal to ""
    if(window.localStorage.getItem("Vista.BergrippenLijst.Token.Admin") === ""){
      this.router.navigateByUrl("");
    }
  }

  sidebarState: string = 'out';

  // Toggels the sidebar between in and out
  toggleSidebar() {
    this.sidebarState = this.sidebarState === 'out' ? 'in' : 'out';
  }

}
