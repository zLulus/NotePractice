using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.UI;
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
    /// Interaction logic for CubeCollision.xaml
    /// </summary>
    public partial class CubeCollision : UserControl
    {
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
                string filepath = GetShapefilePath();
                // Open the shapefile
                ShapefileFeatureTable myShapefile = await ShapefileFeatureTable.OpenAsync(filepath);

                // Create a feature layer to display the shapefile
                FeatureLayer newFeatureLayer = new FeatureLayer(myShapefile);

                // Add the feature layer to the map
                MySceneView.Scene = new Scene(BasemapType.DarkGrayCanvasVector);
                MySceneView.Scene.Basemap = new Basemap(newFeatureLayer);
                //MyMapView.Map.OperationalLayers.Add(newFeatureLayer);

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
                //https://developers.arcgis.com/net/latest/wpf/guide/add-image-overlays.htm
                //// Create an Envelope for displaying the image frame in the correct location
                //Envelope pacificSouthwestEnvelope = new Envelope(563999.999999999, 3546999.99999979, 564500, 3547499.99999979, SpatialReferences.Wgs84);

                //// Create a RuntimeImage to display using a local png file
                ////RuntimeImage image = new RuntimeImage(new System.Uri("file:///D:/Project/场镇/3547.00-564.00.tif"));

                //// Create an ImageFrame with a local image file and the extent envelope  
                //ImageFrame imageFrame = new ImageFrame(new System.Uri("file:///D:/Project/场镇/3547.00-564.00.tif"), pacificSouthwestEnvelope);

                //// Add the ImageFrame to an ImageOverlay and set it to be 50% transparent
                //ImageOverlay imageOverlay = new ImageOverlay(imageFrame);
                //imageOverlay.Opacity = 0.8;

                //// Add the ImageOverlay to the scene view's ImageOverlay collection
                //MyMapView.ImageOverlays.Add(imageOverlay);

                //// Set the viewpoint with the ImageFrame's extent
                //await MyMapView.SetViewpointAsync(new Viewpoint(imageFrame.Extent));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private static string GetShapefilePath()
        {
            return @"D:\Project\场镇\场镇.shp";
        }
    }
}
