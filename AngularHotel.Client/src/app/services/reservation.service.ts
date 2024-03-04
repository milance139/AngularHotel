import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../pages/shared/models/ApiResponse';
import { IReservation } from '../pages/shared/models/Reservation';
import { IArchivedReservation } from '../pages/shared/models/response/archivedReservation.model';

@Injectable({
  providedIn: 'root',
})
export class ReservationService {
  apiurl = 'https://localhost:7156/api/Reservation';
  constructor(private http: HttpClient) { }

  getAllReservations(): Observable<ApiResponse<IReservation[]>> {
    return this.http.get<ApiResponse<IReservation[]>>(`${this.apiurl}/get-all-reservations`);
  }

  getArchivedReservations(): Observable<ApiResponse<IArchivedReservation[]>> {
    return this.http.get<ApiResponse<IArchivedReservation[]>>(`${this.apiurl}/get-all-archived-reservations`);
  }

  getReservation(id: string): Observable<ApiResponse<IReservation>> {
    return this.http.get<ApiResponse<IReservation>>(`${this.apiurl}/${id}`);
  }

  createReservation(reservation: IReservation): Observable<any> {
    return this.http.post(`${this.apiurl}`, reservation);
  }

  updateReservation(reservation: IReservation): Observable<any> {
    return this.http.put(`${this.apiurl}`, reservation);
  }

  deleteReservation(reservationId: number): Observable<ApiResponse<any>> {
    return this.http.delete<ApiResponse<any>>(`${this.apiurl}/${reservationId}`);
  }

}
