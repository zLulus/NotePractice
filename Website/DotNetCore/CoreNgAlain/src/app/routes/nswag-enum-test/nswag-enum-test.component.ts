import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { NzMessageService, NzModalService } from 'ng-zorro-antd';
import { EnumTestServiceProxy } from '../../../shared/service-proxies/service-proxies';

@Component({
  selector: 'app-nswag-enum-test',
  templateUrl: './nswag-enum-test.component.html',
})
export class NswagEnumTestComponent implements OnInit {
    member:any;
    person:any;

  constructor(
    private _enumTestServiceProxy: EnumTestServiceProxy
  ) { }

  ngOnInit() {
    this.fetchData();
  }

  fetchData(){
    this._enumTestServiceProxy.getMember()
    .subscribe((result)=>{
        this.member=result;
    });
    this._enumTestServiceProxy.getPerson()
    .subscribe((result)=>{
        this.person=result;
    })
    ;
  }
}
