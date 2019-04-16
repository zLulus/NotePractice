import {Observable, of, Subject} from 'rxjs';
import { NzMessageService } from 'ng-zorro-antd';
import {HttpInterceptor, HttpHandler, HttpRequest, HttpEvent, HttpResponse, HttpHeaders} from '@angular/common/http';
import {Injectable, Injector} from '@angular/core';

export interface IValidationErrorInfo {
    message: string;
    members: string[];
}

export interface IErrorInfo {
    code: number;
    message: string;
    details: string;
    validationErrors: IValidationErrorInfo[];
}

export interface IAjaxResponse {
    success: boolean;
    result?: any;
    targetUrl?: string;
    error?: any;
    unAuthorizedRequest: boolean;
    __abp: boolean;
}

@Injectable()
export class AbpHttpConfiguration {
    constructor(private _messageService: NzMessageService) {
    }

    defaultError = {
        message: '产生了一个错误!',
        details: '服务器没有发送错误详情.'
    };
    defaultError401 = {
        message: '您未经过身份验证!',
        details: '为了执行此操作，您应该进行身份验证(登录).'
    };
    defaultError403 = {
        message: '您没有权限!',
        details: '您不能执行此操作.'
    };
    defaultError404 = {
        message: '未找到相关资源!',
        details: '服务器上找不到请求的资源.'
    };

    logError(error): void {
        console.log(error);
    }

    showError(error): any {
        if (error.details) {
            return this._messageService.error(error.details, error.message || this.defaultError.message);
        } else {
            return this._messageService.error(error.message || this.defaultError.message);
        }
    }

    handleTargetUrl(targetUrl: string): void {
        if (!targetUrl) {
            location.href = '/';
        } else {
            location.href = targetUrl;
        }
    }

    handleUnAuthorizedRequest(messagePromise: any, targetUrl?: string): void {
        const _this = this;
        const self = this;
        if (messagePromise) {
            messagePromise.done(function () {
                _this.handleTargetUrl(targetUrl || '/');
            });
        } else {
            self.handleTargetUrl(targetUrl || '/');
        }
    }

    handleNonAbpErrorResponse(response: HttpResponse<any>): void {
        const self = this;
        switch (response.status) {
            case 401:
                self.handleUnAuthorizedRequest(self.showError(self.defaultError401), '/');
                break;
            case 403:
                self.showError(self.defaultError403);
                break;
            case 404:
                self.showError(self.defaultError404);
                break;
            default:
                self.showError(self.defaultError);
                break;
        }
    }

    handleAbpResponse(response: HttpResponse<any>, ajaxResponse: IAjaxResponse): HttpResponse<any> {
        let newResponse;
        if (ajaxResponse.success) {
            newResponse = response.clone({
                body: ajaxResponse.result
            });
            if (ajaxResponse.targetUrl) {
                this.handleTargetUrl(ajaxResponse.targetUrl);
            }
        } else {
            newResponse = response.clone({
                body: ajaxResponse.result
            });
            if (!ajaxResponse.error) {
                ajaxResponse.error = this.defaultError;
            }
            this.logError(ajaxResponse.error);
            this.showError(ajaxResponse.error);
            if (response.status === 401) {
                this.handleUnAuthorizedRequest(null, ajaxResponse.targetUrl);
            }
        }
        return newResponse;
    }

    getAbpAjaxResponseOrNull(response: HttpResponse<any>): IAjaxResponse | null {
        if (!response || !response.headers) {
            return null;
        }
        const contentType = response.headers.get('Content-Type');
        if (!contentType) {
            // this._logService.warn('Content-Type is not sent!');
            console.warn('Content-Type is not sent!');
            return null;
        }
        if (contentType.indexOf('application/json') < 0) {
            // this._logService.warn('Content-Type is not application/json: ' + contentType);
            console.log('Content-Type is not application/json: ' + contentType);
            return null;
        }
        const responseObj = JSON.parse(JSON.stringify(response.body));
        if (!responseObj.__abp) {
            return null;
        }
        return responseObj;
    }

    handleResponse(response: HttpResponse<any>): HttpResponse<any> {
        const ajaxResponse = this.getAbpAjaxResponseOrNull(response);
        if (ajaxResponse == null) {
            return response;
        }
        return this.handleAbpResponse(response, ajaxResponse);
    }

    blobToText(blob: any): Observable<string> {
        return new Observable(function (observer) {
            if (!blob) {
                observer.next('');
                observer.complete();
            } else {
                const reader = new FileReader();
                reader.onload = function () {
                    observer.next(this.result);
                    observer.complete();
                };
                reader.readAsText(blob);
            }
        });
    }
}

@Injectable()
export class AbpHttpInterceptor implements HttpInterceptor {
    protected configuration: AbpHttpConfiguration;
    // private _tokenService;
    // private _utilsService;
    constructor(private configuration: AbpHttpConfiguration) {
       
    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const _this = this;
        const interceptObservable: Subject<HttpEvent<any>> = new Subject();
        const modifiedRequest = this.normalizeRequestHeaders(request);
        next.handle(modifiedRequest)
            .subscribe(function (event) {
                _this.handleSuccessResponse(event, interceptObservable);
            }, function (error) {
                return _this.handleErrorResponse(error, interceptObservable);
            });
        return interceptObservable;
    }

