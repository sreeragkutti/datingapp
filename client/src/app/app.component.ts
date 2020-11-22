import { JsonPipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './_modules/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title: string = 'The dating app';
  users: any;
  constructor(private http: HttpClient) {}

  setCurrentUser(){
    const user: User = JSON.parse(localStorage.getItem('user') as string);
  }


  ngOnInit() {
    this.http.get('https://localhost:5001/api/user').subscribe(response => {
      this.users = response;
    },
    error => {
      console.log(error);
    }
    )
  }
}
