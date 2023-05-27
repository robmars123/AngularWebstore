import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { tap } from 'rxjs/operators';
import { Subject, Observable} from 'rxjs';
import { CSVService } from '../Services/CSVService';
import { Product } from '../Models/Product';

@Component({
  selector: 'app-manage-products',
  templateUrl: './manage-products.component.html',
  styleUrls: ['./manage-products.component.css']
})
export class ManageProductsComponent {
  title = "Admin Dashboard";
  products: Product[] = [];
  UploadCSV: any;
  response: any;
  isDataLoaded = false;
  currentPage :number = 1;
  totalProducts: number = 10;
  pageSize: number= 10;
  public importedData: Array<any> = [];
  productTotalCount: number = 0;
  filter: string = "Most_Relevant";
  constructor(
    private route: ActivatedRoute,
    // private heroService: HeroService,
    private location: Location,
    private http: HttpClient,
    private _csvService: CSVService
  ){}

  ngOnInit(): void{
      this.fetchData();
  }

  loadData(){
      this.products = this.response.data;
      this.productTotalCount = this.response.totalRecords;
  }

  onSubmit(id: any): void{
    this.http.delete('https://localhost:7165/api/Products/' + id).subscribe({
      //next: response =>this.products = response,
      error: error => console.log(error),
      complete: () => this.fetchData() //reactive functionality
    });
  }

  fetchData(){
    this.http.get<any[]>('https://localhost:7165/api/Products?pageNumber='+this.currentPage+'&pageSize='+this.pageSize+'&filterString='+this.filter)
    .subscribe({
      next: response =>this.response = response,
      error: error => console.log(error),
      complete: () => this.loadData()
    });
  }

  uploadDataToServer(){
    let options = { headers: new HttpHeaders().set('Content-Type', 'application/json') };
    let toJsonString: any;
    toJsonString = JSON.stringify(this.UploadCSV);

    this.http.post('https://localhost:7165/api/Products/UploadCSV?data=',  toJsonString, options)
    .subscribe({
      next: response =>this.UploadCSV = response,
      error: error => console.log(error),
      complete: () => this.fetchData()
    })
  }
  private async getTextFromFile(event: any) {
    const file: File = event.target.files[0];
    let fileContent = await file.text();
    this.UploadCSV = fileContent;
    
    return fileContent;
  }
  async importDataFromCSV(event: any) {
    let fileContent = await this.getTextFromFile(event);
    this.importedData = this._csvService.importDataFromCSV(fileContent);

    this.uploadDataToServer();
  }

  onPageChange(event: any) {
    // reset page if items array has changed
    if (event !== this.currentPage) {
        this.currentPage = event;
        this.fetchData();
    }
}
}
