using SuperMap.Data;
using SuperMap.UI;
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

namespace SuperMapWpfDemo
{
    /// <summary>
    /// Interaction logic for LoadMapWindow.xaml
    /// </summary>
    public partial class LoadMapWindow : Window
    {
        private Workspace showMapWorkspace;
        private MapControl showMapControl;

        public LoadMapWindow()
        {
            InitializeComponent();
        }

        private void LoadMap_Click(object sender, RoutedEventArgs e)
        {
            showMapWorkspace = new Workspace();
			//SampleData文件夹的内容即超图SampleData文件夹中的内容，在此不上传到仓库，需要自己下载
            showMapWorkspace.Open(new WorkspaceConnectionInfo(@"..\..\..\SampleData\World\World.smwu"));

            showMapControl = new MapControl();

            showMapControl.Action = SuperMap.UI.Action.Pan;
            //必须设置
            showMapControl.Map.Workspace = showMapWorkspace;
            showMapControl.Map.Open(showMapWorkspace.Maps[0]);

            //赋值给前端控件hostMapControl
            hostMapControl.Child = showMapControl;
        }
    }
}
