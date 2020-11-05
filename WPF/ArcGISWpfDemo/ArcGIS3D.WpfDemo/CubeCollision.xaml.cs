using ArcGIS3D.WpfDemo.Dialogs;
using ArcGIS3D.WpfDemo.Enums;
using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Rasters;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI;
using Esri.ArcGISRuntime.UI.Controls;
using Esri.ArcGISRuntime.UI.GeoAnalysis;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlTypes;
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
    /// Interaction logic for CubeCollision.xaml
    /// </summary>
    public partial class CubeCollision : UserControl
    {
        /// <summary>
        /// 点击模式
        /// </summary>
        TapTypeEnum tapTypeEnum { get; set; }
        /// <summary>
        /// 绘画图层
        /// </summary>
        GraphicsOverlay graphicOverlay { get; set; }
        /// <summary>
        /// 重叠结果显示图层
        /// </summary>
        GraphicsOverlay intersectionOverlay { get; set; }
        /// <summary>
        /// shp图层
        /// </summary>
        FeatureLayer featureLayer { get; set; }

        string ShpFilePath
        {
            get { return ConfigurationManager.AppSettings["ShpFilePath"]; }
        }
        string TifFilePath
        {
            get { return ConfigurationManager.AppSettings["TifFilePath"]; }
        }

        /// <summary>
        /// 选择的shp图层要素
        /// </summary>
        GeoElement selectFeatureGeoElement { get; set; }
        /// <summary>
        /// 选择的绘制图层要素
        /// </summary>
        Graphic selectGraphic { get; set; }
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

        public CubeCollision()
        {
            InitializeComponent();


            InitializeMap();
        }

        #region 初始化
        private async void InitializeMap()
        {
            //shp
            await InitializeFeatureLayer();

            //tiff
            InitializeImageOverlay();

            //绘图图层
            InitializeGraphicsOverlay();

            //重叠结果显示图层
            InitializeIntersectionOverlay();

            //观察者
            InitializeViewshed();
        }

        private void InitializeViewshed()
        {
            var initialLocation = new MapPoint(105.67956087176, 32.0470744099947, -9.31322574615479E-10, featureLayer.SpatialReference);
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
            _viewpointOverlay.Graphics.Add(new Graphic(initialLocation, _viewpointSymbol));


            // Create an analysis overlay for showing the viewshed analysis.
            _analysisOverlay = new AnalysisOverlay();

            // Add the viewshed analysis to the overlay.
            _analysisOverlay.Analyses.Add(_viewshed);

            // Add the analysis overlay to the SceneView.
            MySceneView.AnalysisOverlays.Add(_analysisOverlay);

            // Add the graphics overlay
            MySceneView.GraphicsOverlays.Add(_viewpointOverlay);

            // Subscribe to tap events. This enables the 'pick up' and 'drop' workflow for moving the viewpoint.
            MySceneView.GeoViewTapped += MySceneViewOnGeoViewTapped;
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
                //var spF = featureLayer.SpatialReference;
                //var spFG = featureLayer.SpatialReference.BaseGeographic;

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

                //
                featureLayer.Opacity = 0.7;

                //设置底图样式
                MySceneView.Scene = new Scene(BasemapType.DarkGrayCanvasVector);
                // Add the feature layer to the map
                MySceneView.Scene.Basemap = new Basemap(featureLayer);
                //var bSp = MySceneView.Scene.Basemap.BaseLayers[0].SpatialReference;
                //var sp = MySceneView.SpatialReference;
                // Zoom the map to the extent of the shapefile
                await MySceneView.SetViewpointAsync(new Viewpoint(myShapefile.Extent));
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
                //////https://developers.arcgis.com/net/latest/wpf/guide/add-image-overlays.htm
                ////////// Create an Envelope for displaying the image frame in the correct location
                ////var sp = new SpatialReference(4523);
                ////MapPoint centerPoint = new MapPoint(105.67956087176, 32.0470744099947, -9.31322574615479E-10, featureLayer.SpatialReference);
                ////double width = 15.0959;
                ////double height = 14.3770;
                ////Esri.ArcGISRuntime.Geometry.Envelope pacificSouthwestEnvelope = new Envelope(centerPoint, width, height);
                //Esri.ArcGISRuntime.Geometry.Envelope pacificSouthwestEnvelope =featureLayer.FullExtent;//new Envelope(, 3547066.496987, 35564412.860201, 3547500.100019, sp);

                //////// Create an ImageFrame with a local image file and the extent envelope  
                //ImageFrame imageFrame = new ImageFrame(new System.Uri(TifFilePath), pacificSouthwestEnvelope);
                ////ImageFrame imageFrame = new ImageFrame(image, pacificSouthwestEnvelope);

                //////// Add the ImageFrame to an ImageOverlay and set it to be 50% transparent
                //ImageOverlay imageOverlay = new ImageOverlay(imageFrame);
                ////透明度
                //imageOverlay.Opacity = 1;

                ////// Add the ImageOverlay to the scene view's ImageOverlay collection
                ////MySceneView.Overlays.Items.Add(imageOverlay);
                //MySceneView.ImageOverlays.Add(imageOverlay);
                /////todo 没有加载出来
                ////imageFrame.LoadAsync().Wait();
                ////await MySceneView.SetViewpointAsync(new Viewpoint(imageFrame.Extent));



                //ArcGISTiledElevationSource elevationSource = new ArcGISTiledElevationSource(new Uri(TifFilePath));
                
                //Surface mySurface = new Surface();
                //mySurface.ElevationSources.Add(elevationSource);
                //MySceneView.Scene.BaseSurface = mySurface;



                //RasterLayer rasterLayer = new RasterLayer(TifFilePath);
                //var l = rasterLayer as ImageAdjustmentLayer;
                //MySceneView.AnalysisOverlays.Add()
                //MySceneView.ImageOverlays.Add(l.Item) ;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void InitializeGraphicsOverlay()
        {
            //绘画图层
            // Create the graphics overlay.
            graphicOverlay = new GraphicsOverlay();
            //缩小放大
            graphicOverlay.ScaleSymbols = false;
            //透明度
            graphicOverlay.Opacity = 0.7;
            //#region 绘制高程
            //// Create a new simple line symbol for the feature layer
            //SimpleLineSymbol mySimpleLineSymbol = new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, System.Drawing.Color.Black, 1);

            //// Create a new simple fill symbol for the feature layer 
            //SimpleFillSymbol mysimpleFillSymbol = new SimpleFillSymbol(SimpleFillSymbolStyle.Solid, System.Drawing.Color.WhiteSmoke, mySimpleLineSymbol);

            //// Create a new simple renderer for the feature layer
            //SimpleRenderer mySimpleRenderer = new SimpleRenderer(mysimpleFillSymbol);
            

            //// Get the scene properties from the simple renderer
            //RendererSceneProperties myRendererSceneProperties = mySimpleRenderer.SceneProperties;

            //// Set the extrusion mode for the scene properties
            //myRendererSceneProperties.ExtrusionMode = ExtrusionMode.AbsoluteHeight;

            //// Set the initial extrusion expression
            //myRendererSceneProperties.ExtrusionExpression = "[Z]";

            //// Set the feature layer's renderer to the define simple renderer
            //graphicOverlay.Renderer = mySimpleRenderer;
            //#endregion

            // Set the surface placement mode for the overlay.
            graphicOverlay.SceneProperties.SurfacePlacement = SurfacePlacement.Absolute;
            MySceneView.GraphicsOverlays.Add(graphicOverlay);

        }



        private void InitializeIntersectionOverlay()
        {
            intersectionOverlay = new GraphicsOverlay();
            intersectionOverlay.ScaleSymbols = false;
            intersectionOverlay.SceneProperties.SurfacePlacement = SurfacePlacement.Absolute;
            MySceneView.GraphicsOverlays.Add(intersectionOverlay);
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

        private void DrawByCenter_Click(object sender, RoutedEventArgs e)
        {
            ResetEvent();
            tapTypeEnum = TapTypeEnum.DrawByCenter;
            MySceneView.PreviewMouseLeftButtonDown += MySceneViewOnDrawByCenter;
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

        private void ResetEvent()
        {
            MySceneView.MouseMove -= MySceneViewOnMoveViewPoint;
            MySceneView.GeoViewTapped -= MySceneViewOnSelectFeatureLayer;
            MySceneView.GeoViewTapped -= MySceneViewOnSelectGraphicLayer;
            MySceneView.PreviewMouseLeftButtonDown -= MySceneViewOnDrawByCenter;
            MySceneView.PreviewMouseLeftButtonDown -= MySceneViewOnMouseMoveDrawByPolygon;
        }
        #endregion

        #region 绘制
        List<MapPoint> points;
        private void MySceneViewOnMouseMoveDrawByPolygon(object sender, MouseEventArgs mouseEventArgs)
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
                    try
                    {
                        //上
                        List<MapPoint> pointsWithZ = new List<MapPoint>();
                        foreach (var p in points)
                        {
                            pointsWithZ.Add(new MapPoint(p.X, p.Y, setHeight.height, p.SpatialReference));
                            //pointsWithZ.Add(MapPoint.CreateWithM(p.X, p.Y, p.Z + setHeight.height, setHeight.height, p.SpatialReference));
                        }
                        Esri.ArcGISRuntime.Geometry.Polygon polygon = new Esri.ArcGISRuntime.Geometry.Polygon(pointsWithZ, points[0].SpatialReference);
                        DrawOnePolygon(polygon);
                        //下
                        pointsWithZ = new List<MapPoint>();
                        foreach (var p in points)
                        {
                            pointsWithZ.Add(new MapPoint(p.X, p.Y, 0, p.SpatialReference));
                        }
                        polygon = new Esri.ArcGISRuntime.Geometry.Polygon(pointsWithZ, points[0].SpatialReference);
                        DrawOnePolygon(polygon);
                        //左
                        //右
                        //前
                        //后
                        //CreateOneSide(setHeight.height, 0, 1);
                        //CreateOneSide(setHeight.height, 1, 2);
                        //CreateOneSide(setHeight.height, 2, 3);
                        //CreateOneSide(setHeight.height, 3, 0);

                        MySceneView.PreviewMouseLeftButtonDown -= MySceneViewOnMouseMoveDrawByPolygon;
                        points = new List<MapPoint>();
                    }
                    catch(Exception ex)
                    {

                    }
                
                }

            }

        }

        private void CreateOneSide(double height,int index1,int index2)
        {
            List<MapPoint> pointsWithZ = new List<MapPoint>();
            MapPoint p1 = points[0];
            MapPoint p2 = points[1];
            pointsWithZ.Add(new MapPoint(p1.X, p1.Y, 0, p1.SpatialReference));
            pointsWithZ.Add(new MapPoint(p1.X, p1.Y, height, p1.SpatialReference));
            pointsWithZ.Add(new MapPoint(p2.X, p2.Y, 0, p2.SpatialReference));
            pointsWithZ.Add(new MapPoint(p2.X, p2.Y, height, p2.SpatialReference));
            var polygon = new Esri.ArcGISRuntime.Geometry.Polygon(pointsWithZ, p1.SpatialReference);
            DrawOnePolygon(polygon);
        }

        private void DrawOnePolygon(Esri.ArcGISRuntime.Geometry.Polygon polygon)
        {
            SimpleFillSymbol simpleFillSymbol = new SimpleFillSymbol(SimpleFillSymbolStyle.Solid, System.Drawing.Color.AliceBlue, null);
            Graphic item = new Graphic(polygon, simpleFillSymbol);

            graphicOverlay.Graphics.Add(item);
        }
        #endregion

        #region 清理图层
        private void ClearGraphicOverlay_Click(object sender, RoutedEventArgs e)
        {
            graphicOverlay.Graphics.Clear();
        }

        private void ClearIntersectionOverlay_Click(object sender, RoutedEventArgs e)
        {
            intersectionOverlay.Graphics.Clear();
        }
        #endregion

        #region 选择-高亮
        private async void MySceneViewOnSelectFeatureLayer(object sender, Esri.ArcGISRuntime.UI.Controls.GeoViewInputEventArgs e)
        {
            //shp图层
            await SetSelectForFeatureLayer(e);
        }

        private async void MySceneViewOnSelectGraphicLayer(object sender, Esri.ArcGISRuntime.UI.Controls.GeoViewInputEventArgs e)
        {
            //绘制图层
            selectGraphic= await SetSelectForGraphicsOverlay(e,graphicOverlay);
        }

        private async void MySceneViewOnSelectIntersectionOverlay(object sender, GeoViewInputEventArgs e)
        {
            graphicOverlay.ClearSelection();
            featureLayer.ClearSelection();
            //结果显示图层
            await SetSelectForGraphicsOverlay(e, intersectionOverlay);

            //selectGraphic.IsVisible = false;
            //featureLayer.IsVisible = false;
            //selectFeatureGeoElement.
        }

        private async Task SetSelectForFeatureLayer(GeoViewInputEventArgs e)
        {
            var result = await MySceneView.IdentifyLayerAsync(featureLayer, e.Position, 1, false);
            GeoElement geoElement = result.GeoElements.FirstOrDefault();
            if (geoElement != null)
            {
                selectFeatureGeoElement = geoElement;

                QueryParameters queryParams = new QueryParameters
                {
                    // Set the geometry to selection envelope for selection by geometry.
                    Geometry = selectFeatureGeoElement.Geometry //selectionEnvelope
                };
                // Select the features based on query parameters defined above.
                await featureLayer.SelectFeaturesAsync(queryParams, Esri.ArcGISRuntime.Mapping.SelectionMode.New);
            }
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

        #region 绘制-中心点
        private async void MySceneViewOnDrawByCenter(object sender, MouseEventArgs mouseEventArgs)
        {
            // Get the mouse position.
            System.Windows.Point cursorSceenPoint = mouseEventArgs.GetPosition(MySceneView);

            // Get the corresponding MapPoint.
            MapPoint onMapLocation = MySceneView.ScreenToBaseSurface(cursorSceenPoint);

            SetCubeInfo setCubeInfo = new SetCubeInfo(onMapLocation.X, onMapLocation.Y, onMapLocation.Z);
            var dialogResult = setCubeInfo.ShowDialog();
            if (dialogResult.HasValue && dialogResult.Value)
            {
                var centerPoint = new MapPoint(setCubeInfo.vm.X, setCubeInfo.vm.Y, setCubeInfo.vm.Z, onMapLocation.SpatialReference);

                //设置中心点为底部的中心点，否则显示的高度只有设置高度的一半（剩下的部分在地下）
                SimpleMarkerSceneSymbol symbol = SimpleMarkerSceneSymbol.CreateCube(System.Drawing.Color.DarkSeaGreen, 1, SceneSymbolAnchorPosition.Bottom);
                //旋转角度
                symbol.Heading = setCubeInfo.vm.Heading;
                //z 高
                symbol.Height = setCubeInfo.vm.Height;
                //长
                symbol.Width = setCubeInfo.vm.Width;
                //宽
                symbol.Depth = setCubeInfo.vm.Depth;
                
                // Create the graphic from the geometry and the symbol.
                Graphic item = new Graphic(centerPoint, symbol);

                // Add the graphic to the overlay.
                graphicOverlay.Graphics.Add(item);

                MySceneView.PreviewMouseLeftButtonDown -= MySceneViewOnDrawByCenter;
            }

            //根据多点绘制长方体
            //var num = 0.01;
            //List<MapPoint> points = new List<MapPoint>();
            ////points.Add(onMapLocation);
            ////points.Add(new MapPoint(onMapLocation.X + num, onMapLocation.Y, onMapLocation.Z, onMapLocation.SpatialReference));
            ////points.Add(new MapPoint(onMapLocation.X + num, onMapLocation.Y + num, onMapLocation.Z, onMapLocation.SpatialReference));
            ////points.Add(new MapPoint(onMapLocation.X, onMapLocation.Y + num, onMapLocation.Z, onMapLocation.SpatialReference));

            //points.Add(new MapPoint(onMapLocation.X, onMapLocation.Y + num, onMapLocation.Z + num, onMapLocation.SpatialReference));
            //points.Add(new MapPoint(onMapLocation.X + num, onMapLocation.Y + num, onMapLocation.Z + num, onMapLocation.SpatialReference));
            //points.Add(new MapPoint(onMapLocation.X + num, onMapLocation.Y, onMapLocation.Z + num, onMapLocation.SpatialReference));
            //points.Add(new MapPoint(onMapLocation.X, onMapLocation.Y, onMapLocation.Z + num, onMapLocation.SpatialReference));

            //var blueSymbol = new SimpleFillSymbol() { Color = System.Drawing.Color.Pink };

            //Esri.ArcGISRuntime.Geometry.Polygon polygon = new Esri.ArcGISRuntime.Geometry.Polygon(points);
            //// Create the graphic from the geometry and the symbol.
            //Graphic item = new Graphic(polygon);

            //// Add the graphic to the overlay.
            //graphicOverlay.Graphics.Add(item);

            //MySceneView.PreviewMouseLeftButtonDown -= MySceneViewOnDrawByCenter;

        }
        #endregion

        #region 判断关系
        private void CheckOBBCollision_Click(object sender, RoutedEventArgs e)
        {
            //Test();
            //Test2();
            //Test3();


            if (selectGraphic == null || selectFeatureGeoElement == null)
            {
                MessageBox.Show("请选择一个shp数据和一个绘制数据!");
                return;
            }
            //IGeometry2 geo = selectFeatureGeoElement as IGeometry2;

            //ISpatialReferenceFactory spatialReferenceFactory = new SpatialReferenceEnvironmentClass();
            //ISpatialReference spatialReference =spatialReferenceFactory.CreateESRISpatialReferenceFromPRJ(selectFeatureGeoElement.Geometry.SpatialReference.WkText);

            //geo.Project(spatialReference);


            //创建shp几何体
            Esri.ArcGISRuntime.Geometry.Polygon selectFeatureGeometryRealCube = GetSelectFeatureGeometryRealCube();
            

            //创建绘画几何体
            Esri.ArcGISRuntime.Geometry.Polygon selectGraphicGeometryRealCube = GetSelectGraphicGeometryRealCube();

            var b = GeometryEngine.Intersects(selectGraphicGeometryRealCube, selectFeatureGeometryRealCube);
            //var g3 = GeometryEngine.Intersection(selectFeatureGeometryRealCube, selectGraphicGeometryRealCube);
            var g2 = GeometryEngine.Intersections(selectGraphicGeometryRealCube, selectFeatureGeometryRealCube);

            if (b)
            {
                foreach (var g in g2)
                {
                    var redSymbol = new SimpleFillSymbol() { Color = System.Drawing.Color.Red, Style = SimpleFillSymbolStyle.Solid };
                    Graphic item = new Graphic(g, redSymbol);
                    intersectionOverlay.Graphics.Add(item);
                }
                //var redSymbol = new SimpleFillSymbol() { Color = System.Drawing.Color.Red, Style = SimpleFillSymbolStyle.Solid };
                //Graphic item = new Graphic(g3, redSymbol);
                //intersectionOverlay.Graphics.Add(item);

                MessageBox.Show("二者重叠");
            }
            else
            {
                MessageBox.Show("二者不重叠");
            }
            //var g = g3 as Esri.ArcGISRuntime.Geometry.Polygon;
            //foreach (var part in g.Parts)
            //{
            //    foreach (var point in part.Points)
            //    {

            //    }
            //}
        }

        private Esri.ArcGISRuntime.Geometry.Polygon GetSelectGraphicGeometryRealCube()
        {
            Esri.ArcGISRuntime.Geometry.Polygon selectGraphicGeometryRealCube = null;
            if (selectGraphic.Geometry is MapPoint)
            {
                
                var symbol = selectGraphic.Symbol as SimpleMarkerSceneSymbol;
                var z = symbol.Height;
                var kuan = symbol.Width;
                var chang = symbol.Depth;
                var heading = symbol.Heading;
                var selectGraphicGeometryMapPoint = selectGraphic.Geometry as MapPoint;

                var rightMapPoint = GeometryEngine.Project(selectGraphicGeometryMapPoint, selectFeatureGeoElement.Geometry.SpatialReference) as MapPoint;

                List<MapPoint> points = new List<MapPoint>();
                //todo 角度 or 采用四点+设置高程绘图
                //todo 可以在绘图的时候就把坐标系换了
                //获得四个角点的数据
                points.Add(new MapPoint(rightMapPoint.X - 0.5 * kuan, rightMapPoint.Y - 0.5 * chang, z, rightMapPoint.SpatialReference));
                points.Add(new MapPoint(rightMapPoint.X - 0.5 * kuan, rightMapPoint.Y + 0.5 * chang, z, rightMapPoint.SpatialReference));
                points.Add(new MapPoint(rightMapPoint.X + 0.5 * kuan, rightMapPoint.Y - 0.5 * chang, z, rightMapPoint.SpatialReference));
                points.Add(new MapPoint(rightMapPoint.X + 0.5 * kuan, rightMapPoint.Y + 0.5 * chang, z, rightMapPoint.SpatialReference));

                selectGraphicGeometryRealCube = new Esri.ArcGISRuntime.Geometry.Polygon(points, rightMapPoint.SpatialReference);
                
            }
            else if(selectGraphic.Geometry is Esri.ArcGISRuntime.Geometry.Polygon)
            {
                var polygon = selectGraphic.Geometry as Esri.ArcGISRuntime.Geometry.Polygon;
                //转坐标系
                selectGraphicGeometryRealCube= GeometryEngine.Project(polygon, selectFeatureGeoElement.Geometry.SpatialReference) as Esri.ArcGISRuntime.Geometry.Polygon;
            }
            return selectGraphicGeometryRealCube;
        }

        private Esri.ArcGISRuntime.Geometry.Polygon GetSelectFeatureGeometryRealCube()
        {
            Esri.ArcGISRuntime.Geometry.Polygon selectFeatureGeometryRealCube = null;
            var feature = selectFeatureGeoElement as Feature;
            var selectFeatureGeometryPolygon = selectFeatureGeoElement.Geometry as Esri.ArcGISRuntime.Geometry.Polygon;
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
            //var g1 = GeometryEngine.Difference(polygon1, polygon2);

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

    }
}
