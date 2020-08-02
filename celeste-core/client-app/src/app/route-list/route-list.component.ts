import { Component, OnInit } from '@angular/core';
import { StarRoutesDataSource } from '../data-source/star-routes.data-source';
import { StarRoutesService } from '../services/star-routes.service';
import { HeaderTitleService } from '../services/header-title.service';

@Component({
  selector: 'app-route-list',
  templateUrl: './route-list.component.html',
  styleUrls: ['./route-list.component.scss']
})
export class RouteListComponent implements OnInit {

  displayedColumns = ['name', 'totalSystems', 'totalJumps', 'totalStops', 'totalVisited'];
  dataSource: StarRoutesDataSource;

  constructor(private starRoutesService: StarRoutesService, private headerTitleService: HeaderTitleService) { }

  ngOnInit() { 
    this.headerTitleService.setTitle('Routes')

    this.dataSource = new StarRoutesDataSource(this.starRoutesService)
    this.dataSource.loadRoutes();
  }

  onRowClicked(row) {
    console.log('Row clicked: ', row);
  }
}


