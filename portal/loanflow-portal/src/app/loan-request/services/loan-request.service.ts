import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoanRequestDto } from '../models/loan-request.model';
import { environment } from '../../../environments/environment.prod';



@Injectable({ providedIn: 'root' })
export class LoanRequestService { 

  constructor(private http: HttpClient) {}

  /** GET all processed loans */
  getLoans(): Observable<LoanRequestDto[]> {
    return this.http.get<LoanRequestDto[]>(environment.apiLoansUrl);
  }

  createLoan(data: any): Observable<any> {
    return this.http.post(environment.apiLoansUrl, data);
  }
}
