import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Subject, Observable} from 'rxjs';
import { tap } from 'rxjs/operators'

class Product{
  product_Name: any;
  price: any;
  description: any;
  quantityPerUnit: any;
  subcategory_Id: any;
  category_Id: any;
}
@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent {
  product = new Product();
  isComplete:  any;
  constructor(
    private route: ActivatedRoute,
    private http: HttpClient,
    private location: Location,
    private formBuilder: FormBuilder,
    private reactiveForm: ReactiveFormsModule,
    private router: Router
    ){}

    //Declare forms
    form: FormGroup = new FormGroup({
      productName: new FormControl(''),
      description: new FormControl(''),
      price: new FormControl(''),
      quantityPerUnit: new FormControl(''),
      subcategory_Id: new FormControl(''),
      category_Id: new FormControl(''),
    });

    onAdd(){
      //POST (ADD)
      this.http.post('https://localhost:7165/api/Products/',this.product)
      .subscribe({
        //next: response =>this.product = response,
        error: error => console.log(error),
        complete: () => this.goBack()       
      });
    }

    onSubmit() {
      //Get data from view
      this.product.product_Name = this.form.controls['productName'].value;
      this.product.description = this.form.controls['description'].value;
      this.product.price = this.form.controls['price'].value;
      this.product.quantityPerUnit = this.form.controls['quantityPerUnit'].value;
      this.product.category_Id = this.form.controls['category_Id'].value;
      this.product.subcategory_Id = this.form.controls['subcategory_Id'].value;
      this.onAdd();
  }

  goBack(): void {
    this.location.back();
  }
}

