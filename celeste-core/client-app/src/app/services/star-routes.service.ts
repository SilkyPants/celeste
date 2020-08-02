import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators'

import { StarRoute } from '../models/star-route';

@Injectable()
export class StarRoutesService {

    constructor(private http: HttpClient) { }

    findRoutes(): Observable<StarRoute[]> {
        // http.get<StarRoute[]>('/api/routeplanning')
        return this.http.get<StarRoute[]>('/assets//mock/routes.json')
            .pipe(
                map(result => result.map(x => new StarRoute(x)))
            );
    }
}