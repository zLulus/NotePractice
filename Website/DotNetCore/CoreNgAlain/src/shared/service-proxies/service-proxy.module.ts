import { NgModule } from '@angular/core';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

import * as ApiServiceProxies from './service-proxies';

@NgModule({
  providers: [
    ApiServiceProxies.SwaggerTestServiceProxy,
    ApiServiceProxies.EnumTestServiceProxy,
    ApiServiceProxies.UserServiceProxy
  ],
})
export class ServiceProxyModule { }
