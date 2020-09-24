import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-front-count',
  templateUrl: './front-count.component.html',
})
export class FrontCountComponent implements OnInit {
    data:any[];
  constructor(
  ) { }

  ngOnInit() {
    this.data=[];
    this.data.push({number:1,text:'1'});
    this.data.push({number:2,text:'1'});
    this.data.push({number:3,text:'1'});
    this.data.push({number:4,text:'1'});
    this.data.push({number:5,text:'1'});
    this.data.push({number:6,text:'1'});
    this.data.push({number:7,text:'1'});
    this.data.push({number:8,text:'1'});
    this.data.push({number:9,text:'1'});
    this.data.push({number:10,text:'1'});
  }

  getTitle(i){
    return "index="+i;
  }
}
