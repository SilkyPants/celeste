/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { PilotJournalComponent } from './pilot-journal.component';

describe('PilotJournalComponent', () => {
  let component: PilotJournalComponent;
  let fixture: ComponentFixture<PilotJournalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PilotJournalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PilotJournalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
