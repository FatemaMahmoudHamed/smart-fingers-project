import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import{ResponseVM } from '../Models/ResponseVM';
import{CustomerVM } from '../Models/CustomerVM';
import { map, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})

export class CustomerService {
  userToken: string;
  tokenStorageName: string = "userToken";
  userfullNameStorageName: string = "userFullName";

  base_url="http://localhost:4200";
  Api_url="https://localhost:44354";

  

  constructor(private route: Router,private http: HttpClient) {
  }
  
  getAllCustomers(){
    return  this.get(`/api/customer/getall`);
  }

  getCustomerByID(customerID){
    return  this.get(`/api/customer/get?customerID=${customerID}`);
  }

  CreateCustomer(model:CustomerVM )
  {
    return  this.post(`/api/customer/create`,model);
  }

  EditCustomer(model:CustomerVM)
  {
    return  this.update(`/api/customer/Update`,model);
  }

  DeleteCustomer(customerID)
  {
    return  this.remove(`/api/customer/delete?customerID=${customerID}`);
  }

  setToken(token: string) {
    localStorage.setItem(this.tokenStorageName,"Bearer "+token);
  }

  getToken() {
    let token: string = localStorage.getItem("userToken");
    if (token == null || token == 'undefined') {
      return "";
    }
    return token;
  }

  setUserfullName(name: string) {
    localStorage.setItem(this.userfullNameStorageName, name);
  }

  getUserfullName() {
    let name: string = localStorage.getItem(this.userfullNameStorageName);
    if (name == null || name == 'undefined') {
      return "";
    }
    return name;
  }

  hasAccessToken(): boolean {
    return (localStorage.getItem("userToken") != null && localStorage.getItem("userToken").length > 0)
}
  removeToken() {
    
    localStorage.setItem(this.tokenStorageName, "");
    localStorage.removeItem(this.tokenStorageName);
  }

  private setHeaders(): HttpHeaders {
    let headersConfig =
    {
      'Content-Type': 'application/json',
      'Accept': '*/*',    
      'Authorization': this.getToken()
    };
    return new HttpHeaders(headersConfig);
  }

  private formatErrors(error: any) {
    if (error.status != "200") {
     window.open(`${this.base_url}/error/${error.error.message }`, "_self");
    }
    return Observable.throw(error);
  }

  get(path: string, params?: HttpParams): Observable<ResponseVM> {
    return this.http.get(`${this.Api_url}${path}`, { headers: this.setHeaders(), params }).pipe(catchError(er => this.formatErrors(er)), map((res: ResponseVM) => res));
  }

  post(path: string, body: Object = {}): Observable<ResponseVM> {   
    return this.http.post(`${this.Api_url}${path}`, body, { headers: this.setHeaders() }).pipe(catchError(this.formatErrors), map((res: ResponseVM) => res));
  }

  update(path: string, body: Object = {}): Observable<ResponseVM> {
    return this.http.put(`${this.Api_url}${path}`, body, { headers: this.setHeaders() }).pipe(catchError(this.formatErrors), map((res: ResponseVM) => res));
  }

  remove(path: string): Observable<ResponseVM> {
    return this.http.delete(`${this.Api_url}${path}`, { headers: this.setHeaders() })
      .pipe(catchError(this.formatErrors), map((res: ResponseVM) => res));
  }
}
