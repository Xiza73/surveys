import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PersonService {
  private apiUrl: string = environment.apiUrl;

  constructor(private httpClient: HttpClient) {}

  getPeople(): Observable<any> {
    return this.httpClient.get(`${this.apiUrl}/Person`);
  }

  createPerson(person: any): Observable<any> {
    return this.httpClient.post(`${this.apiUrl}/Person`, person);
  }

}
