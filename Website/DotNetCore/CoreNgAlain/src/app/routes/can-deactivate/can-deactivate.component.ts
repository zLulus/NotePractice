import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { NzMessageService, NzModalService } from 'ng-zorro-antd';

@Component({
  selector: 'app-can-deactivate',
  templateUrl: './can-deactivate.component.html',
})
export class CanDeactivateComponent implements OnInit {

  isSave:false;
  constructor(
    private modalService: NzModalService
  ) { }

  ngOnInit() {

  }

  leaveTip() {
    return Observable.create((observer) => {
      if(!this.isSave){
        this.modalService.confirm({
          nzTitle: '页面离开提示',
          nzContent: '数据尚未保存，是否离开该页面？',
          nzOnOk: async () => {
              observer.next(true);
          },
          nzOnCancel: () => {
              observer.next(false);
          }
        });
      }
      else{
        observer.next(true);
      }
    });
  }
}
