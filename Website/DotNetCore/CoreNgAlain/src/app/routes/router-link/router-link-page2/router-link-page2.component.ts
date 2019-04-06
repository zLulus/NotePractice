import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-router-link-page2',
  templateUrl: './router-link-page2.component.html',
})
export class RouterLinkPage2Component implements OnInit {
    parameter:string;
  constructor(
    private _activatedroute:ActivatedRoute
  ) { }

  ngOnInit() {
    this.parameter=this._activatedroute.snapshot.params['parameter'];
  }

}
