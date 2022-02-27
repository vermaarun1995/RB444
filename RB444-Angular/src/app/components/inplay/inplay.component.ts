import { Component, OnInit } from '@angular/core';
import {NgbNavConfig} from '@ng-bootstrap/ng-bootstrap';
import { of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { HttpService } from 'src/app/services/http.service';

@Component({
  selector: 'app-inplay',
  templateUrl: './inplay.component.html',
  styleUrls: ['./inplay.component.css'],
  providers: [NgbNavConfig]
})
export class InplayComponent implements OnInit {

  constructor(private service : HttpService, config: NgbNavConfig) {
    config.destroyOnHide = false;
    config.roles = false;
    this.getDaysWiseEvents();
  }

  inplayData : any = [];
  todayData : any = [];
  tomorrowData : any = [];


  getDaysWiseEvents:any = () =>{
    this.service.post('exchange/events/getDaysWiseEvents', "")
    .pipe(map(response => {
      return response;
    }),
    catchError(() => {
      return of([]);
    }))
    .subscribe(response => {
      if(response.data != null){
        for (let [key, value] of Object.entries(response.data)) {
          if(key == "inplay"){
            this.inplayData = value;
          }
          if(key == "today"){
            this.todayData = value;
          }
          if(key == "tomorrow"){
            this.tomorrowData = value;
          }
        }
      }else{
        this.inplayData = [];
        this.todayData = [];
        this.tomorrowData = [];
      }
    });
  }

  ngOnInit(): void {

  }

}
