import { Component, Input, OnInit } from '@angular/core';
import { of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { HttpService } from 'src/app/services/http.service';
import { SportList } from 'src/app/models/sportList';
import { SportTournamentsList } from 'src/app/models/sportTournamentsList';
import { TournamentEventsList } from 'src/app/models/tournamentEventsList';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {

  constructor(private service : HttpService) { }

  @Input() pageMenu : any[] = [];

  sportTournamentsList : SportTournamentsList[] = [];
  tournamentEventsList : TournamentEventsList[] = [];    
  isSportTournaments : boolean = false;
  isSportTournamentsCollapsed : boolean[] = [];

  getSportList:any = () =>{
    this.isSportTournaments = false;
    this.isSportTournamentsCollapsed.length = 0;
  }

  getSportTournamentsList:any = (sportId:string) =>{
    this.service.post('exchange/tournaments/sportTournamentsList', {"sportId" : sportId})
    .pipe(map(response => {
      return response;
    }),
    catchError(() => {
      return of([]);
    }))
    .subscribe(response => {
      if(response.data != null){
        this.sportTournamentsList = response.data;
        this.isSportTournaments = true;
      }
    });
    
  }

  getTournamentEventsList:any = (tournamentId:string, index : number) =>{
    this.service.post('exchange/events/tournamentEventsList', {"tournamentId" : tournamentId})
    .pipe(map(response => {
      return response;
    }),
    catchError(() => {
      return of([]);
    }))
    .subscribe(response => {
      if(response.data != null){
        this.tournamentEventsList = response.data;
        for(let a = 0; a < this.sportTournamentsList.length; a++){
          if(a != index){
            this.isSportTournamentsCollapsed[a] = false;
          }else{
            console.log(this.isSportTournamentsCollapsed[a]);
            this.isSportTournamentsCollapsed[a] = (!this.isSportTournamentsCollapsed[a] ? true : false);
          }
        }
      }
    });
  }

  updateSportTournamentsList($event:string): void{
    this.getSportTournamentsList($event);
  }

  ngOnInit(): void {
  }

}
