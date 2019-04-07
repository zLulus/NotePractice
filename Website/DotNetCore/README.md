## 项目结构
#### 前端
CoreNgAlain:基于ng-zorro和ng-alain的前端示例,现在使用较多.   
CoreAngular:基于angular的前端示例  
和其他Angular项目一样,使用命令行 `ng serve`运行   
#### 后端
CoreWebsite:asp .net core项目,端口61541   
	包括多项测试用例,由于博客有引用,所以无法迁移.新接口将写在CoreWebsite.Api中.  
CoreWebsite.Api:asp .net core项目,端口,为前端提供api接口  
CoreWebsite.Castle.Windsor.Demo   
CoreWebsite.EntityFramework:EF相关类库    
需要注意的是,所有涉及数据库的操作，需要先修改appsettings.json中的ConnectionStrings,连接并更新数据库 `update-database` 之后，再测试    
