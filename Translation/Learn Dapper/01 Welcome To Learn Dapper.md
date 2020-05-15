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
