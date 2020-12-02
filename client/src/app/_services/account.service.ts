import { JsonPipe } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import {map} from 'rxjs/operators';
import { User } from '../_modules/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl: string = "http://localhost:5000/api/";

  private currentUserSource = new ReplaySubject<User | null>(1);
  currentUserSource$ =this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }

  loging(model: any){
    return this.http.post<User>(this.baseUrl + 'Account/login', model)
      .pipe(
        map((response: User) => {
          const user: User = response;
          if(user){
            localStorage.setItem('user',JSON.stringify(user));
            this.currentUserSource.next(user);
          }
        })
      )
  }

  register(model: any){
    return this.http.post<User>(this.baseUrl + 'Account/register', model)
      .pipe(
        map((response: User ) => {
          const user: User = response;
          localStorage.setItem('user',JSON.stringify(user));
          this.currentUserSource.next(user);
          return user;
        })
      );
  }

  setCurrentUser(user: User){
    this.currentUserSource.next(user);
  }

  logout(){
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
}
