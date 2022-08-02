import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { LineGraphData, LineGraphDataResponse } from '../models/line-graph-data';

@Injectable({
  providedIn: 'root',
})
export default class TransactionService {
  constructor(private httpClient: HttpClient) { }

  getLast12MonthBalances(accountId?: string): Observable<LineGraphDataResponse> {
    if (accountId === null) {
      return this.httpClient.get<LineGraphDataResponse>(`${environment.apiUrlBase}Transaction/GetLast12MonthBalances`);
    }
    return this.httpClient.get<LineGraphDataResponse>(`${environment.apiUrlBase}Transaction/GetLast12MonthBalances/${accountId}`);
  }
}
