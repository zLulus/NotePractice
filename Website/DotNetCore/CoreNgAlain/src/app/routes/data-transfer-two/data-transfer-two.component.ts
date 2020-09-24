import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl, AsyncValidatorFn, ValidationErrors } from '@angular/forms';
import { NzMessageService } from 'ng-zorro-antd';
import { TestService } from '../../api/test-service.service';
import { DataTransferTwoChildComponent } from './data-transfer-two-child/data-transfer-two-child.component';

@Component({
  selector: 'app-data-transfer-two',
  templateUrl: './data-transfer-two.component.html',
})
export class DataTransferTwoComponent implements OnInit {
    tabIndex = 0; // 选中的tab
    validateForm: FormGroup;
    childValidateForm: FormGroup;
    data: any;
    // 根据前端的命名获得组件
    @ViewChild('dataTransferTwoChild') compDataTransferTwoChild: DataTransferTwoChildComponent; // 子组件
    constructor(private fb: FormBuilder,
        private msg: NzMessageService,
        private testService: TestService,
    )
    {
        this.validateForm = this.fb.group({
            code: [null, [Validators.required], [this.checkData]], // 编码
            name: [null, [Validators.required]], // 姓名
            age: [null, [Validators.required, this.isMoreThanZero]], // 年龄
        });
    }
    ngOnInit() {
        this.data = {
            code:'',
            name:'',
            age:0,
            time:''
        };
    }

    submit=function(){
        for (const i in this.validateForm.controls){
            if (this.validateForm.controls[i].errors != null) {
                this.msg.error('请确认表单输入');
                return;
            }
        }
        // 验证子组件表单
        for (const i in this.compDataTransferTwoChild.childValidateForm.controls){
            if (this.compDataTransferTwoChild.childValidateForm.controls[i].errors != null) {
                this.msg.error('请确认表单输入');
                return;
            }
        }
        this.data=Object.assign(this.data, this.validateForm.value);
        // 获得子组件数据
        let childData=Object.assign(this.data, this.compDataTransferTwoChild.childValidateForm.value);
        this.data.time=childData.time;
        // 输出
        let str=JSON.stringify(this.data);
        this.msg.success('当前数据:'+str);
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

    /**
     * @description  自定义表单验证:查询编码是否重复
     */
    checkData: AsyncValidatorFn = (control: FormControl): Promise<ValidationErrors | null> =>{
        return new Promise((resolve2) => {
            setTimeout(() => {
                this.testService.checkData({code:control.value})
                    .then((response: any) => {
                        if (response) {
                            resolve2({existSameCode: true});
                        } else {
                            resolve2(null);
                        }
                    });
            }, 1500);
        });
    }
}