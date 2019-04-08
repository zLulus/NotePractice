## 项目结构
基于`asp .net`的项目示例，提取典型功能点    
采用三层架构 =.=     
BLL:逻辑层/业务层   
DAL:数据操纵层  
Model:数据实体  
NotePractice:Web站点   
NotePractice.Tools:工具类  
OData.Client:OData独立示例   

#### 注意事项
1.涉及数据库的操作     
所有涉及数据库的操作，需要先修改`\DAL\DbManager.cs`和`\DAL\TraditionalSQLServerDBManager.cs`中的连接字符串,连接并更新数据库 `update-database` 之后，再测试 
```
connection = new SqlConnection("Server=;DataBase=;Uid=;pwd=;");
```
