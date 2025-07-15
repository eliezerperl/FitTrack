import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { AuthRequest } from '../models/auth-request-model';
import { Observable } from 'rxjs';
import { User } from '../models/user-model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl: string = `${environment.apiUrl}/auth`;

  constructor(private http: HttpClient) {}

  login(authRequest: AuthRequest): Observable<User> {
    return this.http.post<User>(`${this.apiUrl}/login`, authRequest);
  }

  register(authRequest: AuthRequest): Observable<User> {
    return this.http.post<User>(`${this.apiUrl}/register`, authRequest);
  }
}
