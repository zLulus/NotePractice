using ArcGIS3D.WpfDemo.Dialogs;
using ArcGIS3D.WpfDemo.Enums;
using ArcGIS3D.WpfDemo.OBBCollisions;
using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI;
using Esri.ArcGISRuntime.UI.Controls;
using Esri.ArcGISRuntime.UI.GeoAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for ViewshedLocation.xaml
    /// </summary>
    public partial class ViewshedLocation : UserControl
    {
        // Hold the URL to the elevation source.
        private readonly Uri _localElevationImageService = new Uri("https://scene.arcgis.com/arcgis/rest/services/BREST_DTM_1M/ImageServer");

        // Hold the URL to the buildings scene layer.
        private readonly Uri _buildingsUrl = new Uri("https://tiles.arcgis.com/tiles/P3ePLMYs2RVChkJx/arcgis/rest/services/Buildings_Brest/SceneServer/layers/0");

        ArcGISSceneLayer sceneLayer;

        // Hold a reference to the viewshed analysis.
        private LocationViewshed _viewshed;

        // Hold a reference to the analysis overlay that will hold the viewshed analysis.
        private AnalysisOverlay _analysisOverlay;

        // Graphics overlay for viewpoint symbol.
        private GraphicsOverlay _viewpointOverlay;

        // Symbol for viewpoint.
        private SimpleMarkerSceneSymbol _viewpointSymbol;

        /// <summary>
        /// 移动观察者-是否绘制完毕
        /// Flag indicating if the viewshed will move with the mouse.
        /// </summary>
        private bool subscribedToMouseViewPoint;

        // Height of the viewpoint above the ground.
        private double _viewHeight;

        GraphicsOverlay overlay;

        TapTypeEnum tapTypeEnum { get; set; }
        bool isFirstDrawByPolygon;

        public ViewshedLocation()
        {
            InitializeComponent();

            // Initialize the sample.
            Initialize();
        }

        private void Initialize()
        {
            

            tapTypeEnum = TapTypeEnum.None;

            _viewHeight = HeightSlider.Value;

            // Create the scene with the imagery basemap.
            Scene myScene = new Scene(Basemap.CreateImagery());
            MySceneView.Scene = myScene;

            // Add the surface elevation.
            Surface mySurface = new Surface();
            mySurface.ElevationSources.Add(new ArcGISTiledElevationSource(_localElevationImageService));
            myScene.BaseSurface = mySurface;

            //基础图层-白色建筑
            // Add the scene layer.
            sceneLayer = new ArcGISSceneLayer(_buildingsUrl);
            myScene.OperationalLayers.Add(sceneLayer);

            // Create the MapPoint representing the initial location.
            MapPoint initialLocation = new MapPoint(-4.5, 48.4, 46 + _viewHeight);

            // Create the location viewshed analysis.
            _viewshed = new LocationViewshed(
                initialLocation,
                HeadingSlider.Value,
                PitchSlider.Value,
                HorizontalAngleSlider.Value,
                VerticalAngleSlider.Value,
                MinimumDistanceSlider.Value,
                MaximumDistanceSlider.Value);

            //初始位置摄像头
            // Create a camera based on the initial location.
            Camera camera = new Camera(initialLocation, 200.0, 20.0, 70.0, 0.0);

            // Create a symbol for the viewpoint.
            _viewpointSymbol = SimpleMarkerSceneSymbol.CreateSphere(System.Drawing.Color.Blue, 10, SceneSymbolAnchorPosition.Center);

            // Add the symbol to the viewpoint overlay.
            _viewpointOverlay = new GraphicsOverlay
            {
                SceneProperties = new LayerSceneProperties(SurfacePlacement.Absolute)
            };
            _viewpointOverlay.Graphics.Add(new Graphic(initialLocation, _viewpointSymbol));

            // Apply the camera to the scene view.
            MySceneView.SetViewpointCamera(camera);

            // Create an analysis overlay for showing the viewshed analysis.
            _analysisOverlay = new AnalysisOverlay();

            // Add the viewshed analysis to the overlay.
            _analysisOverlay.Analyses.Add(_viewshed);

            // Add the analysis overlay to the SceneView.
            MySceneView.AnalysisOverlays.Add(_analysisOverlay);

            // Add the graphics overlay
            MySceneView.GraphicsOverlays.Add(_viewpointOverlay);

            // Update the frustum outline Color.
            // The frustum outline shows the volume in which the viewshed analysis is performed.
            Viewshed.FrustumOutlineColor = System.Drawing.Color.Blue;

            // Subscribe to tap events. This enables the 'pick up' and 'drop' workflow for moving the viewpoint.
            MySceneView.GeoViewTapped += MySceneViewOnGeoViewTapped;


            // Create the graphics overlay.
            overlay = new GraphicsOverlay();

            // Set the surface placement mode for the overlay.
            overlay.SceneProperties.SurfacePlacement = SurfacePlacement.Absolute;
            MySceneView.GraphicsOverlays.Add(overlay);

        }

        private void MySceneViewOnGeoViewTapped(object sender, GeoViewInputEventArgs geoViewInputEventArgs)
        {
            //移动观察者
            if (tapTypeEnum==TapTypeEnum.MoveViewPoint)
            {
                // The viewshed observer is picked up and moving. Drop it.
                if (subscribedToMouseViewPoint)
                {
                    MySceneView.MouseMove -= MySceneViewOnMoveViewPoint;
                }
                // The viewshed observer is currently pinned. Pick it up.
                else
                {
                    MySceneView.MouseMove += MySceneViewOnMoveViewPoint;
                }

                // Toggle the viewshed movement flag.
                subscribedToMouseViewPoint = !subscribedToMouseViewPoint;
            }
            //绘制立方体
            else if(tapTypeEnum==TapTypeEnum.DrawByPolygon)
            {
                if (isFirstDrawByPolygon == false)
                {
                    points = new List<MapPoint>();
                    //预览
                    //MySceneView.MouseMove
                    //编辑
                    MySceneView.PreviewMouseLeftButtonDown += MySceneViewOnMouseMoveAddPoint;
                }
                isFirstDrawByPolygon = true;
            }
            else if (tapTypeEnum == TapTypeEnum.DrawByCenter)
            {
                
            }
            else if (tapTypeEnum == TapTypeEnum.Select)
            {
                
            }
        }

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

            foreach (var a in _analysisOverlay.Analyses)
            {

            }
        }
        #endregion

        #region 绘制
        List<MapPoint> points;
        private void MySceneViewOnMouseMoveAddPoint(object sender, MouseEventArgs mouseEventArgs)
        {
            if (points.Count < 3)
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
            if (points.Count >= 3)
            {
                SetHeight setHeight = new SetHeight();
                var dialogResult= setHeight.ShowDialog();
                if(dialogResult.HasValue && dialogResult.Value)
                {
                    //绘制
                    //var lastPoint = new MapPoint(points[0].X, points[2].Y, points[0].Z, points[0].SpatialReference);
                    //var centerX = (points[0].X + points[2].X) / 2.0;
                    //var centerY = (points[0].Y + points[2].Y) / 2.0;
                    //var centerZ = points[0].Z;
                    //var centerPoint = new MapPoint(centerX, centerY, centerZ, points[0].SpatialReference);
                    //Esri.ArcGISRuntime.Geometry.Polygon polygon = new Esri.ArcGISRuntime.Geometry.Polygon(points);
                    //polygon.Dimension


                    //SimpleMarkerSceneSymbol symbol = SimpleMarkerSceneSymbol.CreateCube(System.Drawing.Color.DarkSeaGreen, 1, SceneSymbolAnchorPosition.Center);
                    //旋转角度
                    //symbol.Heading
                    //z
                    //symbol.Height = setHeight.height;
                    //x
                    //symbol.Width = Distance(points[0], points[1]);
                    //y
                    //symbol.Depth = Distance(points[1], points[2]);
                    //symbol.Width = 20;
                    //symbol.Depth = 50;
                    //var _distanceMeasurement = new LocationDistanceMeasurement(points[0], points[1]);
                    // Create the graphic from the geometry and the symbol.
                    //Graphic item = new Graphic(centerPoint, symbol);

                    // Add the graphic to the overlay.
                    //overlay.Graphics.Add(item);

                    Esri.ArcGISRuntime.Geometry.PointCollection thePointCollection = new Esri.ArcGISRuntime.Geometry.PointCollection(SpatialReferences.Wgs84);
                    foreach (var p in points)
                    {
                        thePointCollection.Add(p.X, p.Y, p.Z);
                    }
                    for(int i= points.Count-1;i<0;i--)
                    {
                        thePointCollection.Add(points[i].X, points[i].Y, points[i].Z + 100);
                    }

                    // Create a polyline from the point collection.
                    Esri.ArcGISRuntime.Geometry.Polygon thePolyline = new Esri.ArcGISRuntime.Geometry.Polygon(thePointCollection);
                    SimpleFillSymbol simpleFillSymbol = new SimpleFillSymbol(SimpleFillSymbolStyle.Solid, System.Drawing.Color.AliceBlue, null);
                    Graphic item = new Graphic(thePolyline, simpleFillSymbol);
                    overlay.Graphics.Add(item);

                    points = new List<MapPoint>();
                }
              
            }
           
        }

        public static double Distance(MapPoint point1, MapPoint point2)
        {
            return Distance((decimal)point1.X, (decimal)point1.Y, (decimal)point2.X, (decimal)point2.Y);
        }

        public static double Distance(decimal x1, decimal y1, decimal x2, decimal y2)
        {
            decimal d = (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2);
            
            double result = Math.Sqrt((double)d);

            return result;

        }
        #endregion

        #region 绘制-中心点
        private void MySceneViewOnDrawByCenter(object sender, MouseEventArgs mouseEventArgs)
        {
            // Get the mouse position.
            Point cursorSceenPoint = mouseEventArgs.GetPosition(MySceneView);

            // Get the corresponding MapPoint.
            MapPoint onMapLocation = MySceneView.ScreenToBaseSurface(cursorSceenPoint);

            SetCubeInfo setCubeInfo = new SetCubeInfo(onMapLocation.X, onMapLocation.Y, onMapLocation.Z);
            var dialogResult = setCubeInfo.ShowDialog();
            if(dialogResult.HasValue && dialogResult.Value)
            {
                var centerPoint = new MapPoint(setCubeInfo.vm.X, setCubeInfo.vm.Y, setCubeInfo.vm.Z, onMapLocation.SpatialReference);

                SimpleMarkerSceneSymbol symbol = SimpleMarkerSceneSymbol.CreateCube(System.Drawing.Color.DarkSeaGreen, 1, SceneSymbolAnchorPosition.Center);
                //旋转角度
                symbol.Heading = setCubeInfo.vm.Heading;
                //z
                symbol.Height = setCubeInfo.vm.Height;
                //x
                symbol.Width = setCubeInfo.vm.Width;
                //y
                symbol.Depth = setCubeInfo.vm.Depth;
                // Create the graphic from the geometry and the symbol.
                Graphic item = new Graphic(centerPoint, symbol);

                // Add the graphic to the overlay.
                overlay.Graphics.Add(item);

                MySceneView.PreviewMouseLeftButtonDown -= MySceneViewOnDrawByCenter;
            }
           
        }
        #endregion

        #region 选择
        private async void MySceneViewOnSelect(object sender, Esri.ArcGISRuntime.UI.Controls.GeoViewInputEventArgs e)
        {
            //白色建筑
            //await SetSelectFeature(e);
            //绘制图层
            await SetSelect(e);
        }

        private async Task SetSelect(Esri.ArcGISRuntime.UI.Controls.GeoViewInputEventArgs e)
        {
            IdentifyGraphicsOverlayResult result = null;

            try
            {
                // Identify the tapped graphics
                result = await MySceneView.IdentifyGraphicsOverlayAsync(overlay, e.Position, 1, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }

            // Return if there are no results
            if (result == null || result.Graphics.Count < 1)
            {
                return;
            }

            // Get the first identified graphic
            Graphic identifiedGraphic = result.Graphics.First();

            // Clear any existing selection, then select the tapped graphic
            overlay.ClearSelection();
            identifiedGraphic.IsSelected = true;

            // Get the selected graphic's geometry
            Esri.ArcGISRuntime.Geometry.Geometry selectedGeometry = identifiedGraphic.Geometry;

        }

        private async Task SetSelectFeature(GeoViewInputEventArgs e)
        {
            try
            {
                // Get the scene layer from the scene (first and only operational layer).
                ArcGISSceneLayer sceneLayer = (ArcGISSceneLayer)MySceneView.Scene.OperationalLayers.First();

                // Clear any existing selection.
                sceneLayer.ClearSelection();
                // Identify the layer at the tap point.
                // Use a 10-pixel tolerance around the point and return a maximum of one feature.
                //最多返回6个特征
                IdentifyLayerResult result = await MySceneView.IdentifyLayerAsync(sceneLayer, e.Position, 10, false, 6);

                // Get the GeoElements that were identified (will be 0 or 1 element).
                IReadOnlyList<GeoElement> geoElements = result.GeoElements;

                // If a GeoElement was identified, select it in the scene.
                if (geoElements.Any())
                {
                    GeoElement geoElement = geoElements.FirstOrDefault();
                    if (geoElement != null)
                    {
                        // Select the feature to highlight it in the scene view.
                        sceneLayer.SelectFeature((Feature)geoElement);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }
        #endregion

        #region 切换模式
        private void Draw_Click(object sender, RoutedEventArgs e)
        {
            MySceneView.GeoViewTapped -= MySceneViewOnSelect;
            tapTypeEnum = TapTypeEnum.DrawByPolygon;
            isFirstDrawByPolygon = false;
        }

        private void ChangeModeViewPointStatus_Click(object sender, RoutedEventArgs e)
        {
            MySceneView.GeoViewTapped -= MySceneViewOnSelect;
            tapTypeEnum = TapTypeEnum.MoveViewPoint;
        }

        private void ChangeModeStatus_Click(object sender, RoutedEventArgs e)
        {
            MySceneView.GeoViewTapped -= MySceneViewOnSelect;
            tapTypeEnum = TapTypeEnum.None;
        }

        private void DrawByCenter_Click(object sender, RoutedEventArgs e)
        {
            MySceneView.GeoViewTapped -= MySceneViewOnSelect;
            tapTypeEnum = TapTypeEnum.DrawByCenter;
            MySceneView.PreviewMouseLeftButtonDown += MySceneViewOnDrawByCenter;
        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            tapTypeEnum = TapTypeEnum.Select;
            MySceneView.GeoViewTapped += MySceneViewOnSelect;
        }
        #endregion

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            overlay.Graphics.Clear();
        }

        #region 测试
        private void TestCreateCube(double x ,double y,double z)
        {
            SimpleMarkerSceneSymbol symbol = SimpleMarkerSceneSymbol.CreateCube(System.Drawing.Color.LightPink, 500, SceneSymbolAnchorPosition.Center);
            //角度
            symbol.Heading = 120;
            //高 z
            symbol.Height = 50;
            //x
            symbol.Width = 100;
            //y
            symbol.Depth = 150;
            // Create the graphic from the geometry and the symbol.
            Graphic item = new Graphic(new MapPoint(x, y, z), symbol);

            // Add the graphic to the overlay.
            overlay.Graphics.Add(item);
        }

        private void TestCreate(double x, double y, double z)
        {
            //SimpleMarkerSceneSymbol symbol = SimpleMarkerSceneSymbol.CreateSphere(System.Drawing.Color.LightPink, 50, SceneSymbolAnchorPosition.Center);
            //圆柱体
            //SimpleMarkerSceneSymbol symbol = SimpleMarkerSceneSymbol.CreateCylinder(System.Drawing.Color.LightPink, 50, 80, SceneSymbolAnchorPosition.Center);
            //多面体
            //SimpleMarkerSceneSymbol symbol = SimpleMarkerSceneSymbol.CreateDiamond(System.Drawing.Color.LightPink, 50,80, SceneSymbolAnchorPosition.Center);
            //四面体
            SimpleMarkerSceneSymbol symbol = SimpleMarkerSceneSymbol.CreateTetrahedron(System.Drawing.Color.LightPink, 50,80, SceneSymbolAnchorPosition.Center);
            // Create the graphic from the geometry and the symbol.
            Graphic item = new Graphic(new MapPoint(x, y, z), symbol);

            // Add the graphic to the overlay.
            overlay.Graphics.Add(item);
        }

        private void TestAnalysis_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MapPoint location1 = new MapPoint(-4.5, 48.4, 46 + _viewHeight);
                MapPoint location2 = new MapPoint(-4.5, 48.4, 46 + _viewHeight);

                var b = GeometryEngine.Overlaps(location1, location2);
                var b2= GeometryEngine.Intersection(location1, location2);
                var b3 = GeometryEngine.Within(location1, location2);

                List<MapPoint> points = new List<MapPoint>();
                points.Add(new MapPoint(-4.5, 48.4, 46 + _viewHeight));
                points.Add(new MapPoint(-4.5, 43, 46 + _viewHeight));
                var polyline = new Esri.ArcGISRuntime.Geometry.Polyline(points);
                var g1=  GeometryEngine.Difference(location1, polyline);
            }
            catch(Exception ex)
            {

            }
           
        }

        #endregion

        private void TestLayer_Click(object sender, RoutedEventArgs e)
        {
            var f = sceneLayer.FeatureTable;
            //f.QueryFeaturesAsync
            
            var sel = sceneLayer.GetSelectedFeaturesAsync().Result.ToList();
        }

        private void TestOBBCollision_Click(object sender, RoutedEventArgs e)
        {
            // create two obbs
            OBB A, B;

            // set the first obb's properties
            A = new OBB();
            // set its center position
            A.Pos = new vec3(0, 0, 0);

            // set the half size
            A.Half_size = new vec3(10, 1, 1);

            // set the axes orientation
            A.AxisX = new vec3(1, 0, 0);

            A.AxisY = new vec3(0, 1, 0);

            A.AxisZ = new vec3(0, 0, 1);

            // set the second obb's properties
            B = new OBB();
            // set its center position
            B.Pos = new vec3(20, 0, 0);

            // set the half size
            B.Half_size = new vec3(10, 1, 1);

            // set the axes orientation
            B.AxisX = new vec3(1, 0, 0);
            B.AxisY = new vec3(0, 1, 0);
            B.AxisZ = new vec3(0, 0, 1);

            // run the code and get the result as a message
            if (OBBCollision.getCollision(A, B))
            {
                Console.WriteLine("Collision!!!");
            }
            else
            {
                Console.WriteLine("No collision.");
            }

        }
    }
}
