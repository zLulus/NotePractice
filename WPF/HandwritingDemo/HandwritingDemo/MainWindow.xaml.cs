using HandwritingDemo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Navigation;

namespace HandwritingDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public HandwritingRecognizeViewModel handwritingRecognizeViewModel { get; set; }
     

        public MainWindow()
        {
            handwritingRecognizeViewModel = new HandwritingRecognizeViewModel();
            DataContext = handwritingRecognizeViewModel;

            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            handwritingRecognizeViewModel.Strokes = inkCanvas.Strokes;
        }
    }
}
