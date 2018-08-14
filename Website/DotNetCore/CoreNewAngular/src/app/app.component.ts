import { Component,OnInit } from '@angular/core';
// import { Menu, MenuService, SettingsService, TitleService } from '@delon/theme';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less']
})
export class AppComponent implements OnInit{
  title = 'Angular Demo';

  constructor(
    // private menuService: MenuService
  ) { }

  ngOnInit() {
    this.setMenu();
  }

  setMenu(): void{
    // todo menu
    // let subMenu: Menu;
    // subMenu = {
    //   text: 'dashboard',
    //   link: '/dashboard',
    //   icon: `icon iconfont icon-dashboard`
    // };
    // const arrMenu = new Array<Menu>();
    // arrMenu.push(subMenu);
    // this.menuService.add(arrMenu);
  }
}
