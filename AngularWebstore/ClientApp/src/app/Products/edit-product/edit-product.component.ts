import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common'
import { HttpClient, HttpEventType, HttpHeaders, HttpResponse } from '@angular/common/http';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';

import { ImageUploadComponent } from 'src/app/ImageUpload/image-upload/image-upload.component';
import { Observable } from 'rxjs/internal/Observable';
import { FileUploadService } from 'src/app/Services/file-upload.service';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.css']
})
export class EditProductComponent{
  product: any;
  productId: any;
  notification: any;
  recentUploadFileName = "";

  selectedFiles?: FileList;
  currentFile!: File;
  progress = 0;
  message = '';
  preview = '';
  categories: any;
  subCategories: any;
  imageList?: any;
  singleImage: any;
  uploadProgress: number | undefined;

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    private http: HttpClient,
    private formBuilder: FormBuilder,
    private reactiveForm: ReactiveFormsModule,
    private uploadService: FileUploadService
  ){  }


  //Declare form
  form: FormGroup = new FormGroup({
    productName: new FormControl(''),
    description: new FormControl(''),
    price: new FormControl(''),
    quantityPerUnit: new FormControl(''),
    category_id: new FormControl(''),
    subcategory_id: new FormControl(''),
  });
 
  
  ngOnInit(): void{
    if(this.currentFile)
    {
      this.recentUploadFileName = this.currentFile.name;
    }   
    this.fetchData();
  }
  getCategories(){
    this.categories = this.product.categories;
    this.subCategories = this.product.subcategories;
  }
  fetchData(){
    //GET data
    this.productId = Number(this.route.snapshot.paramMap.get('id'));
    this.http.get('https://localhost:7165/api/Products/' + this.productId).subscribe({
      next: response =>this.product = response,
      error: error => console.log(error),
      complete: () => this.getCategories()
    });

     this.uploadService.getFiles(this.productId).subscribe({
      next: response =>this.imageList = response
     });
  }
  onUpdate(){
        //PUT
        this.http.put('https://localhost:7165/api/Products/'+ this.productId,this.product).subscribe({
         // next: response =>this.product = response,
          error: error => console.log(error),
          complete: () => this.notification = "Successfully saved."
        });
        
  }
  onSubmit() {
      //Get data from view
      this.product.product_Name = this.form.controls['productName'].value,
      this.product.description = this.form.controls['description'].value,
      this.product.price = this.form.controls['price'].value,
      this.product.quantityPerUnit = this.form.controls['quantityPerUnit'].value
      this.product.category_id = this.form.controls['category_id'].value
      this.product.subcategory_id = this.form.controls['subcategory_id'].value
      this.onUpdate();
      this.onUpload();
  }

  goBack(): void {
    this.location.back();
  }

  onFileChanged(event: any) {
    this.currentFile = event.target.files[0];
    this.recentUploadFileName = this.currentFile.name;
  }

  onUpload() {
    this.uploadService.upload(this.currentFile,this.productId).subscribe({
       complete: () => this.fetchData()});
       this.notification = this.recentUploadFileName + " was successfully saved."
  }
}
