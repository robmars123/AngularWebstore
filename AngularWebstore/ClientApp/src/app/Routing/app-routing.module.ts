import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductDetailsComponent } from '../product-details/product-details.component';
import { ProductsComponent } from '../Products/Product-List/products.component';
import { ManageProductsComponent } from '../manage-products/manage-products.component';
import { AddProductComponent } from '../products/add-product/add-product.component';
import { EditProductComponent } from '../products/edit-product/edit-product.component';

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'product-details/:id', component: ProductDetailsComponent },
  { path: 'manage-products', component: ManageProductsComponent},
  { path: 'add-product', component: AddProductComponent},
  { path: 'edit-product/:id', component: EditProductComponent},
  { path: '**', component: ProductsComponent } // If no matching route found, go back to home route
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
