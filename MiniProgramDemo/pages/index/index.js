const util = require('../../utils/util.js')
const api = require('../../utils/api.js')

Page({
  data: {
    apiData:{},
  },
  onLoad: function () {
    
  },
  testApi: function(e) {
    var that = this;
    util.getRequest('/Account/LogInForMiniProgram', that, "apiData");
    //此处打日志是拿不到数据的，因为是异步请求
  }
})
