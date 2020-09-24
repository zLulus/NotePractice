import { NgModule } from '@angular/core';
import * as ApiServiceProxies from './service-proxies';

@NgModule({
  providers: [
    ApiServiceProxies.SwaggerTestServiceProxy,
    ApiServiceProxies.EnumTestServiceProxy,
    ApiServiceProxies.UserServiceProxy,
  ],
})
export class ServiceProxyModule { }
