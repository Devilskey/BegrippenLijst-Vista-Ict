import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-admin-account-page',
  templateUrl: './admin-account-page.component.html',
  styleUrls: ['./admin-account-page.component.css']
})
export class AdminAccountPageComponent {

  // gets the authtoken from the local storage
  private authtoken:string= window.localStorage.getItem('Vista.BergrippenLijst.Token.Admin') ?? "";

  constructor(private Http:HttpClient) {}

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
    //Prepares the header for the api call
    const header = new HttpHeaders({
      Authorization: `Bearer ${this.authtoken}`,
    });
    // uses the http.put to change the email adress using the api
    this.Http.put(`${environment.apiUrl}User/ChangeEmail`, this.EmailData ,{headers: header}).subscribe();
  }

  ChangePassword()
  {        
    //Prepares the header for the api call
    const header = new HttpHeaders({
      Authorization: `Bearer ${this.authtoken}`,
    });
        // uses the http.put to change the email adress using the api

    this.Http.put(`${environment.apiUrl}User/ChangePassword`, this.PasswordData ,{headers: header}).subscribe();
  }
}
