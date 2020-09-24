using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfDemo.PrintTable.Dtos;
using WpfDemo.PrintTable.Templates;

namespace WpfDemo.PrintTable
{
    /// <summary>
    /// PrintTableWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PrintTableWindow : Window
    {
        private delegate void DoPrintMethod(PrintDialog pdlg, DocumentPaginator paginator);
        private delegate void EnableButtonMethod();
        private Timer m_timerToEnableButton;

        private void DoPrint(PrintDialog pdlg, DocumentPaginator paginator)
        {
            pdlg.PrintDocument(paginator, "Order Document");
        }

        private void EnableButton()
        {
            btnPrintDirect.IsEnabled = true;
        }
        public PrintTableWindow()
        {
            InitializeComponent();
        }

        private void btnPrintPreview_Click(object sender, RoutedEventArgs e)
        {
            PrintPreviewWindow previewWnd = new PrintPreviewWindow("OrderDocument.xaml", TestData.m_orderExample, new OrderDocumentRenderer());
            previewWnd.Owner = this;
            previewWnd.ShowInTaskbar = false;
            previewWnd.ShowDialog();
        }

        private void btnPrintDlg_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog pdlg = new PrintDialog();
            if (pdlg.ShowDialog() == true)
            {
                FlowDocument doc = PrintPreviewWindow.LoadDocumentAndRender("OrderDocument.xaml", TestData.m_orderExample, new OrderDocumentRenderer());
                Dispatcher.BeginInvoke(new DoPrintMethod(DoPrint), DispatcherPriority.ApplicationIdle, pdlg, ((IDocumentPaginatorSource)doc).DocumentPaginator);
            }
        }

        private void btnPrintDirect_Click(object sender, RoutedEventArgs e)
        {
            btnPrintDirect.IsEnabled = false;
            PrintDialog pdlg = new PrintDialog();
            FlowDocument doc = PrintPreviewWindow.LoadDocumentAndRender("OrderDocument.xaml", TestData.m_orderExample, new OrderDocumentRenderer());
            Dispatcher.BeginInvoke(new DoPrintMethod(DoPrint), DispatcherPriority.ApplicationIdle, pdlg, ((IDocumentPaginatorSource)doc).DocumentPaginator);
            m_timerToEnableButton = new Timer(TestTimerCallback, null, 3000, Timeout.Infinite);
        }

        public void TestTimerCallback(Object state)
        {
            m_timerToEnableButton.Dispose();
            Dispatcher.BeginInvoke(new EnableButtonMethod(EnableButton));
        }
    }
}
