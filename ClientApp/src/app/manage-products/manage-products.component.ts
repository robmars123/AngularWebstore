import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { tap } from 'rxjs/operators';
import { Subject, Observable} from 'rxjs';

@Component({
  selector: 'app-manage-products',
  templateUrl: './manage-products.component.html',
  styleUrls: ['./manage-products.component.css']
})
export class ManageProductsComponent {
  title = "Admin Dashboard";
  products: any;

  constructor(
    private route: ActivatedRoute,
    // private heroService: HeroService,
    private location: Location,
    private http: HttpClient
  ){}

  ngOnInit(): void{
      this.fetchData();
  }

  onSubmit(id: any): void{
    this.http.delete('https://localhost:7165/api/Products/' + id).subscribe({
      //next: response =>this.products = response,
      error: error => console.log(error),
      complete: () => this.fetchData() //reactive functionality
    })
  }

  fetchData(){
    this.http.get('https://localhost:7165/api/Products/')
    .subscribe({
      next: response =>this.products = response,
      error: error => console.log(error),
      complete: () => console.log('Request has completed.')
    })
  }
}
