import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { environment } from '@env/environment';
// layout
import { LayoutDefaultComponent } from '../layout/default/default.component';
import { LayoutFullScreenComponent } from '../layout/fullscreen/fullscreen.component';
import { LayoutPassportComponent } from '../layout/passport/passport.component';

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
import { PromiseTestComponent } from './promise-test/promise-test.component';
import { DataTransferTwoComponent } from './data-transfer-two/data-transfer-two.component';
import { CanActivateComponent } from './can-activate/can-activate.component';
import { FileUploadDefaulltComponent } from './file-upload/file-upload-defaullt/file-upload-defaullt.component';
import { FileUploadForCustomRequestComponent } from './file-upload/file-upload-for-custom-request/file-upload-for-custom-request.component';
import { ArrayRemoveComponent } from './array-remove/array-remove.component'; 
import { EchartsBaiduComponent } from './echarts-baidu/echarts-baidu.component'; 
import { RouterLinkPage1Component } from './router-link/router-link-page1/router-link-page1.component';
import { RouterLinkPage2Component } from './router-link/router-link-page2/router-link-page2.component';
import { RouterLinkPage3Component } from './router-link/router-link-page3/router-link-page3.component';
import { DrawerCallBackListComponent } from './drawer-call-back/drawer-call-back-list/drawer-call-back-list.component';
import { NswagEnumTestComponent } from './nswag-enum-test/nswag-enum-test.component';
// 路由守卫
import { CanDeactivateGuardService } from './can-deactivate/can-deactivate-guard.service';
import { CanActivateGuard } from './can-activate/can-activate-guard';

const routes: Routes = [
  {
    path: '',
    component: LayoutDefaultComponent,
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      {path: 'dashboard', component: DashboardComponent},
      {path: 'can-deactivate', component: CanDeactivateComponent, canDeactivate: [CanDeactivateGuardService]},
      {path: 'form-set-dynamic-control', component: FormSetDynamicControlComponent},
      {path: 'data-transfer', component: DataTransferComponent},
      {path: 'promise-test', component: PromiseTestComponent},
      {path: 'data-transfer-two', component: DataTransferTwoComponent},
      {
        path: 'can-activate', 
        component: CanActivateComponent,
        //权限permission，CanActivateGuard判定
        data:{permission:'yourPermission',title: '你的标题'},
        //设置路由守卫为CanActivateGuard
        canActivate: [CanActivateGuard],
      },
      {path: 'file-upload-defaullt', component: FileUploadDefaulltComponent},
      {path: 'file-upload-for-custom-request', component: FileUploadForCustomRequestComponent},
      {path: 'array-remove', component: ArrayRemoveComponent},
      {path: 'echarts-baidu', component: EchartsBaiduComponent},
      {path: 'router-link-page1', component: RouterLinkPage1Component},
      {path: 'router-link-page2/:parameter/:parameter2/:parameter3', component: RouterLinkPage2Component},
      {path: 'router-link-page3', component: RouterLinkPage3Component},
      {path: 'drawer-call-back-list', component: DrawerCallBackListComponent},
      {path: 'nswag-enum-test', component: NswagEnumTestComponent},
    ],
  },
  // 单页不包裹Layout
  { path: 'callback/:type', component: CallbackComponent },
  { path: '403', component: Exception403Component },
  { path: '404', component: Exception404Component },
  { path: '500', component: Exception500Component },
  { path: '**', redirectTo: 'dashboard' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: environment.useHash })],
  exports: [RouterModule],
})
export class RouteRoutingModule {}
