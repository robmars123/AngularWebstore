import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductsComponent } from './products.component';
import { RouterModule, Routes } from '@angular/router';
import { ProductRoutingModule } from './product-routing.module';
import { NgxPaginationModule } from 'ngx-pagination';
import { ManageProductsComponent } from 'src/app/manage-products/manage-products.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
const productRoutes: Routes = [{ path: '', component: ProductsComponent}];

@NgModule({
  declarations:
  [ProductsComponent,
  ManageProductsComponent],
  imports: [
    CommonModule, ProductRoutingModule,    NgxPaginationModule,FormsModule,ReactiveFormsModule,
  ]
})
export class ProductListModule { }
