import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-form-set-dynamic-control',
  templateUrl: './form-set-dynamic-control.component.html',
})
export class FormSetDynamicControlComponent implements OnInit {
  validateForm: FormGroup;    // 表单校验
  controlerType:number;
  controlerTypes=[
    {id:0,name:'输入框'},
    {id:1,name:'下拉框'},
    {id:2,name:'文本框'},
  ]

  properties=[
    {nameChain:'input'，controlType:0,displayName:'输入框'}
  ];
  constructor(private fb: FormBuilder) { }

  ngOnInit() {
    this.setControlerType();
  }

  setControlerType():void{
    // if(this.controlerType==0){
    //   this.properties=[
    //     {nameChain:'input'，controlType:0,displayName:'输入框'}
    //   ];
    // }
    // else if(this.controlerType==1){
    //   this.properties=[
    //     {nameChain:'select'，controlType:1,displayName:'下拉框'}
    //   ];
    // }
    // else if(this.controlerType==2){
    //   this.properties=[
    //     {nameChain:'textarea'，controlType:2,displayName:'文本框'}
    //   ];
    // }
    // // 先清理之前的控件
    // this.validateForm.clearValidators();
    // this.properties.forEach(property => {
    //   // 根据新控件数组，插入控件
    //   this.validateForm.addControl(property.nameChain, this.fb.control(null, Validators.required));
    // });
  }
}
