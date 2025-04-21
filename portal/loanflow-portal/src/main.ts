import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { appConfig } from './app/app.config';
import { msalConfig } from './app/auth-config';
import { PublicClientApplication } from '@azure/msal-browser';

async function main() {
  const msalInstance = new PublicClientApplication(msalConfig);

  // ⏳ Wait for MSAL to initialize
  await msalInstance.initialize();

  // Optional: Check session and redirect to login
  const accounts = msalInstance.getAllAccounts();
  const active = msalInstance.getActiveAccount();
  if (accounts.length === 0 && !active) {
    await msalInstance.loginRedirect();
  }

  // 🚀 Now it's safe to bootstrap Angular
  await bootstrapApplication(AppComponent, appConfig);
}

main().catch(err => console.error(err));