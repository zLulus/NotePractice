using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet6.CodeLibrary.DesignPatterns.BuilderPatternTest
{
    internal interface ICarBuilder
    {
        /// <summary>
        /// 创建车门
        /// </summary>
        /// <param name="part">车门型号</param>
        /// <returns></returns>
        ICarBuilder UseCarDoor(string? part = null);

        /// <summary>
        /// 创建车轮
        /// </summary>
        /// <param name="part">车轮型号</param>
        /// <returns></returns>
        ICarBuilder UseCarWheel(string? part = null);

        /// <summary>
        /// 创建引擎
        /// </summary>
        /// <param name="part">引擎型号</param>
        /// <returns></returns>
        ICarBuilder UseCarEngine(string? part = null);

        /// <summary>
        /// 添加其它部件
        /// </summary>
        /// <param name="partName">部件名</param>
        /// <param name="part">部件型号</param>
        /// <remarks>
        /// 比如大灯、方向盘、低音炮等等。
        /// </remarks>
        /// <returns></returns>
        ICarBuilder UseOtherPart(string partName, string? part);

        /// <summary>
        /// 获得组装好的汽车
        /// </summary>
        /// <remarks>
        /// 在建造者模式中，这个“构建”的操作也叫调度者、调度器、指导者。
        ///
        /// 2个标志：
        /// 1.在这个方法里需要返回构建好的、可用的“产品”
        /// 2.不同条件下、不同场景下具体怎么构建也是由这个方法的实现决定的
        ///
        /// 构建的具体“步骤”定义在接口，代码在实现里面。
        ///
        ///
        /// 提醒：
        /// 1.不管Use和构建的方法定义在不在一起，实现里都应该考虑有些“部件”没有被调用的情况。
        /// 也就是有些“场景”是有默认的?还是报错？还是其它处理？
        ///
        /// </remarks>
        /// <returns></returns>
        Car BuildCar();
    }
}
