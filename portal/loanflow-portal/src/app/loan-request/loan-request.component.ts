import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule }  from '@angular/forms';

@Component({
  selector: 'app-loan-request',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './loan-request.component.html',
  styleUrls: ['./loan-request.component.css']
})
export class LoanRequestComponent {
  // --- form fields ---
  customerName = '';
  amount = 1000;
  termMonths = 12;
  type: 'Auto' | 'Personal' | 'Heloc' = 'Auto';

  // --- UI state ---
  isLoading = false;
  result = '';
  error = '';

  submit(): void {
    // basic client‑side validation
    if (!this.customerName.trim()) {
      this.error = 'Please enter your name.';
      return;
    }
    this.error = '';
    this.result = '';
    this.isLoading = true;

    // simulate async call
    setTimeout(() => {
      this.isLoading = false;
      this.result = `✅ Loan for ${this.customerName} ($${this.amount.toFixed(
        2
      )}, ${this.termMonths} mo, ${this.type}) submitted!`;
    }, 800);
  }
}
