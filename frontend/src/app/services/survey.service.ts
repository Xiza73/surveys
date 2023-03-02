import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SurveyService {
  private apiUrl: string = environment.apiUrl;
  private surveyUrl: string = `${this.apiUrl}/Survey`;

  constructor(private httpClient: HttpClient) {}

  saveSurvey(survey: any): Observable<any> {
    return this.httpClient.post(`${this.surveyUrl}`, survey);
  }

  getSurveysByBranchId(branchId: number): Observable<any> {
    return this.httpClient.get(`${this.surveyUrl}/GetSurveysByBranch/${branchId}`);
  }
}
