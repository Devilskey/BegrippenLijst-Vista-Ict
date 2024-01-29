import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class StaticVars {
    Lang:string = "NL";
    static Api:string = "https://localhost:7220/"
    Token:string = "";
}