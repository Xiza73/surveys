import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class BranchService {
  private apiUrl: string = environment.apiUrl;
  private branchUrl: string = `${this.apiUrl}/Branch`;

  constructor(private httpClient: HttpClient) {}

  getBranches(): Observable<any> {
    return this.httpClient.get(`${this.branchUrl}`);
  }
}
