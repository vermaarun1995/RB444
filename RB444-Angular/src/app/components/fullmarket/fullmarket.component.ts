import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { HttpService } from 'src/app/services/http.service';

@Component({
  selector: 'app-fullmarket',
  templateUrl: './fullmarket.component.html',
  styleUrls: ['./fullmarket.component.css']
})
export class FullmarketComponent implements OnInit {

  constructor(private service : HttpService, private activatedRoute : ActivatedRoute) { }

  matchOddsData : any = [];

  getMarketsOfEventList:any = (eventId: number, sportId: number) =>{
    this.service.post('exchange/market/getMarketsOfEventList', { "eventId": eventId, "sportId": sportId})
    .pipe(map(response => {
      return response;
    }),
    catchError(() => {
      return of([]);
    }))
    .subscribe(response => {
      if(response.data != null){
        for (let [key, value] of Object.entries(response.data)) {
          if(key == "matchOddsData"){
            this.matchOddsData = value;
          }
        }
      }
    });
  }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(paramsId => {
      this.getMarketsOfEventList(paramsId.eventId, paramsId.sportId);
    });
  }

}
