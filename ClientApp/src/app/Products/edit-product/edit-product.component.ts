import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common'
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.css']
})
export class EditProductComponent{
  product: any;
  productId: any;
  notification: any;

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    private http: HttpClient,
    private formBuilder: FormBuilder,
    private reactiveForm: ReactiveFormsModule
  ){  }


  //Declare form
  form: FormGroup = new FormGroup({
    productName: new FormControl(''),
    description: new FormControl(''),
    price: new FormControl(''),
    quantityPerUnit: new FormControl(''),
  });
 
  
  ngOnInit(): void{
    //GET
    this.productId = Number(this.route.snapshot.paramMap.get('id'));
    this.http.get('https://localhost:7165/api/Products/' + this.productId).subscribe({
      next: response =>this.product = response,
      error: error => console.log(error),
      complete: () => console.log('Request has completed.')
    })
    
  }

  onUpdate(){
        //PUT
        this.http.put('https://localhost:7165/api/Products/'+ this.productId,this.product).subscribe({
         // next: response =>this.product = response,
          error: error => console.log(error),
          complete: () => console.log('Request has completed.')
        });
        this.notification = "Successfully saved."
  }
  onSubmit() {
      //Get data from view
      this.product.product_Name = this.form.controls['productName'].value,
      this.product.description = this.form.controls['description'].value,
      this.product.price = this.form.controls['price'].value,
      this.product.quantityPerUnit = this.form.controls['quantityPerUnit'].value
      this.onUpdate();
  }

  goBack(): void {
    this.location.back();
  }
}
