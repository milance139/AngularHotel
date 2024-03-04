import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';
import { ApiResponse } from '../pages/shared/models/ApiResponse';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loggedIn = new BehaviorSubject<boolean>(false);

  constructor(private http: HttpClient) {
    this.checkToken();
  }

  //provjeriti token, da ne bi user napisao bilo sta
  private checkToken(): void {
    const token = localStorage.getItem('token');
    if (token) {
      this.loggedIn.next(true);
    }
  }

  //definisati enviroment file i u njega postaviti link do bekenda
  login(credentials: { email: string, password: string }): Observable<ApiResponse<string>> {
    return this.http.post<any>('https://localhost:7156/api/Auth/login', credentials).pipe(
      tap(response => {
        if (response.success) {
          localStorage.setItem('token', response.data);
          this.loggedIn.next(true);
        }
      })
    );
  }
  
  logout(): void {
    localStorage.removeItem('token');
    this.loggedIn.next(false);
  }
  
  isLoggedIn(): Observable<boolean> {
    return this.loggedIn.asObservable();
  }
}
