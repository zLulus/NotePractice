import { Component, OnInit, EventEmitter, OnChanges, Input, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NzMessageService } from 'ng-zorro-antd';

@Component({
  selector: 'app-data-transfer-child',
  templateUrl: './data-transfer-child.component.html',
})
export class DataTransferChildComponent implements OnInit, OnChanges {
    // 输入
    @Input() data;
    // 输出
    @Output() dataChange: EventEmitter<any> = new EventEmitter();
    childValidateForm: FormGroup;
    constructor(private fb: FormBuilder
    )
    {
        this.childValidateForm = this.fb.group({
            time: [null], // 登记时间
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

    /**
     * @description  监听数据变化并返回给父组件
     */
    changeData=function(){
        // 因为子组件没有类似于提交的按钮，否则emit可以在点击按钮的时候调用，表单验证也可以放在子组件内
        this.data=Object.assign(this.data, this.childValidateForm.value);
        this.dataChange.emit({data:this.data,form:this.childValidateForm});
    }
}