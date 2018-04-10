function Test() {
    //创建phantomjs对象
    var system = require('system');
    //取出参数
    var data = system.args[1];
    console.log(Math.floor(data));
}

Test();
phantom.exit();