# 欢迎学习Dapper
该站点面向想要学习如何使用Dapper的开发人员，Dapper是[Stackoverflow](https://stackoverflow.com/)背后的人们生产的微型ORM。
# 什么是Dapper？
Dapper是一种流行的简单对象映射工具。 它主要设计用于希望以强类型方式处理数据的场景——作为.NET应用程序中的业务对象，不想花费大量时间编写代码就可以映射ADO.NET数据查询结果。 Dapper是[Apache](https://www.apache.org/licenses/LICENSE-2.0)许可下的一个开源项目。 它以Nuget软件包的形式提供，已被下载超过1600万次。
# Dapper是ORM吗？
Dapper属于一系列称为微型ORM的工具。 这些工具仅执行成熟的对象关系映射器功能的一部分，例如Entity Framework Core。 功能因产品而异。 下表提供了与ORM相比您可以期望在微型ORM中找到的功能的一般概念：
|  | Micro ORM | ORM |
| ------ | ------ | ------ |
| 将查询映射到对象 | √ | √ |
| 缓存结果 | x | √ |
| 变更追踪 | x<sup>1</sup> | √ |
| SQL生成 | x<sup>2</sup> | √ |
| 身份管理 | x | √ |
| 协会管理 | x | √ |
| 延迟加载 | x | √ |
| 工作单位支持 | x | √ |
| 数据库迁移 | x | √ |     

Dapper专注于ORM的O和M-对象映射。    
[1]已将一些扩展添加到Dapper，这些扩展提供了最小的变更跟踪功能。[2] Dapper实际上确实生成SQL，但是方式有限。  
# 什么时候应该使用Dapper？
在决定是否使用Dapper时，应牢记其存在的主要原因-性能。 Dapper的原始开发人员使用的是Entity Framework Core的前身-寿命短的[Linq to SQL](https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/sql/linq/)。他们发现查询性能不足以解决站点（Stackoverflow）遇到的日益增长的流量问题，因此他们[编写了自己的微型ORM](https://samsaffron.com/archive/2011/03/30/How+I+learned+to+stop+worrying+and+write+my+own+ORM)。    
因此，在只读数据经常更改且经常被请求的情况下，Dapper是一个不错的选择。在无状态的情况下（例如网络），Dapper特别好，因为在这种情况下，无需将复杂的对象图持久保存在内存中。    
Dapper不会像使用完整的ORM一样将.NET语言编写的查询转换为SQL。因此，您需要习惯用SQL编写查询，或者让别人为您编写查询。    
Dapper对您的数据库架构没有真正的期望。它不以与Entity Framework Core相同的方式依赖于约定，因此在数据库结构没有特别规范化的情况下，Dapper也是一个不错的选择。   
Dapper与ADO.NET `IDbConnection`对象一起使用，这意味着它将与任何具有ADO.NET提供程序的数据库系统一起使用。    
没有理由不能在同一项目中同时使用ORM和微型ORM。    
# Dapper实际可以做什么？
这是一些标准的ADO.NET代码，用于从数据库中检索数据并将其具体化为`Product`对象的集合：
```C#
var sql = "select * from products";
var products = new List<Product>();
using (var connection = new SqlConnection(connString))
{
    connection.Open();
    /*****假装这是高亮****/
    using (var command = new SqlCommand(sql, connection))
    {
        using (var reader = command.ExecuteReader())
        {
            var product = new Product
            {
                ProductId = reader.GetInt32(reader.GetOrdinal("ProductId")),
                ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                SupplierId = reader.GetInt32(reader.GetOrdinal("SupplierId")),
                CategoryId = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                QuantityPerUnit = reader.GetString(reader.GetOrdinal("QuantityPerUnit")),
                UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice")),
                UnitsInStock = reader.GetInt16(reader.GetOrdinal("UnitsInStock")),
                UnitsOnOrder = reader.GetInt16(reader.GetOrdinal("UnitsOnOrder")),
                ReorderLevel = reader.GetInt16(reader.GetOrdinal("ReorderLevel")),
                Discontinued = reader.GetBoolean(reader.GetOrdinal("Discontinued")),
                DiscontinuedDate = reader.GetDateTime(reader.GetOrdinal("DiscontinuedDate"))
            };
            products.Add(product);
        }
    }
    /*****高亮结束****/
}
```
在最基本的层次上，Dapper用以下代码替换了上面示例中高亮显示的代码块：
```C#
products = connection.Query<Product>(sql);
```
Dapper还负责创建命令并在需要时打开连接。 如果您使用Dapper来管理这样的基本任务，那么它将节省您的时间。 实际上，Dapper的功能还有很多。    
# 在哪里获得Dapper？
Dapper可以从Nuget中获得。 它符合.NET Standard 2.0，这意味着它可以在面向整个框架和.NET Core的.NET应用程序中使用。 您可以通过.NET CLI，使用以下命令来安装Dapper的最新版本：
```cmd
dotnet add package Dapper
```
或在Visual Studio的程序包管理器控制台中执行以下命令：
```cmd
install-package Dapper
```
Dapper的源代码可在[GitHub](https://github.com/StackExchange/Dapper)上获得。

