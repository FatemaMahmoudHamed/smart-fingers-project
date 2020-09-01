import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import{ResponseVM } from '../Models/ResponseVM';
import { map, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private http:HttpClient ){}
  tokenStorageName: string = "userToken";
  base_url="http://localhost:4200";
  Api_url="https://localhost:44354";

  login(model)
  {
    return  this.post(`/api/user/login`,model);
  }

  post(path: string, body: Object = {}): Observable<ResponseVM> {  
    return this.http.post(`${this.Api_url}${path}`, body, { headers: this.setHeaders() }).pipe(catchError(this.formatErrors), map((res: ResponseVM) => res));
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

}
