import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class QuestionService {
  private apiUrl: string = environment.apiUrl;
  private questionUrl: string = `${this.apiUrl}/Question`;

  constructor(private httpClient: HttpClient) {}

  getQuestions(): Observable<any> {
    return this.httpClient.get(`${this.questionUrl}`);
  }

}
