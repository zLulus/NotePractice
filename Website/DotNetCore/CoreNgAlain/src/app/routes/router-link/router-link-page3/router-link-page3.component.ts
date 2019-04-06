import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-router-link-page3',
  templateUrl: './router-link-page3.component.html',
})
export class RouterLinkPage3Component implements OnInit {
    parameter2:string;
  constructor(
    private _activatedroute:ActivatedRoute
  ) { }

  ngOnInit() {
    this.parameter2=this._activatedroute.snapshot.queryParams['parameter2'];
  }

}
