import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NewSurveyComponent } from './pages/new-survey/new-survey.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CreatePersonModalComponent } from './pages/new-survey/components/create-person-modal/create-person-modal.component';
import { ReportsComponent } from './pages/reports/reports.component';
import { HttpClientModule } from '@angular/common/http';
import { HeaderComponent } from './shared/components/header/header.component';

@NgModule({
  declarations: [
    AppComponent,
    NewSurveyComponent,
    CreatePersonModalComponent,
    ReportsComponent,
    HeaderComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
