const util = require('../../utils/util.js')
const api = require('../../utils/api.js')

Page({
  data: {
    apiData:{},
    sessionKey: "",
  },
  onLoad: function () {
    
  },
  testApi: function() {
    var that = this;
    //todo
    util.getRequest('/AccountForMiniProgram/GetData', that, "apiData");
    //此处打日志是拿不到数据的，因为是异步请求
  },
  testScope1:function(){
    //this在外面
    var that = this;
    //没有绑定appId，这里返回的code是一个模拟code
    wx.login({
      success: function (res) {
        console.log(res)
        if (res.code) {
          //调用后端接口获得sessionkey
          //todo 
          util.postRequest('/AccountForMiniProgram/WechatGetSessionKey', { id: res.code }, that, "sessionKey");
        } else {
          console.log('登录失败！' + res.errMsg)
        }
      }
    });
  },
  testScope2:function(){
    //参考资料：http://jsrocks.org/cn/2014/10/arrow-functions-and-their-scope
    //=>
  },
  testScope3:function(){
    wx.login({
      success: function (res) {
        //this在里面
        var that = this;
        console.log(res)
        if (res.code) {
          //调用后端接口获得sessionkey
          util.postRequest('/AccountForMiniProgram/WechatGetSessionKey', { id: res.code }, that, "sessionKey");
        } else {
          console.log('登录失败！' + res.errMsg)
        }
      }
    });
  },
})
