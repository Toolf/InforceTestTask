import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { LoginComponent } from './pages/login/login.component';
import { RegistryComponent } from './pages/registry/registry.component';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { authInterceptorProviders } from './helpers/auth.interceptor';
import { ShortUrlsTableComponent } from './pages/short-urls-table/short-urls-table.component';
import { ShortUrlInfoComponent } from './pages/short-url-info/short-url-info.component';
import { AddNewUrlComponent } from './pages/add-new-url/add-new-url.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegistryComponent,
    ShortUrlsTableComponent,
    ShortUrlInfoComponent,
    AddNewUrlComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [
    authInterceptorProviders
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
