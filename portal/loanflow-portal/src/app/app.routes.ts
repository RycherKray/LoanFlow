import { Routes } from '@angular/router';
import { LoanRequestComponent } from './loan-request/loan-request.component';
import { LoginComponent } from './login/login.component';
import { MsalGuard } from '@azure/msal-angular';

export const routes: Routes = [
    { path: 'home', component: LoginComponent, canActivate: [MsalGuard] },
    { path: 'loans', component: LoanRequestComponent, canActivate: [MsalGuard] },
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: '**', redirectTo: 'home' } // handles unknown routes
  ];
