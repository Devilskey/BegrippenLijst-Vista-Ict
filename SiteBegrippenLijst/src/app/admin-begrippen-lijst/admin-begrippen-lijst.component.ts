import { Component } from '@angular/core';
import { Concept } from 'src/Objects/Concept';
import { StaticVars } from '../Data/StaticVars';

@Component({
  selector: 'app-admin-begrippen-lijst',
  templateUrl: './admin-begrippen-lijst.component.html',
  styleUrls: ['./admin-begrippen-lijst.component.css']
})
export class AdminBegrippenLijstComponent {
  
  constructor(public staticVar: StaticVars) {}

  Concepts:Concept[] = [
    {Id: 0, Title: "PHP", DutchConcept: "yrds", EnglishConcept: "yrds"},
    {Id: 1, Title: "OOP", DutchConcept: "yrds", EnglishConcept: "yrds"},
    {Id: 2, Title: "CMS", DutchConcept: "yrds", EnglishConcept: "yrds"},
    {Id: 3, Title: "Solid", DutchConcept: "yrds", EnglishConcept: "yrds"}
   ];

   DeleteConcept(id:number){

   }

   EditConcept(id:number){

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
