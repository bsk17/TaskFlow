import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class Auth {
  private apiUrl = 'http://localhost:5023/api';
  private tokenKey = 'taskflow-token'; //might need to look into TaskFlow API
  private jwtHelper = new JwtHelperService();

  constructor(private http: HttpClient, private router: Router) {}

  login(credentials: { username: string; password: string }) {
    return this.http.post<{ token: string }>(`${this.apiUrl}/user/login`, credentials);
  }

  setToken(token: string) {
    localStorage.setItem(this.tokenKey, token);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  isLoggedIn(): boolean {
    const token = this.getToken();
    return !!token && !this.jwtHelper.isTokenExpired(token);
  }

  logout() {
    localStorage.removeItem(this.tokenKey);
    this.router.navigate(['/login']);
  }

  getUsername(): string | null {
    const token = this.getToken();
    if (!token) return null;
    return this.jwtHelper.decodeToken(token)?.unique_name;
  }

  getUserRole(): string | null {
    const token = this.getToken();
    if (!token) return null;
    return this.jwtHelper.decodeToken(token)?.role ?? null;
  }
}
