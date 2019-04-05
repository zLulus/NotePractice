import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-array-remove',
  templateUrl: './array-remove.component.html',
})
export class ArrayRemoveComponent implements OnInit {
    tags:any[];

  constructor(
  ) { }

  ngOnInit() {
    this.initData();
  }

  initData(){
    this.tags=[];
    for(var i=0;i<10;i++){
    this.tags.push(i);
    }
    
  }

  onClose(removedTag: {}): void {
    //https://stackoverflow.com/questions/15292278/how-do-i-remove-an-array-item-in-typescript
    //method 1
    // this.tags = this.tags.filter(tag => tag !== removedTag);
    //method 2
    const index = this.tags.indexOf(removedTag, 0);
    if (index > -1) {
        this.tags.splice(index, 1);
    }
    //method 3 这种方式会产生empty占位
    // const index = this.tags.indexOf(removedTag, 0);
    // if (index > -1) {
    //     delete this.tags[index];
    // }
    console.log('tag was closed.');
  }

  getTags():void{
    console.log(this.tags);
  }
}
