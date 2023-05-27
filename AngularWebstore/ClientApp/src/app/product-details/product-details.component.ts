import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { FileUploadService } from '../Services/file-upload.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})

export class ProductDetailsComponent {
  productDetail: any;
  imageList?: any;
  productId: number = 0;

  imgMainDisplay: HTMLImageElement | undefined; 

  constructor(
    private route: ActivatedRoute,

    private location: Location,
    private http: HttpClient,
    private uploadService: FileUploadService
  ) {}

  ngOnInit(): void{
    this.fetchData();
  }

  fetchData(){
    this.productId = Number(this.route.snapshot.paramMap.get('id'));
    this.http.get('https://localhost:7165/api/Products/' + this.productId).subscribe({
      next: response =>this.productDetail = response,
      error: error => console.log(error),
      complete: () => console.log('Request has completed.')
    })

    this.uploadService.getFiles(this.productId).subscribe({
      next: response =>this.imageList = response
     });
  }

  onSelectImage(image: any){
    document.images[document.images.length - 1].src = image.convertedProductImage; 
  }
 
  goBack(): void{
    this.location.back();
  }
}
