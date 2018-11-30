import { Component, OnInit, EventEmitter, OnChanges, Input, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NzMessageService } from 'ng-zorro-antd';

@Component({
  selector: 'app-data-transfer-two-child',
  templateUrl: './data-transfer-two-child.component.html',
})
export class DataTransferTwoChildComponent implements OnInit, OnChanges {
    // 输入
    @Input() data;
    childValidateForm: FormGroup;
    constructor(private fb: FormBuilder
    )
    {
        this.childValidateForm = this.fb.group({
            time: [null, [Validators.required]], // 登记时间
        });
    }

    ngOnChanges() {
        // 不加延时器，会导致dom未加载前patchValue报错
        setTimeout(async()=>{
            if(this.data!=null){
                this.childValidateForm.patchValue({
                    time: this.data.time,
                });
            }
        }, 0)
    }

    ngOnInit() {
        
    }
}