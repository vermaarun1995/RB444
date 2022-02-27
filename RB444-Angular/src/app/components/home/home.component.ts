import { Component, OnInit } from '@angular/core';
import { of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { HttpService } from 'src/app/services/http.service';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private service : HttpService) {
    this.getAllEventsList();
  }  
  active = 1;

  cricketData : any = [];
  soccerData : any = [];
  tennisData : any = [];

  getAllEventsList:any = () =>{
    this.service.post('exchange/market/matchodds/allEventsList', "")
    .pipe(map(response => {
      return response;
    }),
    catchError(() => {
      return of([]);
    }))
    .subscribe(response => {
      if(response.data != null){
        for (let [key, value] of Object.entries(response.data)) {
          if(parseInt(key) == 4){
            this.cricketData = value;
          }
          if(parseInt(key) == 1){
            this.soccerData = value;
          }
          if(parseInt(key) == 2){
            this.tennisData = value;
          }
        }
      }else{
        this.cricketData = [];
        this.soccerData = [];
        this.tennisData = [];
      }
    });
  }

  ngOnInit(): void {
  }

}
