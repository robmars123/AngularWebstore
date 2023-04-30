import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { tap } from 'rxjs/operators';
import { Subject, Observable} from 'rxjs';
import { CSVService } from '../Services/CSVService';

@Component({
  selector: 'app-manage-products',
  templateUrl: './manage-products.component.html',
  styleUrls: ['./manage-products.component.css']
})
export class ManageProductsComponent {
  title = "Admin Dashboard";
  products: any;
  UploadCSV: any;

  public importedData: Array<any> = [];

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
}
