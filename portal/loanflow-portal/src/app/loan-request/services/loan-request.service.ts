import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoanRequestDto } from '../models/loan-request.model';
import { environment } from '../../../environments/environment.prod';



@Injectable({ providedIn: 'root' })
export class LoanRequestService {
  private readonly baseUrl = `${environment.production}/loans`;

  constructor(private http: HttpClient) {}

  /** GET all processed loans */
  getLoans(): Observable<LoanRequestDto[]> {
    return this.http.get<LoanRequestDto[]>(this.baseUrl);
  }

  createLoan(data: any): Observable<any> {
    return this.http.post(this.baseUrl, data);
  }
}
