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
using WpfDemo.OpenFiles.Commands;
using System.IO;
using System.Runtime.InteropServices;

namespace WpfDemo.OpenFiles
{
    /// <summary>
    /// Interaction logic for OpenTxtFileWindow.xaml
    /// </summary>
    public partial class OpenTxtFileWindow : UserControl
    {
        private OpenFileCommand openFileCommand;
        #region 文件占用
        [DllImport("kernel32.dll")]
        public static extern IntPtr _lopen(string lpPathName, int iReadWrite);
        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);

        public const int OF_READWRITE = 2;
        public const int OF_SHARE_DENY_NONE = 0x40;
        public readonly IntPtr HFILE_ERROR = new IntPtr(-1);
        #endregion
        public OpenTxtFileWindow()
        {
            InitializeComponent();

            openFileCommand= new OpenFileCommand();
            openFileCommand.OnOpened += OpenCommand_OnOpened;
            openTxtFileButton.Command = openFileCommand;
        }

        private void OpenCommand_OnOpened(object parameter)
        {
            //引入System.Windows.Forms.dll
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "txt文件|*.txt";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if (!File.Exists(ofd.FileName))
                    {
                        MessageBox.Show("文件不存在!");
                        return;
                    }

                    IntPtr vHandle = _lopen(ofd.FileName, OF_READWRITE | OF_SHARE_DENY_NONE);

                    if (vHandle == HFILE_ERROR)
                    {
                        MessageBox.Show("文件被占用！");
                        CloseHandle(vHandle);
                        return;
                    }

                    CloseHandle(vHandle);

                    var filePath = ofd.FileName;
                    using (FileStream stream = File.OpenRead(filePath))
                    {
                        TextRange documentTextRange = new TextRange(loadTxtRichTextBox.Document.ContentStart, loadTxtRichTextBox.Document.ContentEnd);
                        string dataFormat = DataFormats.Text;
                        StreamReader sr = new StreamReader(stream, Encoding.Default);
                        documentTextRange.Load(new MemoryStream(Encoding.UTF8.GetBytes(sr.ReadToEnd())), dataFormat);

                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("出现异常", $"{ex.Message}");
                    return;
                }

                
            }
        }
    }
}
