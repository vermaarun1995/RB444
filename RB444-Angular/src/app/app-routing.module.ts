import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { LayoutComponent } from './components/layout/layout.component';
import { AllsportHighlightComponent } from './components/allsport-highlight/allsport-highlight.component';
import { InplayComponent } from './components/inplay/inplay.component';
import { FullmarketComponent } from './components/fullmarket/fullmarket.component';

const routes: Routes = [
  {
    path: '', component: LayoutComponent, children: [
      { path: '', component: HomeComponent },
      { path: 'home', component: HomeComponent },
      { path: 'allsport-highlight/:id', component: AllsportHighlightComponent },
      { path: 'inplay', component: InplayComponent },
      { path: 'fullmarket/:eventId/:sportId', component: FullmarketComponent },
     ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