    protected normalizeRequestHeaders(request: HttpRequest<any>): HttpRequest<any> {
        let modifiedHeaders = new HttpHeaders();
        modifiedHeaders = request.headers.set('Pragma', 'no-cache')
            .set('Cache-Control', 'no-cache')
            .set('Expires', 'Sat, 01 Jan 2000 00:00:00 GMT');
        modifiedHeaders = this.addXRequestedWithHeader(modifiedHeaders);
        modifiedHeaders = this.addAuthorizationHeaders(modifiedHeaders);
        modifiedHeaders = this.addAspNetCoreCultureHeader(modifiedHeaders);
        modifiedHeaders = this.addAcceptLanguageHeader(modifiedHeaders);
        modifiedHeaders = this.addTenantIdHeader(modifiedHeaders);
        return request.clone({
            headers: modifiedHeaders
        });
    }

    protected addXRequestedWithHeader(headers: HttpHeaders): HttpHeaders {
        if (headers) {
            headers = headers.set('X-Requested-With', 'XMLHttpRequest');
        }
        return headers;
    }

    protected addAspNetCoreCultureHeader(headers: HttpHeaders): HttpHeaders {
        // todo: 如果需要再添加.AspNetCore.Culture
        /*const cookieLangValue = this._utilsService.getCookieValue('Abp.Localization.CultureName');
        if (cookieLangValue && headers && !headers.has('.AspNetCore.Culture')) {
            headers = headers.set('.AspNetCore.Culture', cookieLangValue);
        }*/
        return headers;
    }

    protected addAcceptLanguageHeader(headers: HttpHeaders): HttpHeaders {
        // todo: 如果需要再添加Accept-Language
        /*const cookieLangValue = this._utilsService.getCookieValue('Abp.Localization.CultureName');
        if (cookieLangValue && headers && !headers.has('Accept-Language')) {
            headers = headers.set('Accept-Language', cookieLangValue);
        }*/
        return headers;
    }

    protected addTenantIdHeader(headers: HttpHeaders): HttpHeaders {
        // todo: 如果需要再添加Abp.TenantId
        // const cookieTenantIdValue = this._utilsService.getCookieValue('Abp.TenantId');
        // if (cookieTenantIdValue && headers && !headers.has('Abp.TenantId')) {
        //     headers = headers.set('Abp.TenantId', cookieTenantIdValue);
        // }
        return headers;
    }

    protected addAuthorizationHeaders(headers: HttpHeaders): HttpHeaders {
        let authorizationHeaders = headers ? headers.getAll('Authorization') : null;
        if (!authorizationHeaders) {
            authorizationHeaders = [];
        }
        if (!this.itemExists(authorizationHeaders, function (item) {
                return item.indexOf('Bearer ') === 0;
            })) {
            /*const token = this._tokenService.getToken();
            if (headers && token) {
                headers = headers.set('Authorization', 'Bearer ' + token);
            }*/
        }
        return headers;
    }

    protected handleSuccessResponse(event: HttpEvent<any>, interceptObservable: Subject<HttpEvent<any>>): void {
        const self = this;
        if (event instanceof HttpResponse) {
            if (event.body instanceof Blob && event.body.type && event.body.type.indexOf('application/json') >= 0) {
                const clonedResponse = event.clone();
                self.configuration.blobToText(event.body).subscribe(function (json) {
                    const responseBody = json === 'null' ? {} : JSON.parse(json);
                    const modifiedResponse = self.configuration.handleResponse(event.clone({
                        body: responseBody
                    }));
                    interceptObservable.next(modifiedResponse.clone({
                        body: new Blob([JSON.stringify(modifiedResponse.body)], {type: 'application/json'})
                    }));
                    interceptObservable.complete();
                });
            } else {
                interceptObservable.next(event);
                interceptObservable.complete();
            }
        }
    }

    protected handleErrorResponse(error: any, interceptObservable: Subject<HttpEvent<any>>): Observable<any> {
        const _this = this;
        const errorObservable = new Subject();
        if (!(error.error instanceof Blob)) {
            interceptObservable.error(error);
            interceptObservable.complete();
            return of({});
        }
        this.configuration.blobToText(error.error).subscribe(function (json) {
            const errorBody = (json === '' || json === 'null') ? {} : JSON.parse(json);
            const errorResponse = new HttpResponse({
                headers: error.headers,
                status: error.status,
                body: errorBody
            });
            const ajaxResponse = _this.configuration.getAbpAjaxResponseOrNull(errorResponse);
            if (ajaxResponse != null) {
                _this.configuration.handleAbpResponse(errorResponse, ajaxResponse);
            } else {
                _this.configuration.handleNonAbpErrorResponse(errorResponse);
            }
            errorObservable.complete();
            // prettify error object.
            error.error = errorBody;
            interceptObservable.error(error);
            interceptObservable.complete();
        });
        return errorObservable;
    }

    private itemExists<T>(items, predicate) {
        for (let i = 0; i < items.length; i++) {
            if (predicate(items[i])) {
                return true;
            }
        }
        return false;
    }
}