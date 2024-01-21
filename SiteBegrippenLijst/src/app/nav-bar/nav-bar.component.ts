import { ChangeDetectorRef, Component } from '@angular/core';
import { StaticVars } from '../Data/StaticVars';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent {
  constructor(public staticVar: StaticVars) {}

  ToNL(){
    this.staticVar.Lang = "NL";
    console.log(this.staticVar.Lang)
  }

  ToEN(){
    this.staticVar.Lang = "EN";
    console.log(this.staticVar.Lang)
  }
}
