import {CollectionViewer, DataSource} from "@angular/cdk/collections";
import { StarRoute } from '../models/star-route';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { StarRoutesService } from '../services/star-routes-service';
import { catchError, finalize } from 'rxjs/operators';

export class StarRoutesDataSource implements DataSource<StarRoute> {

    private routesSubject = new BehaviorSubject<StarRoute[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);

    public loading$ = this.loadingSubject.asObservable();

    constructor(private starRoutesService: StarRoutesService) {}

    connect(collectionViewer: CollectionViewer): Observable<StarRoute[]> {
      return this.routesSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
      this.loadingSubject.complete();
      this.routesSubject.complete();
    }
  
    loadRoutes(pageIndex: number = 1, pageSize: number = 5) {
      this.loadingSubject.next(true);

      this.starRoutesService.findRoutes()
      .pipe(
          catchError(() => of([])),
          finalize(() => this.loadingSubject.next(false))
      )
      .subscribe(routes => this.routesSubject.next(routes));
    }  
}