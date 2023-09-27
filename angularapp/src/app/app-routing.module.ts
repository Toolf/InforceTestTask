import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { RegistryComponent } from './pages/registry/registry.component';
import { ShortUrlsTableComponent } from './pages/short-urls-table/short-urls-table.component';
import { ShortUrlInfoComponent } from './pages/short-url-info/short-url-info.component';
import { AddNewUrlComponent } from './pages/add-new-url/add-new-url.component';

const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'registry', component: RegistryComponent },
    { path: 'short-urls', component: ShortUrlsTableComponent },
    { path: 'short-urls/:id', component: ShortUrlInfoComponent },
    { path: 'add-new-url', component: AddNewUrlComponent },
    { path: '', redirectTo: '/login', pathMatch: 'full' }, // Default route
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule { }