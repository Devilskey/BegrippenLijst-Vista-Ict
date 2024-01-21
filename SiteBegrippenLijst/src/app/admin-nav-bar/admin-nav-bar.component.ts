import { Component } from '@angular/core';

@Component({
  selector: 'app-admin-nav-bar',
  templateUrl: './admin-nav-bar.component.html',
  styleUrls: ['./admin-nav-bar.component.css']
})
export class AdminNavBarComponent {
  sidebarState: string = 'out';

  toggleSidebar() {
    console.log(this.sidebarState)
    this.sidebarState = this.sidebarState === 'out' ? 'in' : 'out';
  }

}
