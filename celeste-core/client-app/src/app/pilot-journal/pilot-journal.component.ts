import { Component, OnInit } from '@angular/core';
import { HeaderTitleService } from '../services/header-title-service';

@Component({
  selector: 'app-pilot-journal',
  templateUrl: './pilot-journal.component.html',
  styleUrls: ['./pilot-journal.component.scss']
})
export class PilotJournalComponent implements OnInit {

  constructor(private headerTitleService: HeaderTitleService) { }

  ngOnInit() {
    this.headerTitleService.setTitle('Journal')
  }

}
