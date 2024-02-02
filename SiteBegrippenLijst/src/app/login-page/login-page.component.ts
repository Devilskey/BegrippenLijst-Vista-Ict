import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { StaticVars } from '../Data/StaticVars';


@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent {
  Email:string = "";
  Password:string = "";

  constructor(private router:Router, private Http:HttpClient) {}

  BackToHomePage(){
    this.router.navigateByUrl("/");
  }

  Login(){
    let token = "";
    let ErrorCode ="";
    let data = {Email:this.Email, Password:this.Password};
    this.Http.post(`${StaticVars.Api}User/Login`, data , { responseType: 'text' }).subscribe((Token:string) => {
      switch(Token){
        case "Empty String detected code 500":
          token = "Error"
          break;
         case "Error No user found with this email adress.":
          ErrorCode = "No account with this email";
          token = "Error"
          break;
         case "Password Not Valid": 
          ErrorCode = "Password Incorrect"
          token = "Error"
          break;
        default:
          console.log(Token);
          token = Token;
          break;
      }
      if(token != "Error" && token != "") {
        window.localStorage.setItem('Vista.BergrippenLijst.Token.Admin', token)
        this.router.navigateByUrl("/Admin/BegrippenLijst");
      }
    } );
  }
}
