import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NzMessageService } from 'ng-zorro-antd';

@Component({
  selector: 'app-form-set-dynamic-control',
  templateUrl: './form-set-dynamic-control.component.html',
})
export class FormSetDynamicControlComponent implements OnInit {

  properties = [];
  formTypes = [
    {id: 0, name: '登录'},
    {id: 1, name: '修改密码'},
    {id: 2, name: '选择城市'}
  ];
  selectFormTypeId: number;
  dataObj = {};
  validateForm: FormGroup;    // 表单校验
  cities = [
    {id: 0, name: '成都'},
    {id: 1, name: '北京'},
    {id: 2, name: '上海'},
  ];

  constructor(
    private fb: FormBuilder,
    private message: NzMessageService,
  ) { 
    this.validateForm = this.fb.group({

    });
  }

  ngOnInit() {
    this.Init();
  }

  Init() {
    this.selectFormTypeId=0;
    this.setFormType();
  }

  resetForm(): void{
    // 先清理之前的控件
    this.validateForm.clearValidators();
    this.dataObj={};
    this.properties.forEach(property => {
      // 根据新控件数组，插入控件
      this.validateForm.addControl(property.nameChain, this.fb.control(null, Validators.required));
    });
  }

  getData(): void {
    this.message.info(JSON.stringify(this.dataObj));
  }

  setFormType(): void{
    if(this.selectFormTypeId==0){
      this.properties=[
        {nameChain:'account',controlType:0,displayName:'账号'},
        {nameChain:'password',controlType:1,displayName:'密码'},
      ];
    }
    else if(this.selectFormTypeId==1){
      this.properties=[
        {nameChain:'oldPassword',controlType:1,displayName:'旧密码'},
        {nameChain:'newPassword',controlType:1,displayName:'新密码'},
        {nameChain:'againPassword',controlType:1,displayName:'再次输入'},
      ];
    }
    else if(this.selectFormTypeId==2){
      this.properties=[
        {nameChain:'selectCityId',controlType:2,displayName:'城市'},
      ];
    }
    this.resetForm();
  }
}
