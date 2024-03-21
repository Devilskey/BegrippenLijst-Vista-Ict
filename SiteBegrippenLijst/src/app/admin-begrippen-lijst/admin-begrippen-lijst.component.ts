import { Component, OnInit } from '@angular/core';
import { Concept } from 'src/Objects/Concept';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ApiResponseConcepts } from 'src/Objects/ApiResponseConcepts';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-admin-begrippen-lijst',
  templateUrl: './admin-begrippen-lijst.component.html',
  styleUrls: ['./admin-begrippen-lijst.component.css']
})
export class AdminBegrippenLijstComponent implements OnInit{

  UpdateConcepts: Concept = new Concept;
   

  NewConcept = {
    title: "",
    englishVersion: "",
    dutchVersion: "",
  }

  ModeEdit:boolean = false;

  Concepts!:Concept[];

  private authtoken:string= window.localStorage.getItem('Vista.BergrippenLijst.Token.Admin') ?? "";

  constructor( private Http:HttpClient) {}

  ngOnInit(): void {
    this.GetAllConcepts();
  }

  GetAllConcepts(){
    this.Http.get<ApiResponseConcepts>(`${environment.apiUrl}Concept/GetConcepts`).subscribe((ApiConcepts:ApiResponseConcepts) => {
      this.Concepts = JSON.parse(ApiConcepts.data);
   });
  }

   VisibleAddConcept(){
      this.ModeEdit = false;
      console.log( this.ModeEdit)

      let AddConcept = document.getElementById("VisibleAddConcept");
      let AddButton = document.getElementById("AddButton");

      if(AddConcept && AddButton){
        if(AddConcept.style.display == "flex"){
           AddConcept.style.display = "none";
           AddButton.innerHTML = "+";
        }
        else {
          AddConcept.style.display = "flex";
          AddButton.innerHTML = "-";
        }
      }
   }

   // Updates the concept list using the Edit concept api call
   UpdateConcept() {
    //Prepares the header
    const header = new HttpHeaders({
      Authorization: `Bearer ${this.authtoken}`,
    });

    // Gets the Concept data to update a concept with
    let UpdateConceptData = {
      id: this.UpdateConcepts.Id,
      title: this.UpdateConcepts.Title,
      englishVersion: this.UpdateConcepts.English_Version,
      dutchVersion: this.UpdateConcepts.Dutch_Version
    }

    this.Http.put(`${environment.apiUrl}Concept/EditConcept`, UpdateConceptData, { headers: header }).subscribe(() => {});

   }

   // Adds a concept 
   AddConcept(){
   //Prepares the header
    const header = new HttpHeaders({
      Authorization: `Bearer ${this.authtoken}`,
    });

    // POSTS the new concept data 
    this.Http.post(`${environment.apiUrl}Concept/AddConcept`, this.NewConcept, { headers: header }).subscribe(
      response => {
        if(response === 200){
           this.GetAllConcepts();
           this.NewConcept = {
            title: "",
            englishVersion: "",
            dutchVersion: "",
          }
        }
      }
    );
   }

   // Delets a concept using the delete concept api call
   DeleteConcept(id:number){
    // Prepares the header
    const header = new HttpHeaders({
      Authorization: `Bearer ${this.authtoken}`,
    });

    // Removes the concept by calling the api.
    this.Http.delete(`${environment.apiUrl}Concept/DeleteConcept?DeleteId=${id}`, { headers: header }).subscribe( response => {
      if(response === 200){
         this.GetAllConcepts();
      }
    });
   }

   //Switches the add concept panel between add concept en edit concept
   EditConcept(index:number){
    this.UpdateConcepts = this.Concepts[index]
    this.ModeEdit = true;
    
    // Gets the html element
    let AddConcept = document.getElementById("VisibleAddConcept");
    let AddButton = document.getElementById("AddButton");

    // Changes the html elements styling and inner html
      if(AddConcept && AddButton){
        if(AddConcept.style.display == "flex" && !this.ModeEdit){
          AddConcept.style.display = "none";
          AddButton.innerHTML = "+";
        }
        else {
          AddConcept.style.display = "flex";
          AddButton.innerHTML = "-";
        }
      }
   }

   // Unfolds a concept
   MoreData(id:number){

    //Gets the html element based on id
    let extraContent = document.getElementById(id.toString());

    // changes the concept display 
    if(extraContent){
      let stateElement = extraContent.style.display;
      if(stateElement === "block") 
      extraContent.style.display = 'none';
      else
      extraContent.style.display = 'block';
      
    }
   }
}