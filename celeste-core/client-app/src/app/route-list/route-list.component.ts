import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-route-list',
  templateUrl: './route-list.component.html',
  styleUrls: ['./route-list.component.scss']
})
export class RouteListComponent {
  public routes: StarRoute[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<StarRoute[]>(baseUrl + 'api/routeplanning').subscribe(result => {
      this.routes = result;
    }, error => console.error(error));
  }
}

interface StarRoute {
  systems: StarSystem[];
  id: string;
  name: string;
}

interface StarSystem {
  bodies: Body[];
  jumps: number;
  name: string;
  x: number;
  y: number;
  z: number;
}

export interface Body {
  distanceToArrival: number;
  estimatedMappingValue: number;
  estimatedScanValue: number;
  id: number;
  id64: string | null;
  isTerraformable: boolean;
  name: string;
  subtype: string;
  type: string | null;
  visited: boolean;
}
