## Introduction
Extract typical functional points based on the project example of `asp .net`.       
Adopt a three-tier architecture =.=   

1.BLL:Logical layer/business layer   
2.DAL:Data management layer  
3.Model:Data entity  
4.NotePractice:Website     
5.NotePractice.Tools:Tool library  
6.OData.Client:OData independent demo   

#### 注意事项
1.Database related operations     
For all operations involving the database,you need to modify the connection string in `\DAL\DbManager.cs` and `\DAL\TraditionalSQLServerDBManager.cs`, connect and update the database by the command line `update-database`, and then run the program.    
```
connection = new SqlConnection("Server=;DataBase=;Uid=;pwd=;");
```

中文如下：
## 项目结构
基于`asp .net`的项目示例，提取典型功能点    
采用三层架构 =.=     

1.BLL:逻辑层/业务层   
2.DAL:数据操纵层  
3.Model:数据实体  
4.NotePractice:Web站点   
5.NotePractice.Tools:工具类  
6.OData.Client:OData独立示例   

#### 注意事项
1.涉及数据库的操作     
所有涉及数据库的操作，需要先修改`\DAL\DbManager.cs`和`\DAL\TraditionalSQLServerDBManager.cs`中的连接字符串,连接并更新数据库 `update-database` 之后，再测试 
```
connection = new SqlConnection("Server=;DataBase=;Uid=;pwd=;");
```
