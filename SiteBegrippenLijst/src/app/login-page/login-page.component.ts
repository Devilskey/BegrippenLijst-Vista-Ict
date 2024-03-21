import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';


@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent {
  Email:string = "";
  Password:string = "";

  constructor(private router:Router, private Http:HttpClient) {}

  // Routes the user back to the homepage
  BackToHomePage(){
    this.router.navigateByUrl("/");
  }

  // Logs the user in 
  Login(){

    //Sets the token errorcode en data variabel
    let token = "";
    let ErrorCode ="";
    let data = {Email:this.Email, Password:this.Password};

    // Does a http post and waiting 
    this.Http.post(`${environment.apiUrl}User/Login`, data , { responseType: 'text' }).subscribe((Token:string) => {
      // checks if the token is actualy a token and not an error code.
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
          token = Token;
          break;
      }
      
      // if the token not equal to error or empty then log the user in and set the token expire date.
      if(token != "Error" && token != "") {
        let DateTokenExpire = new Date();
        DateTokenExpire.setHours(DateTokenExpire.getHours() + 8)

        window.localStorage.setItem('Vista.BegrippenLijst.Token.ExpireDate', DateTokenExpire.toDateString())

        window.localStorage.setItem('Vista.BergrippenLijst.Token.Admin', token)
        this.router.navigateByUrl("/Admin/BegrippenLijst");
      }
    } );
  }
}
