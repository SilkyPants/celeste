import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MatButtonModule } from '@angular/material/button'

import { TopBarComponent } from './top-bar/top-bar.component';
import { ProductListComponent } from './product-list/product-list.component';
import { NavRailComponent } from './nav-rail/nav-rail.component';
import { SettingsPageComponent } from './settings-page/settings-page.component';
import { RouteListComponent } from './route-list/route-list.component';
import { EliteJournalEventListComponent } from './elite-journal-event-list/elite-journal-event-list.component';
import { PilotJournalComponent } from './pilot-journal/pilot-journal.component';

@NgModule({
   declarations: [
      AppComponent,
      TopBarComponent,
      ProductListComponent,
      NavRailComponent,
      SettingsPageComponent,
      RouteListComponent,
      EliteJournalEventListComponent,
      PilotJournalComponent
   ],
   imports: [
   BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
	 AppRoutingModule,
	 FormsModule,
	 HttpClientModule,
	 BrowserAnimationsModule,
	 MatButtonModule
	],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }