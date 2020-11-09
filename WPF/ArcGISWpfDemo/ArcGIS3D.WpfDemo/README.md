#### 功能说明  
基于ArcGIS开发的观察者视野显示       
![20201109_134553](https://user-images.githubusercontent.com/19277908/98504441-f4f17800-2291-11eb-8433-c10412ece43f.gif)    
基于ArcGIS开发的动态观察者视野显示        
![坦克](https://user-images.githubusercontent.com/19277908/98502948-59aad380-228e-11eb-9b42-c4cc71f43631.gif)    
基于ArcGIS开发的计算两立方体是否相交，相交部分展示      
![立方体关系](https://user-images.githubusercontent.com/19277908/98502752-bfe32680-228d-11eb-9a57-b62519691e1e.gif)    
OBB碰撞算法C#版       

#### 运行须知
shp数据和tif数据需要自行提供，放在`/Data/`目录下，具体说明看该目录下的说明    
坦克初始化的坐标、视点的坐标、摄像头的初始化坐标需要根据自己的数据进行修改(查看初始化region)    
坦克模型需要安装ArcGIS Runtime Local Server SDK或者下载[离线模型数据](https://www.arcgis.com/home/item.html?id=07d62a792ab6496d9b772a24efea45d0)才能正常显示    

#### 错误处理
Severity	Code	Description	Project	File	Line	Suppression State    
Error		ArcGIS Runtime Local Server SDK v100.9 component not installed. Local Server is required to build this project. Download and install Local Server from http://links.esri.com/arcgis-runtime-local-server-sdk-v100			
下载并安装ArcGIS Runtime Local Server SDK    

#### 参考资料    
地图展示    
创建圆柱体   
https://developers.arcgis.com/net/latest/wpf/sample-code/scene-symbols/    
数据分析（重合）   
https://developers.arcgis.com/net/latest/wpf/api-reference/html/M_Esri_ArcGISRuntime_Geometry_GeometryEngine_Overlaps.htm    
选中    
https://developers.arcgis.com/net/latest/wpf/sample-code/scene-layer-selection/   
数据分析（视觉）   
https://developers.arcgis.com/net/latest/wpf/sample-code/viewshed-for-camera/   
https://developers.arcgis.com/net/latest/wpf/sample-code/viewshed-location/   
