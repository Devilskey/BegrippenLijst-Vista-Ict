import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { HomePageComponent } from './home-page/home-page.component';
import { LoginPageComponent } from './login-page/login-page.component';
import { FormsModule } from '@angular/forms';
import { AdminBegrippenLijstComponent } from './admin-begrippen-lijst/admin-begrippen-lijst.component';
import { AdminNavBarComponent } from './admin-nav-bar/admin-nav-bar.component';
import { AdminSideNavBarComponent } from './admin-side-nav-bar/admin-side-nav-bar.component';
import { AdminAccountPageComponent } from './admin-account-page/admin-account-page.component';
import { StaticVars } from './Data/StaticVars';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';



@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    HomePageComponent,
    LoginPageComponent,
    AdminBegrippenLijstComponent,
    AdminNavBarComponent,
    AdminSideNavBarComponent,
    AdminAccountPageComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [StaticVars],
  bootstrap: [AppComponent]
})
export class AppModule { }
