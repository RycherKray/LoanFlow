import {
    MsalInterceptorConfiguration,
    MsalGuardConfiguration
  } from '@azure/msal-angular';
  import {
    LogLevel,
    Configuration,
    BrowserCacheLocation,
    InteractionType
  } from '@azure/msal-browser';
  
  export const msalConfig: Configuration = {
    auth: {
      clientId: '7ec95597-5cd7-4d53-b1bb-da5e203b32d4',
      authority: 'https://loanflowb2c.b2clogin.com/loanflowb2c.onmicrosoft.com/B2C_1_signup_signin',
      knownAuthorities: ['loanflowb2c.b2clogin.com'],
      redirectUri: 'https://icy-forest-0aa10540f.6.azurestaticapps.net/home',
      postLogoutRedirectUri: 'https://icy-forest-0aa10540f.6.azurestaticapps.net/home'
    },
    cache: {
      cacheLocation: BrowserCacheLocation.LocalStorage,
      storeAuthStateInCookie: false
    },
    system: {
      loggerOptions: {
        loggerCallback: (level, message) => {
          if (level === LogLevel.Info || level === LogLevel.Error) console.log(message);
        },
        logLevel: LogLevel.Info,
        piiLoggingEnabled: false
      }
    }
  };
  
  export const msalGuardConfig: MsalGuardConfiguration = {
    interactionType: InteractionType.Redirect,
    authRequest: {
      scopes: [     
        'https://loanflowb2c.onmicrosoft.com/api/access'
      ]
    }
  };
  
  export const msalInterceptorConfig: MsalInterceptorConfiguration = {
    interactionType: InteractionType.Redirect,
    protectedResourceMap: new Map([
      [
        'https://loanflow-api-gkb2duebc2b6ctgy.brazilsouth-01.azurewebsites.net/api/loans',
        ['https://loanflowb2c.onmicrosoft.com/api/access']
      ]
    ])
  };
  