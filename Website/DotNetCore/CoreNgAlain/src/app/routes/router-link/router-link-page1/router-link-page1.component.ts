import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-router-link-page1',
  styleUrls:['router-link-page1.component.less'],
  templateUrl: './router-link-page1.component.html',
})
export class RouterLinkPage1Component implements OnInit {
  parameter:string;
  parameter2:string;
  parameter3:string;
  page3Parameter:string;

  constructor(
  ) { }

  ngOnInit() {
    
  }

}
