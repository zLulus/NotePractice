## Introduction
Extract typical functional points based on project examples of `asp.net core` and `Angular && TypeScript`   
#### front end
1.CoreNgAlain:Front end example based on `ng-zorro` and `ng-alain`.(Project currently in use.)   
2.CoreAngular:Angular-based front end example.(Stop updating)  
Like other Angular projects, run it with the command line `ng serve`. 
#### rear end
1.CoreWebsite:asp .net core project, port 61541   
	Includes multiple test cases and corresponding test pages. 
	Since the blog has references, the old features cannot be migrated. The new interface will be written in CoreWebsite.Api.    
2.CoreWebsite.Api:asp .net coreproject, port 49849, provide apis for the front end.    
3.CoreWebsite.Castle.Windsor.Demo:WindsorContainer demo   
4.CoreWebsite.EntityFramework:EF tool library, CoreWebsite and CoreWebsite.Api share   

#### 注意事项
1.Database related operations     
All operations involving the database, you need to modify the ConnectionStrings in appsettings.json, connect to your database, then use the command line `update-database` to update the database, and then run the program.           


中文如下：
## 项目结构
基于`asp .net core`和`Angular && TypeScript`的项目示例，提取典型功能点     
#### 前端
1.CoreNgAlain:基于`ng-zorro`和`ng-alain`的前端示例.(现在使用较多)   
2.CoreAngular:基于`angular`的前端示例(停止更新)  
和其他Angular项目一样,使用命令行 `ng serve`运行   
#### 后端
1.CoreWebsite:asp .net core项目,端口61541   
	包括多项测试用例和对应的测试页面.    
	由于博客有引用,所以无法迁移.新接口将写在CoreWebsite.Api中.    
2.CoreWebsite.Api:asp .net core项目,端口49849,为前端提供api接口    
3.CoreWebsite.Castle.Windsor.Demo:WindsorContainer demo   
4.CoreWebsite.EntityFramework:EF相关类库,CoreWebsite和CoreWebsite.Api共用    

#### 注意事项
1.涉及数据库的操作     
所有涉及数据库的操作，需要先修改appsettings.json中的ConnectionStrings,连接上你的数据库，再使用命令行`update-database` 更新数据库，才可以运行程序          
