import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { SettingsPageComponent } from './settings-page/settings-page.component';
import { RouteListComponent } from './route-list/route-list.component';
import { EliteJournalEventListComponent } from './elite-journal-event-list/elite-journal-event-list.component';
import { PilotJournalComponent } from './pilot-journal/pilot-journal.component';
import { DashboardComponent } from './dashboard/dashboard.component';

const routes: Routes = [
  { path: '', component: DashboardComponent },
  { path: 'settings', component: SettingsPageComponent },

  { path: 'routes', component: RouteListComponent },

  { path: 'journal', component: PilotJournalComponent },

  { path: 'elite-events', component: EliteJournalEventListComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
