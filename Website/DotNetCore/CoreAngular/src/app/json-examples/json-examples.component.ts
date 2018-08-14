import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-json-examples',
  templateUrl: './json-examples.component.html',
})
export class JsonExamplesComponent implements OnInit {

    data={
        id:10,
        text:{
            title:'title',
            content:'content'
        },
        image:{
            url:'image url',
            name:'image name',
            size:'900KB'
        }
    };
    dataStr:string;
    convertData:any;

    constructor() { }

    ngOnInit() {
        this.Test();
    }

    Test(): void {
        this.dataStr=JSON.stringify(this.data);
        console.log('JSON.stringify(any)');
        console.log(this.dataStr);
        this.convertData=JSON.parse(this.dataStr);
        console.log('JSON.parse(string)');
        console.log(this.convertData);
    }

}
