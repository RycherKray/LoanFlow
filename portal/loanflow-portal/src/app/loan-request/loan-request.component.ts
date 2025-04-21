import { Component, OnInit } from '@angular/core';
import { LoanRequestDto } from './models/loan-request.model';
import { LoanRequestService } from './services/loan-request.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-loan-request',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './loan-request.component.html',
  styleUrls: ['./loan-request.component.css']
})
export class LoanRequestComponent implements OnInit {
  loans: LoanRequestDto[] = [];
  showForm = false;

  newLoan = {
    customerName: '',
    amount: 0,
    termMonths: 0,
    type: 0
  };

  constructor(private loanService: LoanRequestService) {}

  ngOnInit() {
    this.fetchLoans();   
  }

  fetchLoans() {
    this.loanService.getLoans().subscribe({
      next: (loans) => (this.loans = loans),
      error: () => console.error('Could not load loans')
    });
  }

  submitLoan() {
    this.loanService.createLoan(this.newLoan).subscribe({
      next: () => {
        this.showForm = false;
        this.fetchLoans(); // refresh the table
        this.newLoan = { customerName: '', amount: 0, termMonths: 0, type: 0 };
      },
      error: () => console.error('Could not submit loan')
    });
  }
}
