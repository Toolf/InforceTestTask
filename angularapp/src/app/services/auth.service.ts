import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl: string = "https://localhost:7098";

  constructor(private http: HttpClient) { }

  login(username: String, password: String): Observable<Object> {
    return this.http.post(`${this.apiUrl}/Auth/login`, { username, password });
  }

  registry(username: String, password: String): Observable<Object> {
    return this.http.post(`${this.apiUrl}/Auth/registry`, { username, password });
  }

  me(): Observable<Object> {
    return this.http.get(`${this.apiUrl}/Auth/profile`);
  }
}
