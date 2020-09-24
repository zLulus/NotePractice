import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-echarts-baidu',
  templateUrl: './echarts-baidu.component.html',
})
export class EchartsBaiduComponent implements OnInit {
    //数据
    eChartDatas: any;
    //图例
    legends:any;

    //echart
    echarts: any;
    myChart: any;

  constructor(
  ) { }

  ngOnInit() {
    this.fetchData();
  }

  fetchData(): void{
    //通过接口查询数据
    //这里伪造数据
    this.eChartDatas =[
        [{"name":"2019-03-14 22:13:37","value":["2019-03-14 22:13:37","90.18"],"tip":"YG-YZQ250-001","symbol":"℃"},{"name":"2019-03-14 22:15:37","value":["2019-03-14 22:15:37","94.18"],"tip":"YG-YZQ250-001","symbol":"℃"},{"name":"2019-03-14 22:17:37","value":["2019-03-14 22:17:37","95.18"],"tip":"YG-YZQ250-001","symbol":"℃"}],
        [{"name":"2019-03-14 22:13:40","value":["2019-03-14 22:13:40","80.18"],"tip":"YG-YZQ250-002","symbol":"℃"},{"name":"2019-03-14 22:14:50","value":["2019-03-14 22:14:50","89.90"],"tip":"YG-YZQ250-002","symbol":"℃"},{"name":"2019-03-14 22:16:56","value":["2019-03-14 22:16:56","78.18"],"tip":"YG-YZQ250-002","symbol":"℃"}],
        [{"name":"2019-03-14 22:15:00","value":["2019-03-14 22:15:00","88.18"],"tip":"YG-YZQ250-003","symbol":"℃"},{"name":"2019-03-14 22:16:00","value":["2019-03-14 22:16:00","99.98"],"tip":"YG-YZQ250-003","symbol":"℃"},{"name":"2019-03-14 22:17:54","value":["2019-03-14 22:17:54","101.18"],"tip":"YG-YZQ250-003","symbol":"℃"}],
    ];
    this.legends =['YG-YZQ250-001','YG-YZQ250-002','YG-YZQ250-003'];

    // 基于准备好的dom，初始化echarts实例
    this.echarts = require('echarts');
    //只能初始化一次:https://www.echartsjs.com/api.html#echarts.init
    if (this.myChart == null || this.myChart == undefined) {
        this.myChart = this.echarts.init(document.getElementById('chart') as HTMLDivElement);
    }

    //绘制chart
    //时间统计图：https://echarts.baidu.com/examples/editor.html?c=dynamic-data2&qq-pf-to=pcqq.c2c
    // 指定图表的配置项和数据
    var option = {
        //标题
        title: {
        text: '监测数据统计图',
        // left: 'center'
        },
        //图例
        legend: {
            data: this.legends
        },
        tooltip: {
        trigger: 'axis',
        //划上去的显示文字
        // formatter: function (params) {
        //     params = params[0];
        //     var date = new Date(params.name);
        //     return date.getFullYear()  + '/' + (date.getMonth() + 1) + '/' + date.getDate() +' '+ date.getHours()+':'+date.getMinutes()+':'+date.getSeconds() + '    '+ params.data.tip + '  ' + params.value[1]+params.data.symbol;
        // },
        // axisPointer: {
        //   type: 'cross',
        //   label: {
        //       backgroundColor: '#6a7985'
        //   }
        // }
        },
        grid: {
        left: '3%',
        right: '4%',
        bottom: '3%',
        containLabel: true
        },
        toolbox: {
        feature: {
            saveAsImage: {}
        }
        },
        xAxis: {
        type: 'time',
        splitLine: {
            show: false
        }
        },
        yAxis: {
        type: 'value',
        splitLine: {
            show: false
        }
        },
        series: []
    };
    //循环录入数据
    this.eChartDatas.forEach(dataList => {
        option.series.push({
        name: dataList[0].tip,
        type: 'line',
        showSymbol: false,
        hoverAnimation: false,
        data: dataList
        });
    });

    // 使用刚指定的配置项和数据显示图表。
    this.myChart.clear();
    this.myChart.setOption(option);
  }
}
