import { Component, OnInit } from '@angular/core';
import { Concept } from 'src/Objects/Concept';
import { StaticVars } from '../Data/StaticVars';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent {
  constructor(public staticVar: StaticVars) {}


  Concepts:Concept[] = [
    {Id: 0, Title: "PHP", DutchConcept: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas maximus elementum risus, non sollicitudin tortor faucibus in. Praesent et enim porta, bibendum mi et, rutrum ipsum. ", EnglishConcept: "d"},
    {Id: 1, Title: "OOP", DutchConcept: "t nec tellus mauris. In hac habitasse platea dictumst. Donec et laoreet massa, in varius justo. Phasellus suscipit lorem sed dolor congue, sit amet suscipit neque dictum. Aenean quis quam mollis, iaculis ante vitae, pulvinar metus. Nulla vehicula sem eget nibh finibus, sed vulputate ligula luctus. Vestibulum dolor dolor, viverra varius augue congue, blandit blandit eros. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos", EnglishConcept: "Etiam felis nulla, ornare a eros a, lobortis maximus ipsum. Nunc fermentum suscipit auctor. Integer tincidunt vestibulum felis id porta. Nullam volutpat, ligula ut consectetur rutrum, turpis ipsum condimentum diam, sit amet semper dolor est a magna. Praesent sed blandit ante, id blandit enim. Suspendisse quis arcu nec nunc fermentum blandit. Sed maximus sem at enim bibendum molestie. Nulla fringilla augue dui, id ultricies arcu ullamcorper sed. Maecenas luctus ante eros, a tincidunt neque tempus sit amet.    "},
    {Id: 2, Title: "CMS", DutchConcept: "ng elit. Maecenas maximus elementum risus, non sollicitudin tortor faucibus in. Praesent et enim porta, bibendum mi et, rutrum ipsum. Morbi congue turpis sed est blandit tempor. Etiam a odio metus. Ut sit amet varius orci. Donec vel viverra diam. In luctus, libero vel scelerisque mollis", EnglishConcept: "d"},
    {Id: 3, Title: "Solid", DutchConcept: "Pellentesque eget iaculis mi, a venenatis tortor. Donec convallis venenatis velit, at pulvinar lacus pulvinar ac. Curabitur augue lacus, ultricies quis nisi a, scelerisque varius velit. Vestibulum gravida tristique malesuada. Praesent sodales velit id interdum fringilla. Fusce consequat maximus ipsum id varius. Nulla ac condimentum risus. Quisque vestibulum ultricies ex eu lobortis.", EnglishConcept: "quam lacus eleifend metus, sed tempus nunc quam at lectus. Mauris vel ullamcorper nunc. Fusce sodales est nec commodo dictum. Vivamus laoreet risus et dui convallis, eu malesuada neque posuere. Praesent mollis imperdiet scelerisque. Aenean quam erat, luctus a varius vel, posuere faucibus dui. Sed tincidunt, dolor vel scelerisque congue, leo tellus vehicula nibh, id tincidunt nisi tellus a eros. Praesent dictum eu arcu in rhoncus. Sed vel auctor sapien. Fusce accumsan leo non erat maximus vehicula."},
    {Id: 4, Title: "SEO", DutchConcept: "ng elit. Maecenas maximus elementum risus, non sollicitudin tortor faucibus in. Praesent et enim porta, bibendum mi et, rutrum ipsum. Morbi congue turpis sed est blandit tempor. Etiam a odio metus. Ut sit amet varius orci. Donec vel viverra diam. In luctus, libero vel scelerisque mollis", EnglishConcept: "d"},
    {Id: 5, Title: ".NET Framework", DutchConcept: "ng elit. Maecenas maximus elementum risus, non sollicitudin tortor faucibus in. Praesent et enim porta, bibendum mi et, rutrum ipsum. Morbi congue turpis sed est blandit tempor. Etiam a odio metus. Ut sit amet varius orci. Donec vel viverra diam. In luctus, libero vel scelerisque mollis", EnglishConcept: "d"},
    {Id: 6, Title: "Lievenshtein distance calculatie", DutchConcept: "ng elit. Maecenas maximus elementum risus, non sollicitudin tortor faucibus in. Praesent et enim porta, bibendum mi et, rutrum ipsum. Morbi congue turpis sed est blandit tempor. Etiam a odio metus. Ut sit amet varius orci. Donec vel viverra diam. In luctus, libero vel scelerisque mollis", EnglishConcept: "d"}
  ];

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
