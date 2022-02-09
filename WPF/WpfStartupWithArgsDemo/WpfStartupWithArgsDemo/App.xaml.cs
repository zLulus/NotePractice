using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfStartupWithArgsDemo
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            ObservableCollection<string> data = new ObservableCollection<string>();
            for (int i = 0; i < e.Args.Length; i++)
            {
                data.Add(e.Args[i]);
                Console.WriteLine(e.Args[i]);
            }

            MainWindow window = new MainWindow(data);
            window.Show();
        }
    }
}
