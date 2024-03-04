import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../pages/shared/models/ApiResponse';
import { IRoom } from '../pages/shared/models/Room';

@Injectable({
  providedIn: 'root',
})
export class RoomService {
  apiurl = 'https://localhost:7156/api/Room';
  constructor(private http: HttpClient) { }

  getAllRooms(): Observable<ApiResponse<IRoom[]>> {
    return this.http.get<ApiResponse<IRoom[]>>(`${this.apiurl}/get-all-rooms`);
  }

  getRoom(id: string): Observable<ApiResponse<IRoom>> {
    return this.http.get<ApiResponse<IRoom>>(`${this.apiurl}/${id}`);
  }

  createRoom(room: IRoom): Observable<any> {
    return this.http.post(`${this.apiurl}`, room);
  }

  updateRoom(room: IRoom): Observable<any> {
    return this.http.put(`${this.apiurl}`, room);
  }

  deleteRoom(roomId: number): Observable<ApiResponse<any>> {
    return this.http.delete<ApiResponse<any>>(`${this.apiurl}/${roomId}`);
  }
}
