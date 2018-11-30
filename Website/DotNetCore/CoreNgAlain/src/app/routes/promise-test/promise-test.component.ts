import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl, AsyncValidatorFn, ValidationErrors } from '@angular/forms';
import { NzMessageService } from 'ng-zorro-antd';
import { TestService } from '../../api/test-service.service';

@Component({
  selector: 'app-promise-test',
  templateUrl: './promise-test.component.html',
})
export class PromiseTestComponent implements OnInit {
    constructor(private fb: FormBuilder,
        private msg: NzMessageService,
        private testService: TestService,
    )
    {
        
    }
    ngOnInit() {
        
    }

    async callNormal(){
        var r= await this.testService.callNormal();
        this.msg.info(r);
    }

    async callException(){
        var r= await this.testService.callException();
        this.msg.info(r);
    }
}