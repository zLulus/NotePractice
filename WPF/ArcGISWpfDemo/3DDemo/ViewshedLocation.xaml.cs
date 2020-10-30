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

        /// <summary>
        /// 移动观察者
        /// </summary>
        bool isMoveViewpoint;
        /// <summary>
        /// 绘制圆柱体
        /// </summary>
        bool isDraw;
        /// <summary>
        /// 绘制圆柱体-
        /// </summary>
        bool subscribedToDraw;

        public ViewshedLocation()
        {
            InitializeComponent();

            // Initialize the sample.
            Initialize();
        }

        private void Initialize()
        {
            isMoveViewpoint = false;
            isDraw = false;

            _viewHeight = HeightSlider.Value;

            // Create the scene with the imagery basemap.
            Scene myScene = new Scene(Basemap.CreateImagery());
            MySceneView.Scene = myScene;

            // Add the surface elevation.
            Surface mySurface = new Surface();
            mySurface.ElevationSources.Add(new ArcGISTiledElevationSource(_localElevationImageService));
            myScene.BaseSurface = mySurface;

            // Add the scene layer.
            ArcGISSceneLayer sceneLayer = new ArcGISSceneLayer(_buildingsUrl);
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
            if (isMoveViewpoint)
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
            else if(isDraw)
            {
                if (subscribedToDraw == false)
                {
                    points = new List<MapPoint>();
                    //预览
                    //MySceneView.MouseMove
                    //编辑中
                    MySceneView.PreviewMouseLeftButtonDown += MySceneViewOnMouseMoveAddPoint;
                    //完成
                    MySceneView.PreviewMouseRightButtonDown += MySceneViewOnMouseMoveWithDraw;
                }
                subscribedToDraw = true;
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
            // Get the mouse position.
            Point cursorSceenPoint = mouseEventArgs.GetPosition(MySceneView);

            // Get the corresponding MapPoint.
            MapPoint onMapLocation = MySceneView.ScreenToBaseSurface(cursorSceenPoint);
            Test(onMapLocation.X, onMapLocation.Y, onMapLocation.Z);
            //points.Add(onMapLocation);
        }
        private void MySceneViewOnMouseMoveWithDraw(object sender, MouseEventArgs mouseEventArgs)
        {
            // Get the mouse position.
            Point cursorSceenPoint = mouseEventArgs.GetPosition(MySceneView);

            //List<MapPoint> drawPoints = new List<MapPoint>();
            // Get the corresponding MapPoint.
            List<MapPoint> pointsWithZ = new List<MapPoint>();
            for(int i = points.Count - 1; i>=0; i--)
            {
                var point = points[i];
                pointsWithZ.Add(new MapPoint(point.X, point.Y, point.Z + 200, point.SpatialReference));
            }
            //foreach(var point in points)
            //{
            //    pointsWithZ.Add(new MapPoint(point.X, point.Y, point.Z+ 200,point.SpatialReference));
            //}
            points.AddRange(pointsWithZ);
            //drawPoints.AddRange(points);
            //drawPoints.AddRange(pointsWithZ);
            //drawPoints.Add(pointsWithZ[0]);
            //drawPoints.Add(points[points.Count - 1]);
            //drawPoints.Add(points[0]);
            MapPoint onMapLocation = MySceneView.ScreenToBaseSurface(cursorSceenPoint);

            Esri.ArcGISRuntime.Geometry.Polygon polygon = new Esri.ArcGISRuntime.Geometry.Polygon(points);

            SimpleFillSymbol simpleFillSymbol = new SimpleFillSymbol(SimpleFillSymbolStyle.Solid, System.Drawing.Color.Black, null);
            // Create the graphic from the geometry and the symbol.
            Graphic item = new Graphic(polygon, simpleFillSymbol);


            // Add the graphic to the overlay.
            overlay.Graphics.Add(item);

            MySceneView.PreviewMouseLeftButtonDown -= MySceneViewOnMouseMoveAddPoint;
            MySceneView.PreviewMouseRightButtonDown -= MySceneViewOnMouseMoveWithDraw;
        }
        #endregion

        #region 切换模式
        private void Draw_Click(object sender, RoutedEventArgs e)
        {
            isDraw = true;
            isMoveViewpoint = false;
            subscribedToDraw = false;
        }

        private void ChangeModeViewPointStatus_Click(object sender, RoutedEventArgs e)
        {
            isDraw = false;
            isMoveViewpoint = true;
        }

        private void ChangeModeStatus_Click(object sender, RoutedEventArgs e)
        {
            isDraw = false;
            isMoveViewpoint = false;
        }
        #endregion

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            overlay.Graphics.Clear();
        }

        private void DrawTest_Click(object sender, RoutedEventArgs e)
        {
            Test(0,0,0);
        }

        private void Test(double x ,double y,double z)
        {
            points = new List<MapPoint>();
            points.Add(new MapPoint(x, y, z));
            points.Add(new MapPoint(x, y + 100, z));
            points.Add(new MapPoint(x + 100, y, z));
            points.Add(new MapPoint(x + 100, y + 100, z));

            points.Add(new MapPoint(x, y, z + 200));
            points.Add(new MapPoint(x, y + 100, z + 200));
            points.Add(new MapPoint(x + 100, y, z + 200));
            points.Add(new MapPoint(x + 100, y + 100, z + 200));
            Esri.ArcGISRuntime.Geometry.Polygon polygon = new Esri.ArcGISRuntime.Geometry.Polygon(points);


            SimpleMarkerSceneSymbol symbol = SimpleMarkerSceneSymbol.CreateCube(System.Drawing.Color.LightPink, 100, SceneSymbolAnchorPosition.Center);
            //SimpleFillSymbol simpleFillSymbol = new SimpleFillSymbol(SimpleFillSymbolStyle.Solid, System.Drawing.Color.Black, null);
            // Create the graphic from the geometry and the symbol.
            Graphic item = new Graphic(new MapPoint(x, y, z), symbol);

            // Add the graphic to the overlay.
            overlay.Graphics.Add(item);
        }
    }
}
