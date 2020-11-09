using ArcGIS3D.WpfDemo.Dialogs;
using ArcGIS3D.WpfDemo.Enums;
using ArcGIS3D.WpfDemo.Managers;
using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Portal;
using Esri.ArcGISRuntime.Rasters;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI;
using Esri.ArcGISRuntime.UI.Controls;
using Esri.ArcGISRuntime.UI.GeoAnalysis;
using Newtonsoft.Json;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ArcGIS3D.WpfDemo
{
    /// <summary>
    /// Interaction logic for CubeCollision.xaml
    /// </summary>
    public partial class CubeCollision : UserControl
    {
        /// <summary>
        /// 点击模式
        /// </summary>
        TapTypeEnum tapTypeEnum { get; set; }

        #region 初始化
        //视域点
        double ViewshedX = 105.67956087176, ViewshedY= 32.0470744099947, ViewshedZ=5;
        //坦克
        double TankX = 105.68032648899845, TankY = 32.046688336747344;
        //摄像头
        double FeatureLayerLatitude = 32.0429071258535, FeatureLayerLongitude = 105.678052767021;
        /// <summary>
        /// 坦克移动路线
        /// </summary>
        private void InitializeTankRouteData()
        {
            route = new List<MapPoint>();
            route.Add(new MapPoint(105.68032648899845, 32.046688336747344, 0, SpatialReferences.Wgs84));
            route.Add(new MapPoint(105.6795961273594, 32.046558078485113, 0, SpatialReferences.Wgs84));
            route.Add(new MapPoint(105.67895987690494, 32.046532905811574, 0, SpatialReferences.Wgs84));
            route.Add(new MapPoint(105.67835509090905, 32.04680003518245, 0, SpatialReferences.Wgs84));
            route.Add(new MapPoint(105.67764150078037, 32.046889517876, 0, SpatialReferences.Wgs84));
            route.Add(new MapPoint(105.67729368856232, 32.046784288553589, 0, SpatialReferences.Wgs84));
            route.Add(new MapPoint(105.67735337848897, 32.046309531310364, 0, SpatialReferences.Wgs84));
            route.Add(new MapPoint(105.67749941913492, 32.046094942667231, 0, SpatialReferences.Wgs84));
            route.Add(new MapPoint(105.67812928127192, 32.045973713959775, 0, SpatialReferences.Wgs84));
            route.Add(new MapPoint(105.67835997263025, 32.045912348144157, 0, SpatialReferences.Wgs84));
            route.Add(new MapPoint(105.67865843206963, 32.045868063537768, 0, SpatialReferences.Wgs84));
            route.Add(new MapPoint(105.67919632065649, 32.045676804675345, 0, SpatialReferences.Wgs84));
            route.Add(new MapPoint(105.67944181001506, 32.04532480449501, 0, SpatialReferences.Wgs84));
            route.Add(new MapPoint(105.68003860958297, 32.045206927960834, 0, SpatialReferences.Wgs84));
            route.Add(new MapPoint(105.68032699164685, 32.045876596661039, 0, SpatialReferences.Wgs84));
        }
        #endregion

        #region file path
        string ShpFilePath
        {
            get { return $"{System.IO.Directory.GetCurrentDirectory()}\\Data\\{ConfigurationManager.AppSettings["ShpFilePath"]}"; }
        }
        string TifFilePath
        {
            get { return $"{System.IO.Directory.GetCurrentDirectory()}\\Data\\{ConfigurationManager.AppSettings["TifFilePath"]}"; }
        }
        string GraphicShpFilePath
        {
            get { return $"{System.IO.Directory.GetCurrentDirectory()}\\Data\\{ConfigurationManager.AppSettings["GraphicShpFilePath"]}"; }
        }
        string ResultShpFilePath
        {
            get { return $"{System.IO.Directory.GetCurrentDirectory()}\\Data\\{ConfigurationManager.AppSettings["ResultShpFilePath"]}"; }
        }
        #endregion

        #region 绘制 & 分析
        /// <summary>
        /// shp图层
        /// </summary>
        FeatureLayer featureLayer { get; set; }
        /// <summary>
        /// 绘图Layer
        /// </summary>
        FeatureLayer graphicLayer { get; set; }
        /// <summary>
        /// 重叠结果显示图层
        /// </summary>
        FeatureLayer intersectionLayer { get; set; }
        /// <summary>
        /// 选择的shp图层要素
        /// </summary>
        GeoElement selectFeatureGeoElement { get; set; }
        /// <summary>
        /// 选择的绘制图层要素
        /// </summary>
        GeoElement selectGraphic { get; set; }
        #endregion

        #region 观察者
        // Hold a reference to the viewshed analysis.
        private LocationViewshed _viewshed;
        // Graphics overlay for viewpoint symbol.
        private GraphicsOverlay _viewpointOverlay;
        // Symbol for viewpoint.
        private SimpleMarkerSceneSymbol _viewpointSymbol;
        // Hold a reference to the analysis overlay that will hold the viewshed analysis.
        private AnalysisOverlay _analysisOverlay;
        // Height of the viewpoint above the ground.
        private double _viewHeight;
        /// <summary>
        /// 移动观察者-是否绘制完毕
        /// Flag indicating if the viewshed will move with the mouse.
        /// </summary>
        //private bool subscribedToMouseViewPoint;
        #endregion

        #region 坦克观察者
        // Graphic and overlay for showing the tank
        private readonly GraphicsOverlay _tankOverlay = new GraphicsOverlay();
        private Graphic _tank;

        // Animation properties
        private MapPoint _tankEndPoint;
        // Units for geodetic calculation (used in animating tank)
        private readonly LinearUnit _metersUnit = (LinearUnit)Unit.FromUnitId(9001);
        private readonly AngularUnit _degreesUnit = (AngularUnit)Unit.FromUnitId(9102);
        /// <summary>
        /// 切换移动坦克的模式（false=自动；true=手动）
        /// </summary>
        bool isAnimateTank { get; set; }
        bool isFinishRoute { get; set; }
        List<MapPoint> route { get; set; }
        /// <summary>
        /// 误差
        /// </summary>
        double gap = 0.00001;
        #endregion

        public CubeCollision()
        {
            InitializeComponent();


            InitializeMap();
        }

        #region 初始化方法
        private async void InitializeMap()
        {
            //shp
            await InitializeFeatureLayer();

            //tiff
            InitializeImageOverlay();

            //绘图图层
            //shp必须有数据，否则读取shp报错
            await InitializeGraphicsLayer();

            //重叠结果显示图层
            await InitializeIntersectionLayer();

            //观察者
            InitializeViewshed();

            //坦克观察者
            InitializeTankViewshed();
        }

        private async void InitializeTankViewshed()
        {
            isAnimateTank = false;
            isFinishRoute = true;
            //设置坦克自移动的路径
            InitializeTankRouteData();

            // Configure the graphics overlay for the tank and add the overlay to the SceneView.
            _tankOverlay.SceneProperties.SurfacePlacement = SurfacePlacement.Relative;
            MySceneView.GraphicsOverlays.Add(_tankOverlay);

            // Configure the heading expression for the tank; this will allow the
            //     viewshed to update automatically based on the tank's position.
            SimpleRenderer renderer3D = new SimpleRenderer();
            renderer3D.SceneProperties.HeadingExpression = "[HEADING]";
            _tankOverlay.Renderer = renderer3D;

            try
            {
                // Create the tank graphic - get the model path.
                string modelPath = DataManager.GetDataFolder("07d62a792ab6496d9b772a24efea45d0", "bradle.3ds");
                // - Create the symbol and make it 10x larger (to be the right size relative to the scene).
                ModelSceneSymbol tankSymbol = await ModelSceneSymbol.CreateAsync(new Uri(modelPath), 10);
                // - Adjust the position.
                tankSymbol.Heading = 90;
                // - The tank will be positioned relative to the scene surface by its bottom.
                //       This ensures that the tank is on the ground rather than partially under it.
                tankSymbol.AnchorPosition = SceneSymbolAnchorPosition.Bottom;
                // - Create the graphic.
                _tank = new Graphic(new MapPoint(TankX,TankY, SpatialReferences.Wgs84), tankSymbol);
                // - Update the heading.
                _tank.Attributes["HEADING"] = 0.0;
                // - Add the graphic to the overlay.
                _tankOverlay.Graphics.Add(_tank);

                // Create a viewshed for the tank.
                GeoElementViewshed geoViewshed = new GeoElementViewshed(
                    geoElement: _tank,
                    horizontalAngle: 90.0,
                    verticalAngle: 40.0,
                    minDistance: 0.1,
                    maxDistance: 250.0,
                    headingOffset: 0.0,
                    pitchOffset: 0.0)
                {
                    // Offset viewshed observer location to top of tank.
                    OffsetZ = 3.0
                };

                // Create the analysis overlay and add to the scene.
                AnalysisOverlay overlay = new AnalysisOverlay();
                overlay.Analyses.Add(geoViewshed);
                MySceneView.AnalysisOverlays.Add(overlay);

                //// Create a camera controller to orbit the tank.
                //OrbitGeoElementCameraController cameraController = new OrbitGeoElementCameraController(_tank, 200.0)
                //{
                //    CameraPitchOffset = 45.0
                //};
                //// - Apply the camera controller to the SceneView.
                //MySceneView.CameraController = cameraController;

                //手动移动坦克
                // Create a timer; this will enable animating the tank.
                System.Timers.Timer animationTimer = new System.Timers.Timer(60)
                {
                    Enabled = true,
                    AutoReset = true
                };
                // - Move the tank every time the timer expires.
                animationTimer.Elapsed += (o, e) =>
                {
                    if (isAnimateTank)
                    {
                        AnimateTank();
                    }
                };
                // - Start the timer.
                animationTimer.Start();

                //坦克自动移动
                System.Timers.Timer animationTimer2 = new System.Timers.Timer(15000)
                {
                    Enabled = true,
                    AutoReset = true
                };
                // - Move the tank every time the timer expires.
                animationTimer2.Elapsed += (o, e) =>
                {
                    MoveTankForRoute();
                };
                //马上执行一次
                Task.Run(() =>
                {
                    MoveTankForRoute();
                });
                // - Start the timer.
                animationTimer2.Start();

                // Allow the user to click to define a new destination.
                MySceneView.GeoViewTapped += (sender, args) =>
                {
                    _tankEndPoint = args.Location;
                    //route.Add(_tankEndPoint);
                    var rightMapPoint = GeometryEngine.Project(_tankEndPoint, featureLayer.SpatialReference) as MapPoint;
                };
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error");
            }
        }

       

        private void MoveTankForRoute()
        {
            if (!isAnimateTank && isFinishRoute)
            {
                isFinishRoute = false;
                foreach (var p in route)
                {
                    while (true)
                    {
                        MoveTank(p);
                        var nowLocation = _tank.Geometry as MapPoint;
                        //手动等待坦克到位
                        if (nowLocation.X <= p.X + gap
                        && nowLocation.X >= p.X - gap
                        && nowLocation.Y <= p.Y + gap
                        && nowLocation.Y >= p.Y - gap)
                        {
                            break;
                        }
                        if (isAnimateTank)
                        {
                            break;
                        }
                        Thread.Sleep(60);
                    }
                    if (isAnimateTank)
                    {
                        break;
                    }
                }
                isFinishRoute = true;
            }
        }

        private async Task InitializeIntersectionLayer()
        {
            try
            {
                ShapefileFeatureTable myShapefile = await ShapefileFeatureTable.OpenAsync(ResultShpFilePath);
                intersectionLayer = new FeatureLayer(myShapefile)
                {
                    RenderingMode = FeatureRenderingMode.Dynamic
                };

                #region 绘制高程
                SimpleLineSymbol mySimpleLineSymbol = new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, System.Drawing.Color.Black, 1);
                SimpleFillSymbol mysimpleFillSymbol = new SimpleFillSymbol(SimpleFillSymbolStyle.Cross, System.Drawing.Color.Red, mySimpleLineSymbol);
                SimpleRenderer mySimpleRenderer = new SimpleRenderer(mysimpleFillSymbol);
                RendererSceneProperties myRendererSceneProperties = mySimpleRenderer.SceneProperties;
                myRendererSceneProperties.ExtrusionMode = ExtrusionMode.AbsoluteHeight;
                myRendererSceneProperties.ExtrusionExpression = "[Z]";
                intersectionLayer.Renderer = mySimpleRenderer;
                #endregion

                intersectionLayer.Opacity = 1;

                MySceneView.Scene.Basemap.BaseLayers.Add(intersectionLayer);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error");
            }
        }

        private async Task InitializeGraphicsLayer()
        {
            try
            {
                ShapefileFeatureTable myShapefile = await ShapefileFeatureTable.OpenAsync(GraphicShpFilePath);
                graphicLayer = new FeatureLayer(myShapefile)
                {
                    RenderingMode = FeatureRenderingMode.Dynamic
                };

                #region 绘制高程
                SimpleLineSymbol mySimpleLineSymbol = new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, System.Drawing.Color.Black, 1);
                SimpleFillSymbol mysimpleFillSymbol = new SimpleFillSymbol(SimpleFillSymbolStyle.Cross, System.Drawing.Color.DarkGray, mySimpleLineSymbol);
                SimpleRenderer mySimpleRenderer = new SimpleRenderer(mysimpleFillSymbol);
                RendererSceneProperties myRendererSceneProperties = mySimpleRenderer.SceneProperties;
                myRendererSceneProperties.ExtrusionMode = ExtrusionMode.AbsoluteHeight;
                myRendererSceneProperties.ExtrusionExpression = "[Z]";
                graphicLayer.Renderer = mySimpleRenderer;
                #endregion

                graphicLayer.Opacity = 0.8;

                MySceneView.Scene.Basemap.BaseLayers.Add(graphicLayer);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error");
            }
        }

        private void InitializeViewshed()
        {
            var initialLocation = new MapPoint(ViewshedX, ViewshedY, ViewshedZ, featureLayer.SpatialReference);
            // Create the location viewshed analysis.
            _viewshed = new LocationViewshed(
                initialLocation,
                HeadingSlider.Value,
                PitchSlider.Value,
                HorizontalAngleSlider.Value,
                VerticalAngleSlider.Value,
                MinimumDistanceSlider.Value,
                MaximumDistanceSlider.Value);

            // Create a symbol for the viewpoint.
            _viewpointSymbol = SimpleMarkerSceneSymbol.CreateSphere(System.Drawing.Color.Blue, 10, SceneSymbolAnchorPosition.Center);

            // Add the symbol to the viewpoint overlay.
            _viewpointOverlay = new GraphicsOverlay
            {
                SceneProperties = new LayerSceneProperties(SurfacePlacement.Absolute)
            };
            // Update the viewpoint graphic.
            _viewpointOverlay.Graphics.Clear();
            _viewpointOverlay.Graphics.Add(new Graphic(_viewshed.Location, _viewpointSymbol));


            // Create an analysis overlay for showinSetViewpointCameraAsyncg the viewshed analysis.
            _analysisOverlay = new AnalysisOverlay();

            // Add the viewshed analysis to the overlay.
            _analysisOverlay.Analyses.Add(_viewshed);

            // Add the analysis overlay to the SceneView.
            MySceneView.AnalysisOverlays.Add(_analysisOverlay);

            // Add the graphics overlay
            MySceneView.GraphicsOverlays.Add(_viewpointOverlay);

        }

        private async Task InitializeFeatureLayer()
        {
            // Create a new map to display in the map view with a streets basemap
            //shp
            try
            {
                // Open a shapefile stored locally and add it to the map as a feature layer
                // Get the path to the downloaded shapefile
                // Open the shapefile
                ShapefileFeatureTable myShapefile = await ShapefileFeatureTable.OpenAsync(ShpFilePath);

                // Create a feature layer to display the shapefile
                featureLayer = new FeatureLayer(myShapefile)
                {
                    // Set the rendering mode of the feature layer to be dynamic (needed for extrusion to work)
                    RenderingMode = FeatureRenderingMode.Dynamic
                };

                #region 绘制高程
                // Create a new simple line symbol for the feature layer
                SimpleLineSymbol mySimpleLineSymbol = new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, System.Drawing.Color.Black, 1);

                // Create a new simple fill symbol for the feature layer 
                SimpleFillSymbol mysimpleFillSymbol = new SimpleFillSymbol(SimpleFillSymbolStyle.Cross, System.Drawing.Color.WhiteSmoke, mySimpleLineSymbol);
                
                // Create a new simple renderer for the feature layer
                SimpleRenderer mySimpleRenderer = new SimpleRenderer(mysimpleFillSymbol);

                // Get the scene properties from the simple renderer
                RendererSceneProperties myRendererSceneProperties = mySimpleRenderer.SceneProperties;

                // Set the extrusion mode for the scene properties
                myRendererSceneProperties.ExtrusionMode = ExtrusionMode.AbsoluteHeight;

                // Set the initial extrusion expression
                myRendererSceneProperties.ExtrusionExpression = "[Z]";

                // Set the feature layer's renderer to the define simple renderer
                featureLayer.Renderer = mySimpleRenderer;
                #endregion

                featureLayer.Opacity = 0.8;

                //设置底图样式
                MySceneView.Scene = new Scene(BasemapType.DarkGrayCanvasVector);
                // Add the feature layer to the map
                MySceneView.Scene.Basemap.BaseLayers.Add(featureLayer);
                // Zoom the map to the extent of the shapefile
                //设置摄像头
                Camera camera = new Camera(FeatureLayerLatitude, FeatureLayerLongitude, 200.0, 20.0, 70.0, 0.0);
                var viewpoint = new Viewpoint(myShapefile.Extent, camera);
                await MySceneView.SetViewpointAsync(viewpoint);


                //订阅点击事件（含多个功能的逻辑）
                MySceneView.GeoViewTapped += MySceneViewOnGeoViewTapped;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error");
            }
        }

        private async void InitializeImageOverlay()
        {
            try
            {
                //https://developers.arcgis.com/net/latest/wpf/guide/add-image-overlays.htm
                Raster raster = new Raster(TifFilePath);
                RasterLayer rasterLayer = new RasterLayer(raster);
                MySceneView.Scene.Basemap.BaseLayers.Add(rasterLayer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        #endregion

        private void MySceneViewOnGeoViewTapped(object sender, Esri.ArcGISRuntime.UI.Controls.GeoViewInputEventArgs e)
        {
            //移动观察者
            if (tapTypeEnum == TapTypeEnum.MoveViewPoint)
            {
                MySceneView.MouseMove -= MySceneViewOnMoveViewPoint;
            }
            else if (tapTypeEnum == TapTypeEnum.DrawByCenter)
            {

            }
            else if (tapTypeEnum == TapTypeEnum.Select)
            {

            }
        }

        #region 切换模式

        private void ChangeModeStatus_Click(object sender, RoutedEventArgs e)
        {
            ResetEvent();
            tapTypeEnum = TapTypeEnum.None;
        }


        private void DrawByPoint_Click(object sender, RoutedEventArgs e)
        {
            ResetEvent();
            points = new List<MapPoint>();
            tapTypeEnum = TapTypeEnum.DrawByPolygon;
            MySceneView.PreviewMouseLeftButtonDown += MySceneViewOnMouseMoveDrawByPolygon;
        }

        private void SelectFeatureLayer_Click(object sender, RoutedEventArgs e)
        {
            ResetEvent();
            tapTypeEnum = TapTypeEnum.SelectFeatureLayer;
            MySceneView.GeoViewTapped += MySceneViewOnSelectFeatureLayer;
        }

        private void SelectGraphicLayer_Click(object sender, RoutedEventArgs e)
        {
            ResetEvent();
            tapTypeEnum = TapTypeEnum.SelectGraphicLayer;
            MySceneView.GeoViewTapped += MySceneViewOnSelectGraphicLayer;

        }


        private void SelectIntersectionOverlay_Click(object sender, RoutedEventArgs e)
        {
            ResetEvent();
            tapTypeEnum = TapTypeEnum.SelectIntersectionOverlay;
            MySceneView.GeoViewTapped += MySceneViewOnSelectIntersectionOverlay;
        }

        
        private void ChangeModeViewPointStatus_Click(object sender, RoutedEventArgs e)
        {
            ResetEvent();
            tapTypeEnum = TapTypeEnum.MoveViewPoint;
            MySceneView.MouseMove += MySceneViewOnMoveViewPoint;
        }


        private void MoveTank_Click(object sender, RoutedEventArgs e)
        {
            ResetEvent();
            isAnimateTank = true;
        }

        private void ResetEvent()
        {
            MySceneView.MouseMove -= MySceneViewOnMoveViewPoint;
            MySceneView.GeoViewTapped -= MySceneViewOnSelectFeatureLayer;
            MySceneView.GeoViewTapped -= MySceneViewOnSelectGraphicLayer;
            MySceneView.PreviewMouseLeftButtonDown -= MySceneViewOnMouseMoveDrawByPolygon;
            MySceneView.GeoViewTapped -= MySceneViewOnSelectIntersectionOverlay;
            isAnimateTank = false;
        }
        #endregion

        #region 绘制
        List<MapPoint> points;
        private async void MySceneViewOnMouseMoveDrawByPolygon(object sender, MouseEventArgs mouseEventArgs)
        {
            if (points.Count < 4)
            {
                // Get the mouse position.
                Point cursorSceenPoint = mouseEventArgs.GetPosition(MySceneView);

                // Get the corresponding MapPoint.
                MapPoint onMapLocation = MySceneView.ScreenToBaseSurface(cursorSceenPoint);
                //TestCreateCube(onMapLocation.X, onMapLocation.Y, onMapLocation.Z);
                if (onMapLocation != null)
                {
                    //防止重复点击
                    var isExist = points.Where(x => x != null && x.X == onMapLocation.X && x.Y == onMapLocation.Y && x.Z == onMapLocation.Z).FirstOrDefault();
                    if (isExist == null)
                        points.Add(onMapLocation);
                }

            }
            if (points.Count >= 4)
            {
                SetHeight setHeight = new SetHeight();
                var dialogResult = setHeight.ShowDialog();
                if (dialogResult.HasValue && dialogResult.Value)
                {
                    var feature = graphicLayer.FeatureTable.CreateFeature();
                    feature.Attributes.Remove("Z");
                    feature.Attributes.Add("Z", setHeight.height);
                    List<MapPoint> pointsWithZ = new List<MapPoint>();
                    foreach (var p in points)
                    {
                        pointsWithZ.Add(new MapPoint(p.X, p.Y, p.SpatialReference));
                        //pointsWithZ.Add(MapPoint.CreateWithM(p.X, p.Y, p.Z + setHeight.height, setHeight.height, p.SpatialReference));
                    }
                    Esri.ArcGISRuntime.Geometry.Polygon polygon = new Esri.ArcGISRuntime.Geometry.Polygon(pointsWithZ, points[0].SpatialReference);
                    feature.Geometry = polygon;
                    await graphicLayer.FeatureTable.AddFeatureAsync(feature);

                    MySceneView.PreviewMouseLeftButtonDown -= MySceneViewOnMouseMoveDrawByPolygon;
                    //清空
                    points = new List<MapPoint>();
                    //try
                    //{
                    //    //上
                    //    List<MapPoint> pointsWithZ = new List<MapPoint>();
                    //    foreach (var p in points)
                    //    {
                    //        pointsWithZ.Add(new MapPoint(p.X, p.Y, setHeight.height, p.SpatialReference));
                    //        //pointsWithZ.Add(MapPoint.CreateWithM(p.X, p.Y, p.Z + setHeight.height, setHeight.height, p.SpatialReference));
                    //    }
                    //    Esri.ArcGISRuntime.Geometry.Polygon polygon = new Esri.ArcGISRuntime.Geometry.Polygon(pointsWithZ, points[0].SpatialReference);
                    //    DrawOnePolygon(polygon);
                    //    //下
                    //    pointsWithZ = new List<MapPoint>();
                    //    foreach (var p in points)
                    //    {
                    //        pointsWithZ.Add(new MapPoint(p.X, p.Y, 0, p.SpatialReference));
                    //    }
                    //    polygon = new Esri.ArcGISRuntime.Geometry.Polygon(pointsWithZ, points[0].SpatialReference);
                    //    DrawOnePolygon(polygon);
                    //    //左
                    //    //右
                    //    //前
                    //    //后
                    //    //CreateOneSide(setHeight.height, 0, 1);
                    //    //CreateOneSide(setHeight.height, 1, 2);
                    //    //CreateOneSide(setHeight.height, 2, 3);
                    //    //CreateOneSide(setHeight.height, 3, 0);

                    //    MySceneView.PreviewMouseLeftButtonDown -= MySceneViewOnMouseMoveDrawByPolygon;
                    //    points = new List<MapPoint>();
                    //}
                    //catch(Exception ex)
                    //{

                    //}

                }

            }

        }

        //private void CreateOneSide(double height,int index1,int index2)
        //{
        //    List<MapPoint> pointsWithZ = new List<MapPoint>();
        //    MapPoint p1 = points[0];
        //    MapPoint p2 = points[1];
        //    pointsWithZ.Add(new MapPoint(p1.X, p1.Y, 0, p1.SpatialReference));
        //    pointsWithZ.Add(new MapPoint(p1.X, p1.Y, height, p1.SpatialReference));
        //    pointsWithZ.Add(new MapPoint(p2.X, p2.Y, 0, p2.SpatialReference));
        //    pointsWithZ.Add(new MapPoint(p2.X, p2.Y, height, p2.SpatialReference));
        //    var polygon = new Esri.ArcGISRuntime.Geometry.Polygon(pointsWithZ, p1.SpatialReference);
        //    DrawOnePolygon(polygon);
        //}

        //private void DrawOnePolygon(Esri.ArcGISRuntime.Geometry.Polygon polygon)
        //{
        //    SimpleFillSymbol simpleFillSymbol = new SimpleFillSymbol(SimpleFillSymbolStyle.Solid, System.Drawing.Color.AliceBlue, null);
        //    Graphic item = new Graphic(polygon, simpleFillSymbol);

        //    graphicOverlay.Graphics.Add(item);
        //}
        #endregion

        #region 清理图层
        private void ClearGraphicOverlay_Click(object sender, RoutedEventArgs e)
        {
            //todo
        }

        private void ClearIntersectionOverlay_Click(object sender, RoutedEventArgs e)
        {
            //todo
        }
        #endregion

        #region 选择-高亮
        private async void MySceneViewOnSelectFeatureLayer(object sender, Esri.ArcGISRuntime.UI.Controls.GeoViewInputEventArgs e)
        {
            //shp图层
            selectFeatureGeoElement= await SetSelectForFeatureLayer(e);
        }

        private async void MySceneViewOnSelectGraphicLayer(object sender, Esri.ArcGISRuntime.UI.Controls.GeoViewInputEventArgs e)
        {
            //绘制图层
            selectGraphic= await SetSelectForGraphicLayer(e);
            //selectGraphic = await SetSelectForGraphicsOverlay(e,graphicOverlay);
        }

        private async void MySceneViewOnSelectIntersectionOverlay(object sender, GeoViewInputEventArgs e)
        {
            graphicLayer.ClearSelection();
            featureLayer.ClearSelection();
            //结果显示图层
            await SetSelectForIntersectionLayer(e);
            //await SetSelectForGraphicsOverlay(e, intersectionOverlay);

            //selectGraphic.IsVisible = false;
            //featureLayer.IsVisible = false;
            //selectFeatureGeoElement.
        }

        //private async Task<GeoElement> SetSelectForLayer(GeoViewInputEventArgs e,FeatureLayer featureLayer)
        //{
        //    var result = await MySceneView.IdentifyLayerAsync(featureLayer, e.Position, 1, false);
        //    GeoElement geoElement = result.GeoElements.FirstOrDefault();
        //    if (geoElement != null)
        //    {
        //        QueryParameters queryParams = new QueryParameters
        //        {
        //            // Set the geometry to selection envelope for selection by geometry.
        //            Geometry = geoElement.Geometry //selectionEnvelope
        //        };
        //        // Select the features based on query parameters defined above.
        //        await featureLayer.SelectFeaturesAsync(queryParams, Esri.ArcGISRuntime.Mapping.SelectionMode.New);
        //    }
        //    return geoElement;
        //}

        private async Task<GeoElement> SetSelectForIntersectionLayer(GeoViewInputEventArgs e)
        {
            var result = await MySceneView.IdentifyLayerAsync(intersectionLayer, e.Position, 1, false);
            GeoElement geoElement = result.GeoElements.FirstOrDefault();
            if (geoElement != null)
            {
                QueryParameters queryParams = new QueryParameters
                {
                    // Set the geometry to selection envelope for selection by geometry.
                    Geometry = geoElement.Geometry //selectionEnvelope
                };
                // Select the features based on query parameters defined above.
                await intersectionLayer.SelectFeaturesAsync(queryParams, Esri.ArcGISRuntime.Mapping.SelectionMode.New);
            }
            return geoElement;
        }

        private async Task<GeoElement> SetSelectForFeatureLayer(GeoViewInputEventArgs e)
        {
            var result = await MySceneView.IdentifyLayerAsync(featureLayer, e.Position, 1, false);
            GeoElement geoElement = result.GeoElements.FirstOrDefault();
            if (geoElement != null)
            {
                QueryParameters queryParams = new QueryParameters
                {
                    // Set the geometry to selection envelope for selection by geometry.
                    Geometry = geoElement.Geometry //selectionEnvelope
                };
                // Select the features based on query parameters defined above.
                await featureLayer.SelectFeaturesAsync(queryParams, Esri.ArcGISRuntime.Mapping.SelectionMode.New);
            }
            return geoElement;
        }

        private async Task<GeoElement> SetSelectForGraphicLayer(GeoViewInputEventArgs e)
        {
            var result = await MySceneView.IdentifyLayerAsync(graphicLayer, e.Position, 1, false);
            GeoElement geoElement = result.GeoElements.FirstOrDefault();
            if (geoElement != null)
            {
                QueryParameters queryParams = new QueryParameters
                {
                    // Set the geometry to selection envelope for selection by geometry.
                    Geometry = geoElement.Geometry //selectionEnvelope
                };
                // Select the features based on query parameters defined above.
                await graphicLayer.SelectFeaturesAsync(queryParams, Esri.ArcGISRuntime.Mapping.SelectionMode.New);
            }
            return geoElement;
        }

        private async Task<Graphic> SetSelectForGraphicsOverlay(Esri.ArcGISRuntime.UI.Controls.GeoViewInputEventArgs e, GraphicsOverlay target)
        {
            IdentifyGraphicsOverlayResult result = null;

            try
            {
                // Identify the tapped graphics
                result = await MySceneView.IdentifyGraphicsOverlayAsync(target, e.Position, 1, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }

            // Return if there are no results
            if (result == null || result.Graphics.Count < 1)
            {
                return null;
            }

            // Get the first identified graphic
            var graphic = result.Graphics.First();

            // Clear any existing selection, then select the tapped graphic
            target.ClearSelection();

            graphic.IsSelected = true;

            return graphic;
        }


        #endregion

        #region 判断关系
        private async void CheckOBBCollision_Click(object sender, RoutedEventArgs e)
        {
            //Test();
            //Test2();
            //Test3();


            if (selectGraphic == null || selectFeatureGeoElement == null)
            {
                MessageBox.Show("请选择一个shp数据和一个绘制数据!");
                return;
            }

            //创建shp几何体
            Esri.ArcGISRuntime.Geometry.Polygon selectFeatureGeometryRealCube = GetSelectFeatureGeometryRealCube(selectFeatureGeoElement);
            

            //创建绘画几何体
            Esri.ArcGISRuntime.Geometry.Polygon selectGraphicGeometryRealCube = GetSelectFeatureGeometryRealCube(selectGraphic);

            var b = GeometryEngine.Intersects(selectGraphicGeometryRealCube, selectFeatureGeometryRealCube);
            var g2 = GeometryEngine.Intersections(selectGraphicGeometryRealCube, selectFeatureGeometryRealCube);

            if (b)
            {
                foreach (var g in g2)
                {
                    if(g is Esri.ArcGISRuntime.Geometry.Polygon)
                    {
                        var p = g as Esri.ArcGISRuntime.Geometry.Polygon;
                        foreach(var part in p.Parts)
                        {
                            var feature = intersectionLayer.FeatureTable.CreateFeature();
                            feature.Attributes.Remove("Z");
                            double z = double.MaxValue;
                            List<MapPoint> pointsWithZ = new List<MapPoint>();
                            foreach (var point in part.Points)
                            {
                                //只能计算平面体（即同一平面Z相同的数据）
                                if (point.Z < z)
                                {
                                    z = point.Z;
                                }
                                
                                pointsWithZ.Add(new MapPoint(point.X, point.Y, point.SpatialReference));
                            }
                            feature.Attributes.Add("Z", z);
                            Esri.ArcGISRuntime.Geometry.Polygon polygon = new Esri.ArcGISRuntime.Geometry.Polygon(pointsWithZ, pointsWithZ[0].SpatialReference);
                            feature.Geometry = polygon;
                            await intersectionLayer.FeatureTable.AddFeatureAsync(feature);

                            graphicLayer.ClearSelection();
                            featureLayer.ClearSelection();
                        }
                    }
                }

                MessageBox.Show("二者重叠");
            }
            else
            {
                MessageBox.Show("二者不重叠");
            }
        }

     
        private Esri.ArcGISRuntime.Geometry.Polygon GetSelectFeatureGeometryRealCube(GeoElement geoElement)
        {
            Esri.ArcGISRuntime.Geometry.Polygon selectFeatureGeometryRealCube = null;
            var feature = geoElement as Feature;
            var selectFeatureGeometryPolygon = geoElement.Geometry as Esri.ArcGISRuntime.Geometry.Polygon;
            if (selectFeatureGeometryPolygon.Parts.Count > 0)
            {
                List<MapPoint> points = new List<MapPoint>();
                var part = selectFeatureGeometryPolygon.Parts[0];
                foreach (var point in part.Points)
                {
                    //从属性中获得z
                    object z = 0;
                    feature.Attributes.TryGetValue("Z", out z);
                    points.Add(new MapPoint(point.X, point.Y, (double)z, selectFeatureGeometryPolygon.SpatialReference));
                }
                selectFeatureGeometryRealCube = new Esri.ArcGISRuntime.Geometry.Polygon(points, selectFeatureGeometryPolygon.SpatialReference);
            }

            return selectFeatureGeometryRealCube;
        }

        private void Test()
        {
            var onMapLocation = new MapPoint(0, 0, 0, SpatialReferences.Wgs84);

            var num = 1;
            List<MapPoint> points = new List<MapPoint>();

            points.Add(new MapPoint(onMapLocation.X, onMapLocation.Y + num, onMapLocation.Z + num, onMapLocation.SpatialReference));
            points.Add(new MapPoint(onMapLocation.X + num, onMapLocation.Y + num, onMapLocation.Z + num, onMapLocation.SpatialReference));
            points.Add(new MapPoint(onMapLocation.X + num, onMapLocation.Y, onMapLocation.Z + num, onMapLocation.SpatialReference));
            points.Add(new MapPoint(onMapLocation.X, onMapLocation.Y, onMapLocation.Z + num, onMapLocation.SpatialReference));

            Esri.ArcGISRuntime.Geometry.Polygon polygon1 = new Esri.ArcGISRuntime.Geometry.Polygon(points);

            var num2 = 2;
            points = new List<MapPoint>();
            points.Add(new MapPoint(onMapLocation.X, onMapLocation.Y + num2, onMapLocation.Z + num2, onMapLocation.SpatialReference));
            points.Add(new MapPoint(onMapLocation.X + num2, onMapLocation.Y + num2, onMapLocation.Z + num2, onMapLocation.SpatialReference));
            points.Add(new MapPoint(onMapLocation.X + num2, onMapLocation.Y, onMapLocation.Z + num2, onMapLocation.SpatialReference));
            points.Add(new MapPoint(onMapLocation.X, onMapLocation.Y, onMapLocation.Z + num2, onMapLocation.SpatialReference));

            Esri.ArcGISRuntime.Geometry.Polygon polygon2 = new Esri.ArcGISRuntime.Geometry.Polygon(points);

            var b= GeometryEngine.Intersects(polygon1, polygon2);
            var g3 = GeometryEngine.Intersection(polygon1, polygon2);
            var g2= GeometryEngine.Intersections(polygon1, polygon2);
            var g = g3 as Esri.ArcGISRuntime.Geometry.Polygon;
            foreach(var part in g.Parts)
            {
                foreach(var point in part.Points)
                {

                }
            }

        }

        private void Test2()
        {
            var onMapLocation = new MapPoint(0, 0, 0, SpatialReferences.Wgs84);

            var num =-1;
            List<MapPoint> points = new List<MapPoint>();

            points.Add(new MapPoint(onMapLocation.X, onMapLocation.Y + num, onMapLocation.Z + num, onMapLocation.SpatialReference));
            points.Add(new MapPoint(onMapLocation.X + num, onMapLocation.Y + num, onMapLocation.Z + num, onMapLocation.SpatialReference));
            points.Add(new MapPoint(onMapLocation.X + num, onMapLocation.Y, onMapLocation.Z + num, onMapLocation.SpatialReference));
            points.Add(new MapPoint(onMapLocation.X, onMapLocation.Y, onMapLocation.Z + num, onMapLocation.SpatialReference));

            Esri.ArcGISRuntime.Geometry.Polygon polygon1 = new Esri.ArcGISRuntime.Geometry.Polygon(points);

            var num2 = 2;
            points = new List<MapPoint>();
            points.Add(new MapPoint(onMapLocation.X, onMapLocation.Y + num2, onMapLocation.Z + num2, onMapLocation.SpatialReference));
            points.Add(new MapPoint(onMapLocation.X + num2, onMapLocation.Y + num2, onMapLocation.Z + num2, onMapLocation.SpatialReference));
            points.Add(new MapPoint(onMapLocation.X + num2, onMapLocation.Y, onMapLocation.Z + num2, onMapLocation.SpatialReference));
            points.Add(new MapPoint(onMapLocation.X, onMapLocation.Y, onMapLocation.Z + num2, onMapLocation.SpatialReference));

            Esri.ArcGISRuntime.Geometry.Polygon polygon2 = new Esri.ArcGISRuntime.Geometry.Polygon(points);

            var b = GeometryEngine.Intersects(polygon1, polygon2);
            var g3 = GeometryEngine.Intersection(polygon1, polygon2);
            var g2 = GeometryEngine.Intersections(polygon1, polygon2);

        }

        private void Test3()
        {
            var onMapLocation = new MapPoint(0, 0, 0, SpatialReferences.Wgs84);

            List<MapPoint> points = new List<MapPoint>();

            points.Add(new MapPoint(onMapLocation.X, onMapLocation.Y , onMapLocation.Z-3 , onMapLocation.SpatialReference));
            points.Add(new MapPoint(onMapLocation.X + 3, onMapLocation.Y , onMapLocation.Z , onMapLocation.SpatialReference));
            points.Add(new MapPoint(onMapLocation.X + 3, onMapLocation.Y+3, onMapLocation.Z , onMapLocation.SpatialReference));
            points.Add(new MapPoint(onMapLocation.X, onMapLocation.Y, onMapLocation.Z + 3, onMapLocation.SpatialReference));

            Esri.ArcGISRuntime.Geometry.Polygon polygon1 = new Esri.ArcGISRuntime.Geometry.Polygon(points);

            var num2 = 5;
            points = new List<MapPoint>();
            points.Add(new MapPoint(onMapLocation.X, onMapLocation.Y + num2, onMapLocation.Z + num2, onMapLocation.SpatialReference));
            points.Add(new MapPoint(onMapLocation.X + num2, onMapLocation.Y + num2, onMapLocation.Z + num2, onMapLocation.SpatialReference));
            points.Add(new MapPoint(onMapLocation.X + num2, onMapLocation.Y, onMapLocation.Z + num2, onMapLocation.SpatialReference));
            points.Add(new MapPoint(onMapLocation.X, onMapLocation.Y, onMapLocation.Z + num2, onMapLocation.SpatialReference));

            Esri.ArcGISRuntime.Geometry.Polygon polygon2 = new Esri.ArcGISRuntime.Geometry.Polygon(points);

            var b = GeometryEngine.Intersects(polygon1, polygon2);
            var g3 = GeometryEngine.Intersection(polygon1, polygon2);
            var g2 = GeometryEngine.Intersections(polygon1, polygon2);

        }

        #endregion

        #region 观察者
        /// <summary>
        /// 移动观察者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="mouseEventArgs"></param>
        private void MySceneViewOnMoveViewPoint(object sender, MouseEventArgs mouseEventArgs)
        {
            // Get the mouse position.
            Point cursorSceenPoint = mouseEventArgs.GetPosition(MySceneView);

            // Get the corresponding MapPoint.
            MapPoint onMapLocation = MySceneView.ScreenToBaseSurface(cursorSceenPoint);

            // Return if the MapPoint is null. This might happen if mouse leaves SceneView area.
            if (onMapLocation == null)
            {
                return;
            }

            // Adjust the Z value of the MapPoint to reflect the selected height.
            onMapLocation = new MapPoint(onMapLocation.X, onMapLocation.Y, onMapLocation.Z + _viewHeight);

            // Update the viewshed.
            _viewshed.Location = onMapLocation;

            // Update the viewpoint symbol.
            _viewpointOverlay.Graphics.Clear();
            _viewpointOverlay.Graphics.Add(new Graphic(onMapLocation, _viewpointSymbol));
        }

        /// <summary>
        /// 修改观察者的设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleSettingsChange(object sender, RoutedEventArgs e)
        {
            // Return if viewshed hasn't been created yet. This happens when the sample is starting.
            if (_viewshed == null)
            {
                return;
            }
            // Calculate the difference between the old and new height.
            double difference = HeightSlider.Value - _viewHeight;

            // Update the view height value to the new value.
            _viewHeight = HeightSlider.Value;

            // Move the viewshed to the new height.
            _viewshed.Location = new MapPoint(_viewshed.Location.X, _viewshed.Location.Y, _viewshed.Location.Z + difference);

            // Update the viewshed settings.
            _viewshed.Heading = HeadingSlider.Value;
            _viewshed.Pitch = PitchSlider.Value;
            _viewshed.HorizontalAngle = HorizontalAngleSlider.Value;
            _viewshed.VerticalAngle = VerticalAngleSlider.Value;
            _viewshed.MinDistance = MinimumDistanceSlider.Value;
            _viewshed.MaxDistance = MaximumDistanceSlider.Value;

            // Update visibility of the viewshed analysis.
            _viewshed.IsVisible = (bool)AnalysisVisibilityCheck.IsChecked;

            // Update visibility of the frustum. Note that the frustum will be invisible
            //     regardless of this setting if the viewshed analysis is not visible.
            _viewshed.IsFrustumOutlineVisible = (bool)FrustumVisibilityCheck.IsChecked;

            // Update the viewpoint graphic.
            _viewpointOverlay.Graphics.Clear();
            _viewpointOverlay.Graphics.Add(new Graphic(_viewshed.Location, _viewpointSymbol));

        }


        #endregion

        #region 坦克观察者
        private void AnimateTank()
        {
            // Return if the tank already arrived.
            if (_tankEndPoint == null)
            {
                return;
            }

            GeodeticDistanceResult distance = MoveTank(_tankEndPoint);

            // Clear the destination if the tank already arrived.
            if (distance.Distance < 5)
            {
                _tankEndPoint = null;
            }
        }

        /// <summary>
        /// 移动一小段距离，并且转向
        /// </summary>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private GeodeticDistanceResult MoveTank(MapPoint endPoint)
        {
            // Get the current location and distance from the destination.
            MapPoint location = (MapPoint)_tank.Geometry;
            //var rightMapPoint = GeometryEngine.Project(location, endPoint.SpatialReference) as MapPoint;
            GeodeticDistanceResult distance = GeometryEngine.DistanceGeodetic(
                location, endPoint, _metersUnit, _degreesUnit, GeodeticCurveType.Geodesic);

            // Move the tank a short distance.
            location = GeometryEngine.MoveGeodetic(new List<MapPoint>() { location }, 1.0, _metersUnit, distance.Azimuth1, _degreesUnit,
                GeodeticCurveType.Geodesic).First();
            _tank.Geometry = location;

            // Rotate to face the destination.
            double heading = (double)_tank.Attributes["HEADING"];
            heading = heading + (distance.Azimuth1 - heading) / 10;
            _tank.Attributes["HEADING"] = heading;

            return distance;
        }
        #endregion
    }
}
