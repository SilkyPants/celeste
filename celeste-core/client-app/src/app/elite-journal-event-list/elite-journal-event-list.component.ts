import { Component, OnInit } from '@angular/core';
import { HeaderTitleService } from '../services/header-title-service';

@Component({
  selector: 'app-elite-journal-event-list',
  templateUrl: './elite-journal-event-list.component.html',
  styleUrls: ['./elite-journal-event-list.component.scss']
})
export class EliteJournalEventListComponent implements OnInit {

  constructor(private headerTitleService: HeaderTitleService) { }

  ngOnInit() {
    this.headerTitleService.setTitle('Elite Events')
  }

}
