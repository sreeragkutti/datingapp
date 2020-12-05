import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Member } from '../_modules/member';


@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl;

  constructor(private http:HttpClient) { }

  getMembers(): Observable<Member[]>{
    return this.http.get<Member[]>(this.baseUrl + 'user');
  }

  getMember(username: string): Observable<Member>  {
    return this.http.get<Member>(this.baseUrl+ `user/${username}`);
  }

}
