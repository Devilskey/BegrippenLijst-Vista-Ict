import { Component, Input } from '@angular/core';
import { trigger, state, style, animate, transition } from '@angular/animations';

@Component({
  selector: 'app-admin-side-nav-bar',
  templateUrl: './admin-side-nav-bar.component.html',
  styleUrls: ['./admin-side-nav-bar.component.css'],
  animations: [
    trigger('slideInOut', [
      state('in', style({
        left: '0',
      })),
      state('out', style({
        left: '-250px',
      })),
      transition('in => out', animate('300ms ease-in-out')),
      transition('out => in', animate('300ms ease-in-out')),
    ]),
  ],
})
export class AdminSideNavBarComponent {
  @Input() sidebarState: string = 'out';

  toggleSidebar() {
    this.sidebarState = this.sidebarState === 'out' ? 'in' : 'out';
  }
}
