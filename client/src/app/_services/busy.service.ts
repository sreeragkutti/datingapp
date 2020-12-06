import { NoopAnimationPlayer } from '@angular/animations';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
  busyRequestCount: number = 0;
  constructor(private toastrService: ToastrService) { }


  busy(){
    this.busyRequestCount ++;
    this.toastrService.info('loading...');
  }

  idle(){
    this.busyRequestCount --;
    if(this.busyRequestCount <= 0) {
      this.busyRequestCount = 0;
      this.toastrService.info('loading complete...');
    }
  }


}
