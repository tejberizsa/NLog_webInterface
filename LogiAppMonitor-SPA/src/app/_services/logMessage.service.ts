import { Injectable } from '@angular/core';
import { LogMessage } from '../_models/logMessage';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LogMessageService {
  baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }

getPosts(page?, itemsPerPage?): Observable<PaginatedResult<LogMessage[]>> {
  const paginatedResult: PaginatedResult<LogMessage[]> = new PaginatedResult<LogMessage[]>();

  let params = new HttpParams();

  if (page != null && itemsPerPage != null) {
    params = params.append('pageNumber', page);
    params = params.append('pageSize', itemsPerPage);
  }

  return this.http.get<LogMessage[]>(this.baseUrl + 'logger', { observe: 'response', params})
  .pipe(
    map(response => {
      paginatedResult.result = response.body;
      if (response.headers.get('Pagination') != null) {
        paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
      }
      return paginatedResult;
    })
  );
}
}
