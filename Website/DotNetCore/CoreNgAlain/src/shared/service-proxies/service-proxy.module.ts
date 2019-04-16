import { NgModule } from '@angular/core';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import * as ApiServiceProxies from './service-proxies';
import { AbpHttpConfiguration,AbpHttpInterceptor } from './abpHttpInterceptor';

@NgModule({
  providers: [
    AbpHttpConfiguration,
    ApiServiceProxies.SwaggerTestServiceProxy,
    ApiServiceProxies.EnumTestServiceProxy,
    ApiServiceProxies.UserServiceProxy,
    { provide: HTTP_INTERCEPTORS, useClass: AbpHttpInterceptor, multi: true },
  ],
})
export class ServiceProxyModule { }
