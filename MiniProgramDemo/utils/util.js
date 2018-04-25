var apiHost = "http://localhost:1107/";
var tokenKey = "token";
var logInUrl = "/Account/LogInForMiniProgram";

//url添加最后的相对路径即可
function getRequest(url, that, targetName) {
  wx.request({
    url: apiHost + url,
    method: 'GET',
    header: {
      'content-type': 'application/json' // 默认值
    },
    success: function (res) {
      var param = {};
      param[targetName] = res.data;
      that.setData(param);
    },
    fail: function (error) {
      console.log(error);
    }
  })
}

//url添加最后的相对路径即可
function postRequest(url, data, that, targetName, func, statusName, getfailFunc, ) {
  var token = "";
  //无论是否拿到token，都去请求接口（eg.调用登录接口无需token）
  wx.getStorage({
    key: tokenKey,
    success: function (res) {
      token = res.data;
      console.log(res)
    },
    fail: function (error) {
      console.log(error);
    },
    complete: function () {
      console.log(url);
      wx.request({
        url: apiHost + url,
        data: data,
        method: 'POST',
        header: {
          'content-type': 'application/json', // 默认值
          'Authorization': "Bearer " + token
        },
        success: function (res) {
          var param = {};
          param[targetName] = res.data;
          // console.log(res.data);
          that.setData(param);
          //如果调用的登录接口，存储Token
          if (url == logInUrl) {
            wx.setStorage({
              key: tokenKey,
              data: res.data.result
            })
          }
          if (func != undefined) {
            func();
          }
        },
        fail: function (error) {
          console.log(error);
          if (getfailFunc != undefined) {
            var failparam = {};
            failparam[statusName] = error;
            that.setData(failparam);
            getfailFunc();
          }
        }
      })
    }
  })
}

module.exports.getRequest = getRequest;
module.exports.postRequest = postRequest;