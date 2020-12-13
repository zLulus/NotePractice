using Microsoft.WindowsAPICodePack.Dialogs;
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

namespace WpfDemo.SelectFolder
{
    /// <summary>
    /// Interaction logic for SelectFolderDemo.xaml
    /// </summary>
    public partial class SelectFolderDemo : UserControl
    {
        public SelectFolderDemo()
        {
            InitializeComponent();
        }

        private void SelectFolrderByFolderBrowserDialog_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            selectFolrderPathTextBlock.Text = dialog.SelectedPath.Trim();
        }

        private void SelectFolrderByCommonOpenFileDialog_Click(object sender, RoutedEventArgs e)
        {
            //引用WindowsAPICodePack
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                selectFolrderPathTextBlock.Text = dialog.FileName;
            }
        }
    }
}
