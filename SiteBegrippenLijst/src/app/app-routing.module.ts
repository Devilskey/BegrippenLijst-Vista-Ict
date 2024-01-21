import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './home-page/home-page.component';
import { LoginPageComponent } from './login-page/login-page.component';
import { AdminBegrippenLijstComponent } from './admin-begrippen-lijst/admin-begrippen-lijst.component';
import { AdminAccountPageComponent } from './admin-account-page/admin-account-page.component';

const routes: Routes = [
  {path: "", component: HomePageComponent},
  {path: "Login", component: LoginPageComponent},
  {path: "Admin/BegrippenLijst", component: AdminBegrippenLijstComponent},
  {path: "Admin/Account", component: AdminAccountPageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
