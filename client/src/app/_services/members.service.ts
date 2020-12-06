import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Member } from '../_modules/member';
import { User } from '../_modules/user';


@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl;
  mambers: Member[] = [];

  constructor(private http:HttpClient) { }

  getMembers(): Observable<Member[]>{
    if(this.mambers.length > 0) return of(this.mambers);
    return this.http.get<Member[]>(this.baseUrl + 'user')
          .pipe(
            map(member => {
              this.mambers = member;
              return member;
            })
          );
  }

  getMember(username: string): Observable<Member>  {
    const member = this.mambers.find(x=>x.username == username);

    if (member !== undefined)   return of(member);
    return this.http.get<Member>(this.baseUrl+ `user/${username}`);
  }

  updateMember(member: Member){
    return this.http.put(this.baseUrl+'user', member)
      .pipe(
        map(()=> {
          const index = this.mambers.indexOf(member);
          this.mambers[index] = member;
        })
      );
  }

}
