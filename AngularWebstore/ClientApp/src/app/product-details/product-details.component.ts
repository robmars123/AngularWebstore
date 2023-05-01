import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent {
  productDetail: any;
  constructor(
    private route: ActivatedRoute,
    // private heroService: HeroService,
    private location: Location,
    private http: HttpClient
  ) {}

  ngOnInit(): void{
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.http.get('https://localhost:7165/api/Products/' + id).subscribe({
      next: response =>this.productDetail = response,
      error: error => console.log(error),
      complete: () => console.log('Request has completed.')
    })
  }

  goBack(): void {
    this.location.back();
  }
}
