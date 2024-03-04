import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../pages/shared/models/ApiResponse';
import { ICurrency } from '../pages/shared/models/Currency';

@Injectable({
  providedIn: 'root',
})
export class CurrencyService {
  apiurl = 'https://localhost:7156/api/Currency';
  constructor(private http: HttpClient) { }

  getAllCurrencies(): Observable<ApiResponse<ICurrency[]>> {
    return this.http.get<ApiResponse<ICurrency[]>>(`${this.apiurl}/get-all-currencies`);
  }

  getCurrency(id: string): Observable<ApiResponse<ICurrency>> {
    return this.http.get<ApiResponse<ICurrency>>(`${this.apiurl}/${id}`);
  }

  createCurrency(currency: ICurrency): Observable<any> {
    return this.http.post(`${this.apiurl}`, currency);
  }

  updateCurrency(currency: ICurrency): Observable<any> {
    return this.http.put(`${this.apiurl}`, currency);
  }

  deleteCurrency(currencyId: number): Observable<ApiResponse<any>> {
    return this.http.delete<ApiResponse<any>>(`${this.apiurl}/${currencyId}`);
  }
}
