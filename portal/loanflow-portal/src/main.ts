import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { appConfig } from './app/app.config';
import { msalConfig } from './app/auth-config';
import { PublicClientApplication } from '@azure/msal-browser';

async function main() {
  const msalInstance = new PublicClientApplication(msalConfig);

  //  MSAL initialize
  await msalInstance.initialize();

  //  redirect response first
  try {
    await msalInstance.handleRedirectPromise();
  } catch (error) {
    console.error('Redirect error:', error);
  }

  // check login state
  const accounts = msalInstance.getAllAccounts();
  if (accounts.length === 0) {
    // No user logged in — redirect to login
    await msalInstance.loginRedirect();
    return; // Stop bootstrapping — redirecting now
  } else {
    msalInstance.setActiveAccount(accounts[0]);
  }

  // Now it's safe to bootstrap Angular
  await bootstrapApplication(AppComponent, appConfig);
}

main().catch(err => console.error(err));