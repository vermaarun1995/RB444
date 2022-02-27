import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  @Input() pageMenu : any[] = [];
  @Output() pageChange : EventEmitter<any> = new EventEmitter();

  constructor() { }

  changeSidebarList(sportId:string):void {
    this.pageChange.emit(sportId);
  }

  ngOnInit(): void {
  }

}
