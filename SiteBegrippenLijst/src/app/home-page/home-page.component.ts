import { Component, OnInit } from '@angular/core';
import { Concept } from 'src/Objects/Concept';
import { StaticVars } from '../Data/StaticVars';
import { ApiResponseConcepts } from 'src/Objects/ApiResponseConcepts';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit{
  Concepts:Concept[] = [];

  constructor(public staticVar: StaticVars, private Http:HttpClient) {}
  
  ngOnInit(): void {
    this.Http.get<ApiResponseConcepts>(`${StaticVars.Api}Concept/GetConcepts`).subscribe((ApiConcepts:ApiResponseConcepts) => {
       this.Concepts = JSON.parse(ApiConcepts.data);
       console.log(ApiConcepts.data);
    });
  }



   MoreData(id:number){
    console.log(`More Data ${id}`);
    let extraContent = document.getElementById(id.toString());
    if(extraContent){
      let stateElement = extraContent.style.display;
      if(stateElement === "block") 
      extraContent.style.display = 'none';
      else
      extraContent.style.display = 'block';
      
    }
   }
}
