import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class StaticVars {
    Lang:string = "NL";
    static Api:string = "https://192.168.2.13:32768/"
    Token:string = "";
}