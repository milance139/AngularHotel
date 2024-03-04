import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../pages/shared/models/ApiResponse';
import { ICommittee } from '../pages/shared/models/Committee';

@Injectable({
  providedIn: 'root',
})
export class CommitteeService {
  apiurl = 'https://localhost:7156/api/Committee';
  constructor(private http: HttpClient) { }

  getAllCommittees(): Observable<ApiResponse<ICommittee[]>> {
    return this.http.get<ApiResponse<ICommittee[]>>(`${this.apiurl}/get-all-comittees`);
  }
}
