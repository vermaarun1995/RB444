import { AfterViewInit, Component, DoCheck, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { HttpService } from 'src/app/services/http.service';

@Component({
  selector: 'app-allsport-highlight',
  templateUrl: './allsport-highlight.component.html',
  styleUrls: ['./allsport-highlight.component.css']
})
export class AllsportHighlightComponent implements OnInit, OnChanges {

  constructor(private service : HttpService, private activatedRoute: ActivatedRoute) { }

  eventList : any;

  getAllEventsList:any = (paramId : number) =>{
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
          if(parseInt(key) == paramId){
            this.eventList = value;
          }
        }
      }else{
        this.eventList = [];
      }
    });
  }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(paramsId => {
      this.getAllEventsList(paramsId.id);
    });
  }

  ngOnChanges(): void {  
  }

}
