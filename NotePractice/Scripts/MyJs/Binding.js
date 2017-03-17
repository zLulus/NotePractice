var app = angular.module('editResource', []);
//可以添加上自己注入的服务
app.controller('editResourceCtrl', ['$scope','$http', '$location',
    function ($scope, $http,$location) {
        var vm = this;
    vm.firstSort = [];
    vm.provinceSort = [];
    vm.citySort = [];
    vm.editResource = {

    };

    //初始化
    init();

    //选择省级单位，初始化市级数据   二级联动
    vm.selectProvince = function () {
        var fatherID = vm.province.id;
        vm.citySort = [];
        $http({
            method: 'POST',
            url: '/AngularjsStudy/GetChildrenSort',
            data: { fatherID: fatherID }
        }).then(function successCallback(data) {
            vm.citySort = data.data;
        }, function errorCallback(response) {
            // 请求失败执行代码
        });
    }
    //vm.selectProvince();

    //计算总价
    vm.getTotalPrice = function () {
        vm.editResource.totalPrice = vm.editResource.unitPrice * vm.editResource.listingNumber;
    }

    //保存编辑信息
    vm.save = function ($event) {
        if (vm.city == undefined) {
            alert('请选择地区');
            return;
        }
        //验证表单输入，合法就提交
        if ($scope.requiredInfosForm.$valid) {
            $http({
                method: 'POST',
                url: '/AngularjsStudy/SaveForm',
                data: {
                    editResource: vm.editResource
                }
            }).then(function successCallback(data) {
                alert(data.data);
            }, function errorCallback(response) {
                // 请求失败执行代码
            });
        } else {
            alert('请完善表单信息');
        }

    }

    //初始化页面
    function init() {
        //基本信息
        $http({
            method: 'POST',
            url: '/AngularjsStudy/GetResource',
            data: {  }
        }).then(function successCallback(data) {
            //data有多余属性，data.data才是controller返回的data
            vm.resource = data.data;
        }, function errorCallback(response) {
            // 请求失败执行代码
        });
        //绑定分类
        $http({
            method: 'POST',
            url: '/AngularjsStudy/GetFirstSort',
            data: {}
        }).then(function successCallback(data) {
            vm.firstSort = data.data;
            //当前选择项
            vm.first = vm.firstSort[0];
        }, function errorCallback(response) {
            // 请求失败执行代码
        });
        //省
        $http({
            method: 'POST',
            url: '/AngularjsStudy/GetProvinceSort',
            data: {}
        }).then(function successCallback(data) {
            vm.provinceSort = data.data;
            vm.province = vm.provinceSort[0];
        }, function errorCallback(response) {
            // 请求失败执行代码
        });
    }
}])