using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfServicePractice.Model;

namespace WcfServicePractice
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetOneParameter(string value);

        [OperationContract]
        string GetManyParameter(string number1, string number2);

        [OperationContract]
        string GetManyParameter2(string number1, string number2);

        [OperationContract]
        string PostOneParameter(int value);

        [OperationContract]
        string PostManyParameters(int number1, int number2);

        [OperationContract]
        string PostModel(Student student);

        [OperationContract]
        string PostModelAndParameter(Student student, string isDelete);

        [OperationContract]
        string PostStream(string name, Stream stream);

        [OperationContract]
        Student CombinationStudent(int PKID, string StudentNumber, string Name);
    }
}
