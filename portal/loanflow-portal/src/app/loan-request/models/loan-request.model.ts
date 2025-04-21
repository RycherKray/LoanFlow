export interface LoanRequestDto {
  id: string;
  customerName: string;
  amount: number;
  termMonths: number;
  type: number;
  status: string;
  processedAt: string;
}