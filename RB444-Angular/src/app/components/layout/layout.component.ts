import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { SportList } from 'src/app/models/sportList';
import { HttpService } from 'src/app/services/http.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent implements OnInit {

  constructor(private service : HttpService) { }

  sidebarList : SportList[] = [];

  getSportList():Observable<SportList[]> {
    return this.service.post('exchange/sports/sportsList', "")
    .pipe(map(response => {
      return response.data;
    }),
    catchError(() => {
      return of([]);
    }));
  }

  ngOnInit(): void {
    this.getSportList().subscribe(res => this.sidebarList = res);
  }

}
