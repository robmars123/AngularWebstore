import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule} from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './Routing/app-routing.module';
import { AppComponent } from './app.component';
import { ProductsComponent } from './Products/Product-List/products.component';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { ManageProductsComponent } from './manage-products/manage-products.component';
import { AddProductComponent } from './products/add-product/add-product.component';
import { EditProductComponent } from './products/edit-product/edit-product.component';
import { CSVService } from './Services/CSVService';
import { FileUploadService } from './Services/file-upload.service';
import {NgxPaginationModule} from 'ngx-pagination';

@NgModule({
  declarations: [
    AppComponent,
    ProductDetailsComponent,
    AddProductComponent,
    EditProductComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    NgxPaginationModule,
    HttpClientModule,//added this for http request
  ],
  providers: [CSVService, FileUploadService],
  bootstrap: [AppComponent]
})
export class AppModule { }
