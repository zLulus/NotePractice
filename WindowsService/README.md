## 项目结构
#### MyWindowsService    
Windows service本体，在TimingService的代码层面写业务代码   
#### MyWindowsService.Client    
管理Windows service的可视化界面，可以选择Windows service程序、填写service名称，对服务进行管理   
默认管理MyWindowsService     
![13](https://user-images.githubusercontent.com/19277908/60768690-72154780-a0b6-11e9-8388-b41515c977af.png)     

## 使用说明
选择MyWindowsService.Client，右键生成    
到\bin\Debug目录下，包括MyWindowsService.Client.exe和MyWindowsService.exe等内容   
打开MyWindowsService.Client.exe，安装并启动MyWindowsService.exe    
默认功能是在服务开启、关闭时打日志，每隔一分钟打一次日志，日志在\bin\Debug\output目录下       

## 文档说明
[创建Windows Service](https://github.com/zLulus/My_Note/wiki/%E5%88%9B%E5%BB%BAWindows-Service)
