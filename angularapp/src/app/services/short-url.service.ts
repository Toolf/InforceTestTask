import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ShortUrlShortInfo } from '../models/short-url';
import { PaginationResponse } from '../models/pagination-response';
import { ShortUrlInfo } from '../models/short-url-info';

@Injectable({
  providedIn: 'root'
})
export class ShortUrlService {
  private apiUrl: string = "https://localhost:7098";

  constructor(private http: HttpClient) { }

  getShortUrls(page: number, perPage: number = 10): Observable<PaginationResponse<ShortUrlShortInfo>> {
    return this.http.get<PaginationResponse<ShortUrlShortInfo>>(`${this.apiUrl}/ShortUrl/list?Page=${page}&PerPage=${perPage}`);
  }

  delete(shortUrlId: number) {
    return this.http.delete<boolean>(`${this.apiUrl}/ShortUrl/?id=${shortUrlId}`);
  }

  getShortUrlById(shortUrlId: number): Observable<ShortUrlInfo> {
    return this.http.get<ShortUrlInfo>(`${this.apiUrl}/ShortUrl/${shortUrlId}`);
  }

  create(shortUrl: string) {
    return this.http.post<number>(`${this.apiUrl}/ShortUrl?url=${shortUrl}`, {});
  }
}