using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBox = System.Windows.Forms.MessageBox;

namespace WpfDemo.FTP
{
    /// <summary>
    /// FTPWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FTPWindow : System.Windows.Controls.UserControl
    {
        #region 文件占用
        [DllImport("kernel32.dll")]
        public static extern IntPtr _lopen(string lpPathName, int iReadWrite);
        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);

        public const int OF_READWRITE = 2;
        public const int OF_SHARE_DENY_NONE = 0x40;
        public readonly IntPtr HFILE_ERROR = new IntPtr(-1);
        #endregion
        string host { get { return FTPHostTextBox.Text.Trim(); } }
        string userName { get { return UserNameTextBox.Text.Trim(); } }
        string password { get { return PasswordTextBox.Text.Trim(); } }

        public FTPWindow()
        {
            InitializeComponent();
        }

        private void SelectUploadFile_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "文件|*.*";
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
                        SelectUploadFileTextBox.Text = filePath;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("出现异常", $"{ex.Message}");
                    return;
                }


            }
        }

        private void StartUpload_Click(object sender, RoutedEventArgs e)
        {
            FTPTool tool = new FTPTool(host,userName,password);
            tool.Upload(UploadFileNameTextBox.Text, SelectUploadFileTextBox.Text);
        }

        private void SelectDownloadFilePath_Click(object sender, RoutedEventArgs e)
        {
            //https://blog.csdn.net/zhumingyan/article/details/51105132
            FolderBrowserDialog m_Dialog = new FolderBrowserDialog();
            DialogResult result = m_Dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                SelectDownloadFilePathTextBox.Text= m_Dialog.SelectedPath.Trim();
            }
        }

        private void StartDownload_Click(object sender, RoutedEventArgs e)
        {
            FTPTool tool = new FTPTool(host, userName, password);
            tool.Download(DownloadFTPFileNameTextBox.Text, $"{SelectDownloadFilePathTextBox.Text}//{DownloadFileNameTextBox.Text}");
        }

        private void DeleteFTPFile_Click(object sender, RoutedEventArgs e)
        {
            FTPTool tool = new FTPTool(host, userName, password);
            tool.Delete(DeleteFTPFilePathTextBox.Text);
        }

        private void CreateDirectory_Click(object sender, RoutedEventArgs e)
        {
            FTPTool tool = new FTPTool(host, userName, password);
            tool.CreateDirectory(CreateDirectoryNameTextBox.Text);
        }

        private void DeleteDirectory_Click(object sender, RoutedEventArgs e)
        {
            FTPTool tool = new FTPTool(host, userName, password);
            tool.DeleteDirectory(DeleteDirectoryNameTextBox.Text);
        }
    }
}
