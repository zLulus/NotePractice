import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-router-link-page3',
  templateUrl: './router-link-page3.component.html',
})
export class RouterLinkPage3Component implements OnInit {
    page3Parameter:string;
  constructor(
    private _activatedroute:ActivatedRoute
  ) { }

  ngOnInit() {
    this.page3Parameter=this._activatedroute.snapshot.queryParams['page3Parameter'];
  }

}
