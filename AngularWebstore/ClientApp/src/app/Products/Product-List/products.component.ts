import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Routes } from '@angular/router';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent {
  products: any;

  constructor(private http: HttpClient) {}
    
  ngOnInit(): void{
      this.http.get('https://localhost:7165/api/Products').subscribe({
        next: response =>this.products = response,
        error: error => console.log(error),
        complete: () => console.log('Request has completed.')
      })
    }
}
