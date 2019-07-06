using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyWindowsService.Client
{
    public partial class ServiceManageForm : Form
    {
        //需要引用MyWindowsService项目
        string serviceFilePath = $"{Application.StartupPath}\\MyWindowsService.exe";
        //这里是设置的serviceName，不是项目名称或者生成的exe的名称
        string serviceName = "TimingService";
        public ServiceManageForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 安装服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InstallButton_Click(object sender, EventArgs e)
        {
            if (this.IsServiceExisted(serviceName))
                this.UninstallService(serviceName);
            this.InstallService(serviceFilePath);
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartButton_Click(object sender, EventArgs e)
        {
            if (this.IsServiceExisted(serviceName))
                this.ServiceStart(serviceName);
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopButton_Click(object sender, EventArgs e)
        {
            if (this.IsServiceExisted(serviceName)) this.ServiceStop(serviceName);
        }

        /// <summary>
        /// 卸载服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UninstallButton_Click(object sender, EventArgs e)
        {
            if (this.IsServiceExisted(serviceName))
            {
                this.ServiceStop(serviceName);
                this.UninstallService(serviceFilePath);
            }
        }

        //判断服务是否存在
        private bool IsServiceExisted(string serviceName)
        {
            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController sc in services)
            {
                if (sc.ServiceName.ToLower() == serviceName.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        //安装服务
        private void InstallService(string serviceFilePath)
        {
            using (AssemblyInstaller installer = new AssemblyInstaller())
            {
                installer.UseNewContext = true;
                installer.Path = serviceFilePath;
                IDictionary savedState = new Hashtable();
                installer.Install(savedState);
                installer.Commit(savedState);
            }
        }

        //卸载服务
        private void UninstallService(string serviceFilePath)
        {
            using (AssemblyInstaller installer = new AssemblyInstaller())
            {
                installer.UseNewContext = true;
                installer.Path = serviceFilePath;
                installer.Uninstall(null);
            }
        }
        //启动服务
        private void ServiceStart(string serviceName)
        {
            using (ServiceController control = new ServiceController(serviceName))
            {
                if (control.Status == ServiceControllerStatus.Stopped)
                {
                    control.Start();
                }
            }
        }

        //停止服务
        private void ServiceStop(string serviceName)
        {
            using (ServiceController control = new ServiceController(serviceName))
            {
                if (control.Status == ServiceControllerStatus.Running)
                {
                    control.Stop();
                }
            }
        }
    }
}
