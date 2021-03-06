import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import LoginComponent from './login/login.component';
import PageNotFoundComponent from './page-not-found/page-not-found.component';
import DashboardComponent from './shared/dashboard/dashboard.component';
import AuthGuard from './shared/guards/auth.guard';

const routes: Routes = [
  { path: '', component: DashboardComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  {
    path: 'bank-manager',
    loadChildren: () => import('src/app/bank-manager/bank-manager.module').then((m) => m.default),
  },
  {
    path: 'account-holder',
    loadChildren: () => import('src/app/account-holder/account-holder.module').then((m) => m.default),
  },
  { path: '**', component: PageNotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export default class AppRoutingModule { }
