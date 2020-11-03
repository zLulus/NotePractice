using ArcGIS3D.WpfDemo.Dialogs;
using ArcGIS3D.WpfDemo.Enums;
using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Rasters;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI;
using Esri.ArcGISRuntime.UI.Controls;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
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
        TapTypeEnum tapTypeEnum { get; set; }
        /// <summary>
        /// 绘画图层
        /// </summary>
        GraphicsOverlay graphicOverlay { get; set; }
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

        Esri.ArcGISRuntime.Geometry.Geometry selectFeatureGeometry { get; set; }
        Esri.ArcGISRuntime.Geometry.Geometry selectGraphicGeometry { get; set; }

        public CubeCollision()
        {
            InitializeComponent();


            InitializeMap();
        }

        private async void InitializeMap()
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
                var spF = featureLayer.SpatialReference;
                var spFG = featureLayer.SpatialReference.BaseGeographic;

                #region 绘制高程
                // Create a new simple line symbol for the feature layer
                SimpleLineSymbol mySimpleLineSymbol = new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, System.Drawing.Color.Black, 1);

                // Create a new simple fill symbol for the feature layer 
                SimpleFillSymbol mysimpleFillSymbol = new SimpleFillSymbol(SimpleFillSymbolStyle.Solid, System.Drawing.Color.WhiteSmoke, mySimpleLineSymbol);

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

                // Add the feature layer to the map
                MySceneView.Scene = new Scene(BasemapType.DarkGrayCanvasVector);
                MySceneView.Scene.Basemap = new Basemap(featureLayer);
                var bSp = MySceneView.Scene.Basemap.BaseLayers[0].SpatialReference;
                var sp = MySceneView.SpatialReference;
                // Zoom the map to the extent of the shapefile
                await MySceneView.SetViewpointAsync(new Viewpoint(myShapefile.Extent));
                //await MyMapView.SetViewpointGeometryAsync(newFeatureLayer.FullExtent, 50);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error");
            }

            //tiff
            try
            {
                //////https://developers.arcgis.com/net/latest/wpf/guide/add-image-overlays.htm
                ////////// Create an Envelope for displaying the image frame in the correct location
                //var sp = new SpatialReference(4523);
                //Envelope pacificSouthwestEnvelope = new Envelope(35563999.499069, 3547066.496987, 35564412.860201, 3547500.100019, sp);

                //////// Create a RuntimeImage to display using a local png file
                ////RuntimeImage image = new RuntimeImage(new System.Uri("file:///D:/Project/ChangZhen/3547.00-564.00.tif"));
                ////RuntimeImage image = new RuntimeImage(new System.Uri("file:///D:/Project/ChangZhen/3547.00-564.00.png"));

                //////// Create an ImageFrame with a local image file and the extent envelope  
                //ImageFrame imageFrame = new ImageFrame(new System.Uri(TifFilePath), pacificSouthwestEnvelope);
                ////ImageFrame imageFrame = new ImageFrame(image, pacificSouthwestEnvelope);

                //////// Add the ImageFrame to an ImageOverlay and set it to be 50% transparent
                //ImageOverlay imageOverlay = new ImageOverlay(imageFrame);
                //imageOverlay.Opacity = 0.5;

                ////// Add the ImageOverlay to the scene view's ImageOverlay collection
                //MySceneView.ImageOverlays.Add(imageOverlay);
                /////todo 没有加载出来
                //imageFrame.LoadAsync().Wait();

                //// Load the raster file
                //Raster myRasterFile = new Raster("file:///D:/Project/ChangZhen/3547.00-564.00.tif");

                //// Create the layer
                //RasterLayer myRasterLayer = new RasterLayer(myRasterFile);

                //MySceneView.Overlays.Items.Add(myRasterLayer);

                //myRasterLayer.LoadAsync().Wait();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }

            //todo 加载高程
            //https://developers.arcgis.com/net/10-2/store/guide/add-layers-to-your-scene.htm
            //// query for all graphics in the feature service, include z-coordinate values
            //var queryTask = new QueryTask(new Uri("file:///D:/Project/场镇/场镇.shp"));
            //var query = new Query("1=1");
            //query.ReturnZ = true;
            //var result = await queryTask.ExecuteAsync(query);

            //// create a graphics layer to show the graphics using absolute placement using the z values
            //var graphicsLayer = new GraphicsLayer();
            //graphicsLayer.SceneProperties.SurfacePlacement = SurfacePlacement.Absolute;

            //// build graphics from the results, symbolize with a red line
            //var lineSym = new SimpleLineSymbol { Color = Colors.Red, Style = SimpleLineStyle.Solid, Width = 5 };
            //graphicsLayer.GraphicsSource = from f in result.FeatureSet.Features select new Graphic(f.Geometry, lineSym);

            //// add the layer to the scene
            //MySceneView.Scene.Layers.Add(graphicsLayer);

            //绘画图层
            // Create the graphics overlay.
            graphicOverlay = new GraphicsOverlay();
            //缩小放大
            graphicOverlay.ScaleSymbols = true;

            // Set the surface placement mode for the overlay.
            graphicOverlay.SceneProperties.SurfacePlacement = SurfacePlacement.Absolute;
            MySceneView.GraphicsOverlays.Add(graphicOverlay);
        }

        private void MySceneViewOnGeoViewTapped(object sender, Esri.ArcGISRuntime.UI.Controls.GeoViewInputEventArgs e)
        {
            if (tapTypeEnum == TapTypeEnum.DrawByCenter)
            {

            }
            else if (tapTypeEnum == TapTypeEnum.Select)
            {

            }
        }
        #region 切换模式

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
            graphicOverlay.Graphics.Clear();
        }

        #region 选择
        private async void MySceneViewOnSelect(object sender, Esri.ArcGISRuntime.UI.Controls.GeoViewInputEventArgs e)
        {
            //shp图层
            await SetSelectForFeatureLayer(e);

            //绘制图层
            await SetSelectForGraphicsOverlay(e);
        }

        private async Task SetSelectForFeatureLayer(GeoViewInputEventArgs e)
        {
            var result = await MySceneView.IdentifyLayerAsync(featureLayer, e.Position, 1, false);
            GeoElement geoElement = result.GeoElements.FirstOrDefault();
            if (geoElement != null)
            {
                selectFeatureGeometry = geoElement.Geometry;

                #region 根据范围查询
                // Define the selection tolerance.
                //double tolerance = 15;

                //// Convert the tolerance to map units.
                ////double mapTolerance = tolerance * MySceneView.UnitsPerPixel;
                ////todo 单位
                //double mapTolerance = tolerance * 0.000001;

                //// Get the tapped point.
                //MapPoint geometry = e.Location;

                //// Normalize the geometry if wrap-around is enabled.
                ////    This is necessary because of how wrapped-around map coordinates are handled by Runtime.
                ////    Without this step, querying may fail because wrapped-around coordinates are out of bounds.
                ////if (MyMapView.IsWrapAroundEnabled)
                ////{
                ////    geometry = (MapPoint)GeometryEngine.NormalizeCentralMeridian(geometry);
                ////}

                //// Define the envelope around the tap location for selecting features.
                //Envelope selectionEnvelope = new Envelope(geometry.X - mapTolerance, geometry.Y - mapTolerance, geometry.X + mapTolerance,
                //    geometry.Y + mapTolerance, MySceneView.Scene.SpatialReference);

                //// Define the query parameters for selecting features.
                //QueryParameters queryParams = new QueryParameters
                //{
                //    // Set the geometry to selection envelope for selection by geometry.
                //    Geometry = selectionEnvelope
                //};
                #endregion
                QueryParameters queryParams = new QueryParameters
                {
                    // Set the geometry to selection envelope for selection by geometry.
                    Geometry = selectFeatureGeometry//selectionEnvelope
                };
                // Select the features based on query parameters defined above.
                await featureLayer.SelectFeaturesAsync(queryParams, Esri.ArcGISRuntime.Mapping.SelectionMode.New);
            }
        }

        private async Task SetSelectForGraphicsOverlay(Esri.ArcGISRuntime.UI.Controls.GeoViewInputEventArgs e)
        {
            IdentifyGraphicsOverlayResult result = null;

            try
            {
                // Identify the tapped graphics
                result = await MySceneView.IdentifyGraphicsOverlayAsync(graphicOverlay, e.Position, 1, false);
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
            graphicOverlay.ClearSelection();
            identifiedGraphic.IsSelected = true;

            // Get the selected graphic's geometry
            selectGraphicGeometry = identifiedGraphic.Geometry;
        }

        #endregion

        #region 绘制-中心点
        private async void MySceneViewOnDrawByCenter(object sender, MouseEventArgs mouseEventArgs)
        {
            // Get the mouse position.
            Point cursorSceenPoint = mouseEventArgs.GetPosition(MySceneView);

            // Get the corresponding MapPoint.
            MapPoint onMapLocation = MySceneView.ScreenToBaseSurface(cursorSceenPoint);
            //var p2=await MySceneView.ScreenToLocationAsync(cursorSceenPoint);
            //var p3= MySceneView.PointFromScreen(cursorSceenPoint);
            //var p4 = MySceneView.PointToScreen(cursorSceenPoint);
            //todo 转换坐标系，原坐标系单位是经纬度，要转成米
            //MapPointBuilder.CreateWithM()
            //onMapLocation.SpatialReference = new SpatialReference(@"");

            SetCubeInfo setCubeInfo = new SetCubeInfo(onMapLocation.X, onMapLocation.Y, onMapLocation.Z);
            var dialogResult = setCubeInfo.ShowDialog();
            if (dialogResult.HasValue && dialogResult.Value)
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
                graphicOverlay.Graphics.Add(item);

                MySceneView.PreviewMouseLeftButtonDown -= MySceneViewOnDrawByCenter;
            }

        }
        #endregion

        #region OBB
        private void CheckOBBCollision_Click(object sender, RoutedEventArgs e)
        {
            if (selectGraphicGeometry == null || selectFeatureGeometry == null)
            {
                MessageBox.Show("请选择一个shp数据和一个绘制数据!");
                return;
            }

            var g1 = GeometryEngine.Difference(selectGraphicGeometry, selectFeatureGeometry);

            //https://desktop.arcgis.com/zh-cn/arcmap/10.3/tools/3d-analyst-toolbox/intersect-3d-3d-analyst-.htm
        }

        #endregion
    }
}
