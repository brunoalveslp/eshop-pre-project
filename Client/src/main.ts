
import { enableProdMode } from '@angular/core';

// Just in time compilation - larger files
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { environment } from './environments/environment.development';

// Ahead of time compilation - smaller files
// import { platformBrowser } from '@angular/platform-browser';

// Enable production Mode
if(environment.production){
  enableProdMode();
}

import { AppModule } from './app/app.module';


platformBrowserDynamic().bootstrapModule(AppModule)
  .catch(err => console.error(err));
