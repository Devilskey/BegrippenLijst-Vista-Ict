import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent {
  Email:string = "";
  Password:string = "";

  constructor(private router:Router) {

  }

  BackToHomePage(){
    this.router.navigateByUrl("/");

  }

  Login(){
    console.log(`${this.Email} : ${this.Password}`);

    if(true === true) {
      this.router.navigateByUrl("/Admin/BegrippenLijst");
    }
  }

}
