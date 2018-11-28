import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { NzMessageService } from 'ng-zorro-antd';

@Component({
  selector: 'app-data-transfer',
  templateUrl: './data-transfer.component.html',
})
export class DataTransferComponent implements OnInit {
    tabIndex = 0; // 选中的tab
    validateForm: FormGroup;
    childValidateForm: FormGroup;
    data: any;
    constructor(private fb: FormBuilder,
        private msg: NzMessageService
    )
    {
        this.validateForm = this.fb.group({
            name: [null, [Validators.required]], // 姓名
            age: [null, [Validators.required, this.isMoreThanZero]], // 年龄
        });
    }
    ngOnInit() {
        this.data = {
            name:'',
            age:0,
            time:'2018/11/28'
        };
    }

    submit=function(){
        for (const i in this.validateForm.controls){
            if (this.validateForm.controls[i].errors != null) {
                this.msg.error('请确认表单输入');
                return;
            }
        }
        this.data=Object.assign(this.data, this.validateForm.value);
        // 输出
        let str=JSON.stringify(this.data);
        this.msg.success('当前数据:'+str);
    }

    /**
     * @description  获得输出
     */
    getTime=function(e){
        // 返回表单和数据
        // 这里的数据是在子组件里面emit的数据
        this.data.time=e.data.time;
        this.childValidateForm=e.childValidateForm;
    }

     /**
     * @description  自定义表单验证:是数字并且大于等于0
     */
    isMoreThanZero = (control: FormControl) => {
        if (!control.value) {
            return { required: true };
        } else if (isNaN(Number(control.value)) || control.value < 0) {
            // 注意，这里返回的是isMoreThanZero，才能对应.hasError('isMoreThanZero')
            return {  isMoreThanZero: true };
        }
    }
}