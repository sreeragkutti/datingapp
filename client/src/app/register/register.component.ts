import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Input() usersFromHomeComponent: any | null;
  @Output() cancelRegister = new EventEmitter();

  model: any = {}

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
  }

  register(){
    this.accountService.register(this.model)
      .subscribe((response) => {
        this.canecl();
        console.log(response);
      }, error => {
        console.log(error);
      });
  }

  canecl(){
    this.cancelRegister.emit(false);
  }

}
