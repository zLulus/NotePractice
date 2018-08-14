import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { FormSetDynamicControlComponent } from './form-set-dynamic-control/form-set-dynamic-control.component';
import { CanDeactivateComponent } from './can-deactivate/can-deactivate.component';
import { CanDeactivateGuardService } from './can-deactivate/can-deactivate-guard.service';

const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'form-set-dynamic-control', component: FormSetDynamicControlComponent },
  { path: 'can-deactivate', component: CanDeactivateComponent, canDeactivate: [CanDeactivateGuardService] },
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ],
  declarations: [],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }
