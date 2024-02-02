import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { StaticVars } from '../Data/StaticVars';

@Component({
  selector: 'app-admin-account-page',
  templateUrl: './admin-account-page.component.html',
  styleUrls: ['./admin-account-page.component.css']
})
export class AdminAccountPageComponent {

  private authtoken:string= window.localStorage.getItem('Vista.BergrippenLijst.Token.Admin') ?? "";

  constructor(private Api:StaticVars, private Http:HttpClient) {}


  EmailData = {
    Email: "",
    EmailVerify: ""
  }

  PasswordData = {
    Password: "",
    PasswordVerify: ""
  }

  ChangeEmail()
  {
    const header = new HttpHeaders({
      Authorization: `Bearer ${this.authtoken}`,
    });

    this.Http.put(`${StaticVars.Api}User/ChangeEmail`, this.EmailData ,{headers: header}).subscribe();
  }

  ChangePassword()
  {    
    const header = new HttpHeaders({
      Authorization: `Bearer ${this.authtoken}`,
    });
    
    this.Http.put(`${StaticVars.Api}User/ChangePassword`, this.PasswordData ,{headers: header}).subscribe();
  }
}
