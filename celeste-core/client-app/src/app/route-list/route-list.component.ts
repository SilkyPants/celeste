import { Component, Inject, SystemJsNgModuleLoader } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-route-list',
  templateUrl: './route-list.component.html',
  styleUrls: ['./route-list.component.scss']
})
export class RouteListComponent {
  public routes: StarRoute[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    // http.get<StarRoute[]>(baseUrl + 'api/routeplanning').subscribe(result => {
    http.get<StarRoute[]>('/assets//mock/routes.json').subscribe(result => {
      this.routes = result.map(route=>{
        // do transforms
        // because Javascript is stupid
        return {...route, 
          totalJumps: this.totalJumps(route),
          totalBodies: this.totalBodies(route),
          totalVisited: this.totalVisited(route)
        }
      })
      debugger
    }, error => console.error(error));

  }

  totalJumps(route): number {
    return route.systems
      .map((system) => system.jumps)
      .reduce((result, value) => result + value);
  }

  totalBodies(route): number {
    return route.systems
      .map((system) => system.bodies)
      .reduce((result, value) => result.concat(value))
      .length;
  }

  totalVisited(route): number {
    return route.systems
      .map((system) => system.bodies.filter((body) => body.visited))
      .reduce((result, value) => result.concat(value))
      .length;
  }
  
}

export interface StarRoute {
  systems: StarSystem[];
  id: string;
  name: string;
  totalJumps?: number;
  totalBodies?: number;
  totalVisited?: number;
}

export interface StarSystem {
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


