import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NzModalService } from 'ng-zorro-antd';
import config from '../../assets/config';

@Injectable()
export class RequestHelperService {
    constructor(private httpClient: HttpClient, private nzModalService: NzModalService) {
    }

    // 超时时间
    getTimeoutPromise(timeout = 1e3 * 60 * 2) {
        return new Promise((resolve, reject) => {
            setTimeout(() => {
                reject({message: '请求超时，请刷新重试'});
            }, timeout);
        });
    }

    getRequest(url: string, isShowError = true, options): Promise<any> {
        const pRequest = this.httpClient.get(url, options).toPromise().then((ret: any) => {
            return ret.result;
        });

        return Promise.race([this.getTimeoutPromise(), pRequest]).catch(ret => {
            this.handleRequestError(ret, isShowError);
        });
    }

    postRequest(url: string, body?: Object, options?: any, isShowError = true): Promise<any> {
        const pRequest = this.httpClient.post(url, body, options).toPromise().then((ret: any) => {
            if (ret.success == null) return this.processNotNormalRet(ret); // 如果是非正常的请求结果
            if (!ret.success) throw ret;
            return ret.result;
        });

        return Promise.race([this.getTimeoutPromise(), pRequest]).catch(ret => {
            this.handleRequestError(ret, isShowError);
        });
    }

    // 处理非正常请求
    processNotNormalRet = (ret) => {
        return ret;
    }

    handleRequestError(ret, isShowError = true) {
        // 如果不显示error 则直接返回结果
        if (!isShowError) throw ret;
        let title: string = '请求失败！';
        let content: string = ret.error && ret.error.error ? ret.error.error.details ? ret.error.error.details : ret.error.error.message : ret.message; // 错误信息
        let zIndex: number = 10000;
        let cancelText: string = '取消';
        let model: any;
        let onOk = (): boolean | void => {
            model.destroy();
            model = null;
        };
        if (ret.status === 401) {
            title = '请求失败';
            cancelText = '';
            zIndex = 99;
            onOk = () => {
                (<any>window).location = `${config.apiHost}/account/login?returnUrl=${encodeURIComponent(window.location.href)}`;
            };

            // 如果全屏loading还在 干掉
            if (document.querySelector('.preloader')) {
                (<HTMLElement>document.querySelector('.preloader')).style.display = 'none';
            }
        } else if (ret.status === 0) {
            content = '网络异常！请重试';
        }

        // if (ret.status === 0) {
        //     content = '网络异常！请重试';
        // }

        model = this.nzModalService.confirm({
            nzTitle: title,
            nzContent: content,
            nzOkText: '确定',
            nzMaskClosable: false,
            nzClosable: false,
            nzZIndex: zIndex,
            nzCancelText: cancelText,
            nzOnOk: onOk
        })
        throw {
            dialog: model,
            ret
        };
    }
}
