import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-array-remove',
  templateUrl: './array-remove.component.html',
})
export class ArrayRemoveComponent implements OnInit {
    tags1:any[];
    tags2:any[];
    tags3:any[];
    tagsStr1:string;
    tagsStr2:string;
    tagsStr3:string;
  constructor(
  ) { }

  ngOnInit() {
    this.initData();
  }

  initData(){
    this.tags1=[];
    this.tags2=[];
    this.tags3=[];
    for(var i=0;i<10;i++){
      this.tags1.push(i);
      this.tags2.push(i);
      this.tags3.push(i);
    }
    this.getTagsStr();
  }

  //参考资料:https://stackoverflow.com/questions/15292278/how-do-i-remove-an-array-item-in-typescript

  //method 1
  onClose1(removedTag: {}): void{
    this.tags1 = this.tags1.filter(tag => tag !== removedTag);
  }

  //method 2
  onClose2(removedTag: {}): void {
    const index = this.tags2.indexOf(removedTag, 0);
    if (index > -1) {
        this.tags2.splice(index, 1);
    }
    
  }

  //method 3 这种方式会产生empty占位
  onClose3(removedTag: {}): void {
    const index = this.tags3.indexOf(removedTag, 0);
    if (index > -1) {
        delete this.tags3[index];
    }
  }

  getTags():void{
    console.log('1');
    console.log(this.tags1);
    console.log('2');
    console.log(this.tags2);
    console.log('3');
    console.log(this.tags3);
    this.getTagsStr();
  }

  getTagsStr(){
    this.tagsStr1='';
    this.tags1.forEach(tag => {
      this.tagsStr1+=tag+','
    });

    this.tagsStr2='';
    this.tags2.forEach(tag => {
      this.tagsStr2+=tag+','
    });

    this.tagsStr3='';
    //foreach的时候略过了empty
    this.tags3.forEach(tag => {
      this.tagsStr3+=tag+','
    });
  }
}
