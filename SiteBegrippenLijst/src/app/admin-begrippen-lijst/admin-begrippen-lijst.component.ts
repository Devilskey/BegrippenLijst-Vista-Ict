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

   UpdateConcept() {
    const header = new HttpHeaders({
      Authorization: `Bearer ${this.authtoken}`,
    });

    let UpdateConceptData = {
      id: this.UpdateConcepts.Id,
      title: this.UpdateConcepts.Title,
      englishVersion: this.UpdateConcepts.English_Version,
      dutchVersion: this.UpdateConcepts.Dutch_Version
    }

    this.Http.put(`${environment.apiUrl}Concept/EditConcept`, UpdateConceptData, { headers: header }).subscribe(() => {});

   }

   AddConcept(){
    const header = new HttpHeaders({
      Authorization: `Bearer ${this.authtoken}`,
    });

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

   DeleteConcept(id:number){

    const header = new HttpHeaders({
      Authorization: `Bearer ${this.authtoken}`,
    });

    this.Http.delete(`${environment.apiUrl}Concept/DeleteConcept?DeleteId=${id}`, { headers: header }).subscribe( response => {
      if(response === 200){
         this.GetAllConcepts();
      }
    });
   }

   EditConcept(index:number){
    this.UpdateConcepts = this.Concepts[index]
    this.ModeEdit = true;
    
    let AddConcept = document.getElementById("VisibleAddConcept");
    let AddButton = document.getElementById("AddButton");

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


   MoreData(id:number){
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