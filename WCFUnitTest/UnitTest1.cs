using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WCFUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        //测试SOAP服务
        //1、添加服务引用
        //2、使用Client对象
        [TestMethod]
        public void IsGreaterThanZero_True()
        {
            //正常执行，返回成功结果
            SoapService.Service1Client client = new SoapService.Service1Client();
            bool result= client.IsGreaterThanZero(1);
        }

        [TestMethod]
        public void IsGreaterThanZero_False()
        {
            //正常执行，返回失败结果
            SoapService.Service1Client client = new SoapService.Service1Client();
            bool result = client.IsGreaterThanZero(0);
        }

        [TestMethod]
        public void IsGreaterThanZero_Fail()
        {
            //异常执行，返回报错结果  此方法由于过于简单，故而没有这种情况
            //正常测试应该把成功、失败、多种报错分情况进行测试，以保证业务逻辑的正确性

        }

        //测试Rest服务

    }
}
