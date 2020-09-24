import { Injectable } from '@angular/core';
import {
  CanActivate,
  Router,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  CanActivateChild,
} from '@angular/router';

@Injectable()
export class CanActivateGuard implements CanActivate, CanActivateChild {

  constructor(
    private _router: Router,
  ) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot,
  ): boolean {
    //在这里判定是否跳转目标路由
    //如果可以跳转页面，返回true,不能，则返回false
    //建议逻辑
    //1.是否登录
    //2.访问是否要求权限
    //3.查询当前登录用户是否拥有目标权限
    //如果不符合条件，则根据selectBestRoute()方法返回其他页面

    //这里的permission是在routes-routing.module.ts中data:{permission:'yourPermission'}传参过来的内容
    console.log('该页面所需权限:'+route.data['permission']);
    
    return true;
    //参考
    // //未登录
    // if (!this._sessionService.user) {
    //   this._router.navigate(['/account/login']);
    //   return false;
    // }
    // //不要求权限
    // if (!route.data || !route.data['permission']) {
    //   return true;
    // }
    // //判定权限
    // if (this._permissionChecker.isGranted(route.data['permission'])) {
    //   return true;
    // }
    // let url=this.selectBestRoute();
    // this._router.navigate([url]);
    // return false;
  }

  canActivateChild(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot,
  ): boolean {
    return this.canActivate(route, state);
  }

  selectBestRoute(): string {
    //判定是否登录，没有则返回登录页
    //否则返回首页
    // if (!this._sessionService.user) {
    //   return '/account/login';
    // }

    return '/dashboard';
  }
}
