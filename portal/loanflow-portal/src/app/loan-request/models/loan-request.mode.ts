// src/app/models/loan-request.model.ts

/** All possible loan types */
export type LoanType =
  | 'Auto'      // Auto Loan
  | 'Personal'  // Personal Loan
  | 'Heloc';    // Home Equity Line of Credit

/** All possible statuses in our workflow */
export type LoanStatus =
  | 'Submitted'
  | 'Processing'
  | 'Approved'
  | 'Declined';

/** Core LoanRequest contract */
export interface LoanRequest {
  /** unique identifier (GUID) */
  id: string;

  /** who’s taking out the loan */
  customerName: string;

  /** principal amount in USD */
  amount: number;

  /** length of loan in months */
  termMonths: number;

  /** one of our defined types */
  type: LoanType;

  /** current state in the pipeline */
  status: LoanStatus;

  /** ISO timestamp when request was created */
  createdUtc: string;
}
