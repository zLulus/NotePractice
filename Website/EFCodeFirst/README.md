## 项目结构
基于`asp .net`和`ef`的项目示例，以大学的课程、学生管理为背景做的demo，该项目不再更新   

需修改Web.config的连接字符串`SchoolContext`，连接并更新数据库 `update-database` 之后，再运行    
```C#
<add name="SchoolContext" connectionString="Server=; Database=test;User Id=sa;Password=;" providerName="System.Data.SqlClient" />
```
