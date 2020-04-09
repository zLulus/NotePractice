using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.UI.Controls;
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
using System.Windows.Shapes;

namespace ArcGISNewWpfDemo.Data
{
    /// <summary>
    /// AddFeatures.xaml 的交互逻辑
    /// </summary>
    public partial class AddFeatures : Window
    {
        // URL to the feature service.
        private const string FeatureServiceUrl = "https://sampleserver6.arcgisonline.com/arcgis/rest/services/DamageAssessment/FeatureServer/0";

        // Hold a reference to the feature table.
        private ServiceFeatureTable _damageFeatureTable;

        public AddFeatures()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            // Create the map with streets basemap.
            MyMapView.Map = new Map(Basemap.CreateStreets());

            // Create the feature table, referring to the Damage Assessment feature service.
            _damageFeatureTable = new ServiceFeatureTable(new Uri(FeatureServiceUrl));

            // Create a feature layer to visualize the features in the table.
            FeatureLayer damageLayer = new FeatureLayer(_damageFeatureTable);

            // Add the layer to the map.
            MyMapView.Map.OperationalLayers.Add(damageLayer);

            // Listen for user taps on the map - this will select the feature.
            MyMapView.GeoViewTapped += MapView_Tapped;

            // Zoom to the United States.
            MyMapView.SetViewpointCenterAsync(new MapPoint(-10800000, 4500000, SpatialReferences.WebMercator), 3e7);
        }
        private async void MapView_Tapped(object sender, GeoViewInputEventArgs e)
        {
            try
            {
                // Create the feature.
                ArcGISFeature feature = (ArcGISFeature)_damageFeatureTable.CreateFeature();

                // Get the normalized geometry for the tapped location and use it as the feature's geometry.
                MapPoint tappedPoint = (MapPoint)GeometryEngine.NormalizeCentralMeridian(e.Location);
                feature.Geometry = tappedPoint;

                // Set feature attributes.
                feature.SetAttributeValue("typdamage", "Minor");
                feature.SetAttributeValue("primcause", "Earthquake");

                // Add the feature to the table.
                await _damageFeatureTable.AddFeatureAsync(feature);

                // Apply the edits to the service.
                await _damageFeatureTable.ApplyEditsAsync();

                // Update the feature to get the updated objectid - a temporary ID is used before the feature is added.
                feature.Refresh();

                // Confirm feature addition.
                MessageBox.Show("Created feature " + feature.Attributes["objectid"], "Success!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error adding feature");
            }
        }
    }
}
