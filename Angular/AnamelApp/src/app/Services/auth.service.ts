import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private router: Router) { }
  tokenStorageName: string = "userToken";
  canActivate(): boolean {
    let logged = this.hasAccessToken();
    if (logged)
      return true;
    else {
      this.router.navigateByUrl('/login');
      return false;
    }
  }

  getToken() {
    let token: string = localStorage.getItem("userToken");
    if (token == null || token == 'undefined') {
      return "";
    }
    return token;
  }

  hasAccessToken(): boolean {
    return (localStorage.getItem("userToken") != null && localStorage.getItem("userToken").length > 0)
  }
  removeToken() {
    localStorage.setItem(this.tokenStorageName, "");
    localStorage.removeItem(this.tokenStorageName);
  }
}
