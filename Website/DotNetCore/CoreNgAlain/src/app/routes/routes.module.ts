import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';
import { RouteRoutingModule } from './routes-routing.module';
// single pages
import { CallbackComponent } from './callback/callback.component';
import { Exception403Component } from './exception/403.component';
import { Exception404Component } from './exception/404.component';
import { Exception500Component } from './exception/500.component';

// 业务
import { DashboardComponent } from './dashboard/dashboard.component';
import { CanDeactivateComponent } from './can-deactivate/can-deactivate.component';
import { FormSetDynamicControlComponent } from './form-set-dynamic-control/form-set-dynamic-control.component';
import { DataTransferComponent } from './data-transfer/data-transfer.component';
import { DataTransferChildComponent } from './data-transfer/data-transfer-child/data-transfer-child.component';
import { PromiseTestComponent } from './promise-test/promise-test.component';
import { DataTransferTwoComponent } from './data-transfer-two/data-transfer-two.component';
import { DataTransferTwoChildComponent } from './data-transfer-two/data-transfer-two-child/data-transfer-two-child.component';
import { CanActivateComponent } from './can-activate/can-activate.component';
// 路由守卫
import { CanDeactivateGuardService } from './can-deactivate/can-deactivate-guard.service';
import { CanActivateGuard } from './can-activate/can-activate-guard';

const COMPONENTS = [
  CallbackComponent,
  Exception403Component,
  Exception404Component,
  Exception500Component,
  DashboardComponent,
  CanDeactivateComponent,
  FormSetDynamicControlComponent,
  DataTransferComponent,
  DataTransferChildComponent,
  PromiseTestComponent,
  DataTransferTwoComponent,
  DataTransferTwoChildComponent,
  CanActivateComponent
];
const COMPONENTS_NOROUNT = [];

@NgModule({
  imports: [SharedModule, RouteRoutingModule],
  declarations: [...COMPONENTS, ...COMPONENTS_NOROUNT],
  entryComponents: COMPONENTS_NOROUNT,
  providers:[
    CanDeactivateGuardService,
    CanActivateGuard
  ]
})
export class RoutesModule {}
