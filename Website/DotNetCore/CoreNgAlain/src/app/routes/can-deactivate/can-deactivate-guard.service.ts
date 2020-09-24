import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {
    CanDeactivate,
    ActivatedRouteSnapshot,
    RouterStateSnapshot
} from '@angular/router';
import {CanDeactivateComponent} from './can-deactivate.component';


@Injectable()
export class CanDeactivateGuardService implements CanDeactivate<CanDeactivateComponent> {

    canDeactivate(component: CanDeactivateComponent,
                  route: ActivatedRouteSnapshot,
                  state: RouterStateSnapshot): Observable<boolean> | boolean {
        return component.leaveTip();
    }
}
