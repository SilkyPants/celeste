/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { EliteJournalEventListComponent } from './elite-journal-event-list.component';

describe('EliteJournalEventListComponent', () => {
  let component: EliteJournalEventListComponent;
  let fixture: ComponentFixture<EliteJournalEventListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EliteJournalEventListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EliteJournalEventListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
