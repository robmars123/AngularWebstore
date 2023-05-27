import { HttpClient, HttpEvent, HttpEventType, HttpHeaders, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ProductImage } from '../Models/ProductImage';

@Injectable({
  providedIn: 'root'
})
export class FileUploadService {
  private baseUrl = 'https://localhost:7165/api/Products';

  constructor(private http: HttpClient) {}

  upload(file: File, productId: number): Observable<HttpEvent<any>>{
    const formData: FormData = new FormData();
    
    formData.append('file', file, file.name);

    const req = new HttpRequest('POST', `${this.baseUrl}` + "/UploadImageAsync?productId=" + productId, formData);

    return this.http.request(req);
  }

  getFiles(productId: number): Observable<any> {
    return this.http.get(`${this.baseUrl}`+ "/GetImagesAsync?productId=" + productId);
  }

  getAllImages(){
    return this.http.get<ProductImage[]>(`${this.baseUrl}`+ "/GetAllImagesAsync");
  }
}
