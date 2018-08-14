import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-name-chains',
  templateUrl: './name-chains.component.html',
})
export class NameChainsComponent implements OnInit {

    data={};
    dataStr:string;
    mergeResult:any;
    mergeResultStr:string;
    objPro2ProDicResult:any;
    objPro2ProDicResultStr:string;

    constructor() { }

    ngOnInit() {
        this.Test();
    }

    Test(): void {
        this.data.id=10;
        this.data.text={};
        this.data.text.title="title";
        this.data.text.content='content';
        this.data.image={};
        this.data.image.url='image url';
        this.data.image.name='image name';
        this.data.image.size='900KB';
        this.dataStr=JSON.stringify(this.data);
        this.objPro2ProDicResult=this.objPro2ProDic(this.data,undefined,undefined);
        console.log(this.objPro2ProDicResult);
        this.objPro2ProDicResultStr=JSON.stringify(this.objPro2ProDicResult);
        this.mergeResult=this.Merge(this.objPro2ProDicResult);
        console.log(this.mergeResult);
        this.mergeResultStr=JSON.stringify(this.mergeResult);
    }

    // 合并属性链
    Merge(items): any {
        var res = {}    
        for (let i = 0; i < items.length; i++) {
            var item = items[i];
            // key
            var prochains: string[] = item.key.split('.');//属性链
            var tmpObj = res;
            //组织属性
            for (let i = 0; i < prochains.length; i++) {
                const pro = prochains[i];        
                var islast = i == prochains.length - 1;
                //没有属性就创建为obj
                if (!tmpObj.hasOwnProperty(pro)) {
                    tmpObj[pro] = {}
                }
                else {
                    //检查一下,如果已经有属性了, 并且现在不是最后一个属性, 那么这个地方应该是obj,不是的话说明属性链有问题
                    if(typeof tmpObj[pro]!=="object"&&!islast){
                        tmpObj[pro] = {}            
                    }
                }
                //属性链最后一个要赋值
                if (islast) {
                    // value
                    tmpObj[pro] = item.value;
                }
                //属性下钻
                tmpObj = tmpObj[pro]
            }
        }
        return res;
    }
    // 还原属性链
    objPro2ProDic(obj, dic, root) {
        if (!dic) {
            dic = [];
        }
        for (const key in obj) {
            if (obj.hasOwnProperty(key)) {
                const ele = obj[key];
                if (typeof ele === "object" && !(ele instanceof Array)) {
                    //下钻
                    this.objPro2ProDic(ele, dic,key)
                }
                else{
                    var _key = root?`${root}.${key}`:key;
                    // 这里返回key-value
                    dic.push({
                        key:_key,
                        value:ele
                    });
                }
            }
        }
        return dic;
    }
}
