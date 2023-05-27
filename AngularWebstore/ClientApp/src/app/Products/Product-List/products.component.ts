import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductImage } from 'src/app/Models/ProductImage';
import { FileUploadService } from 'src/app/Services/file-upload.service';
import { Product } from 'src/app/Models/Product';


@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent {
  response: any;
  productsList: Product[] = [];
  productImages: ProductImage[] = [];
  count :number = 0;
  currentPage :number = 1;
  totalProducts: number = 10;
  pageSize: number= 10;
  isDataLoaded = false;
  productTotalCount: number = 0;
  filter: string = "Most_Relevant";
  filters: any[] = ["Most_Relevant","Price", "Date_Added"];
  constructor(private http: HttpClient,
    private uploadService: FileUploadService 
    ) {}
    
  ngOnInit(): void{
    //initial load
      this.fetchData();
    }
  loadData(){
      this.productsList = this.response.data;
      this.productTotalCount = this.response.totalRecords;
      this.loadAllImagesFromDB();
  }
  fetchData(): void {
    this.http.get<any[]>('https://localhost:7165/api/Products?pageNumber='+this.currentPage+'&pageSize='+this.pageSize+'&filterString='+this.filter)
      .subscribe({
          next: response =>this.response = response,
          error: error => console.log(error),
          complete: () => this.loadData()
        });
  }

  //Query all images
  loadAllImagesFromDB(){
    this.uploadService.getAllImages().subscribe(data => {
      this.productImages = data;

      //map product images per  product
      this.productsList.forEach(_product => {
        _product.images = this.productImages.filter((_img: any) => _img.product_Id == _product.product_Id);
    });
    });
  }

  onPageChange(event: any) {
    // reset page if items array has changed
    if (event !== this.currentPage) {
        this.currentPage = event;
        this.fetchData();
    }
}
selected(){
  this.fetchData();
}

}
