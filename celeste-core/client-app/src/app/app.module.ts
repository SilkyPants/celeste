import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { TopBarComponent } from './top-bar/top-bar.component';
import { NavRailComponent } from './nav-rail/nav-rail.component';
import { SettingsPageComponent } from './settings-page/settings-page.component';
import { RouteListComponent } from './route-list/route-list.component';
import { EliteJournalEventListComponent } from './elite-journal-event-list/elite-journal-event-list.component';
import { PilotJournalComponent } from './pilot-journal/pilot-journal.component';
import { StarRoutesService } from './services/star-routes.service';
import { HeaderTitleService } from './services/header-title.service';

import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { DashboardComponent } from './dashboard/dashboard.component';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { LayoutModule } from '@angular/cdk/layout'

@NgModule({
   declarations: [
      AppComponent,
      TopBarComponent,
      NavRailComponent,
      SettingsPageComponent,
      RouteListComponent,
      EliteJournalEventListComponent,
      PilotJournalComponent,
      DashboardComponent
   ],
   imports: [
      BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
      AppRoutingModule,
      FormsModule,
      HttpClientModule,
      BrowserAnimationsModule,
      MatButtonModule,
      MatTableModule,
      MatProgressSpinnerModule,
      MatInputModule,
      MatCardModule,
      MatGridListModule,
      MatMenuModule,
      MatIconModule,
      LayoutModule,
   ],
   providers: [
      StarRoutesService,
      HeaderTitleService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }