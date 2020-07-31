import { Component, Inject, SystemJsNgModuleLoader } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators'
import { StarRoute } from '../models/star-route';

@Component({
  selector: 'app-route-list',
  templateUrl: './route-list.component.html',
  styleUrls: ['./route-list.component.scss']
})
export class RouteListComponent {
  public routes: Observable<StarRoute[]>;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    // http.get<StarRoute[]>(baseUrl + 'api/routeplanning').pipe(map(result => {
    this.routes = http.get<StarRoute[]>('/assets//mock/routes.json').pipe(map(result => {
      return result.map(x => new StarRoute(x));
    }, error => console.error(error)));  
  }
}


