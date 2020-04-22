using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.PrintTable.Dtos
{
    public static class TestData
    {
        static TestData()
        {
            m_orderExample.m_orderDetails.Add(new OrderDetail
            {
                Sku = "MBP1304",
                Spec = "黑色",
                Number = 1,
                Unit = "台",
                UnitPrice = 5000.00m,
                Description = "送你的"
            });
            m_orderExample.m_orderDetails.Add(new OrderDetail
            {
                Sku = "DDR1600",
                Spec = "",
                Number = 4,
                Unit = "盒",
                UnitPrice = 200.00m,
                Description = ""
            });
            m_orderExample.m_orderDetails.Add(new OrderDetail
            {
                Sku = "FAN68",
                Spec = "",
                Number = 2,
                Unit = "个",
                UnitPrice = 40.00m,
                Description = "fan fan"
            });
            m_orderExample.m_orderDetails.Add(new OrderDetail
            {
                Sku = "MANUALBOOK",
                Spec = "",
                Number = 1,
                Unit = "本",
                UnitPrice = 30.00m,
                Description = ""
            });
            m_orderExample.m_orderDetails.Add(new OrderDetail
            {
                Sku = "KK103-AF",
                Spec = "USB接口",
                Number = 1,
                Unit = "个",
                UnitPrice = 35.00m,
                Description = ""
            });
            m_orderExample.m_orderDetails.Add(new OrderDetail
            {
                Sku = "DISP-1200",
                Spec = "白色",
                Number = 2,
                Unit = "台",
                UnitPrice = 1600.00m,
                Description = ""
            });
        }

        public static OrderMaster m_orderExample = new OrderMaster
        {
            OrderNo = "106587",
            CustomerName = "BPMF公司",
            ShipAddress = "外高桥AA大道XXX号B厂房X座",
            Express = "顺丰速运",
            Freight = 120.00m
        };
    }
}
