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
import { FileUploadDefaulltComponent } from './file-upload/file-upload-defaullt/file-upload-defaullt.component';
import { FileUploadForCustomRequestComponent } from './file-upload/file-upload-for-custom-request/file-upload-for-custom-request.component';
import { ArrayRemoveComponent } from './array-remove/array-remove.component'; 
import { EchartsBaiduComponent } from './echarts-baidu/echarts-baidu.component'; 
import { RouterLinkPage1Component } from './router-link/router-link-page1/router-link-page1.component';
import { RouterLinkPage2Component } from './router-link/router-link-page2/router-link-page2.component';
import { RouterLinkPage3Component } from './router-link/router-link-page3/router-link-page3.component';
import { DrawerCallBackListComponent } from './drawer-call-back/drawer-call-back-list/drawer-call-back-list.component';
import { DrawerCallBackEditComponent } from './drawer-call-back/drawer-call-back-edit/drawer-call-back-edit.component';
import { NswagEnumTestComponent } from './nswag-enum-test/nswag-enum-test.component';
import { FrontCountComponent } from './front-count/front-count.component';
// 路由守卫
import { CanDeactivateGuardService } from './can-deactivate/can-deactivate-guard.service';
import { CanActivateGuard } from './can-activate/can-activate-guard';
// api变量
import { API_BASE_URL } from '../../shared/service-proxies/service-proxies';
import { environment } from '@env/environment';

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
  CanActivateComponent,
  FileUploadDefaulltComponent,
  FileUploadForCustomRequestComponent,
  ArrayRemoveComponent,
  EchartsBaiduComponent,
  RouterLinkPage1Component,
  RouterLinkPage2Component,
  RouterLinkPage3Component,
  DrawerCallBackListComponent,
  DrawerCallBackEditComponent,
  NswagEnumTestComponent,
  FrontCountComponent
];
const COMPONENTS_NOROUNT = [];

export function getApiBaseUrl(): string {
  return environment.SERVER_URL;
}

@NgModule({
  imports: [SharedModule, RouteRoutingModule],
  declarations: [...COMPONENTS, ...COMPONENTS_NOROUNT],
  entryComponents: COMPONENTS_NOROUNT,
  providers:[
    CanDeactivateGuardService,
    CanActivateGuard,
    { provide: API_BASE_URL, useFactory: getApiBaseUrl },
  ]
})
export class RoutesModule {}
