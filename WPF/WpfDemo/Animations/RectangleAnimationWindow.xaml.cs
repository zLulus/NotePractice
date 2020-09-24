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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfDemo.Animations
{
    /// <summary>
    /// RectangleAnimationWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RectangleAnimationWindow : UserControl
    {
        //https://docs.microsoft.com/zh-cn/dotnet/framework/wpf/graphics-multimedia/animation-overview?WT.mc_id=DT-MVP-5003010&WT.mc_id=DT-MVP-5003010
        //后端实现方法

        private Storyboard myStoryboard;

        public RectangleAnimationWindow()
        {
            InitializeComponent();

            //容器
            StackPanel myPanel = new StackPanel();
            myPanel.Margin = new Thickness(10);

            //方块
            Rectangle myRectangle = new Rectangle();
            myRectangle.Name = "myRectangle";
            this.RegisterName(myRectangle.Name, myRectangle);
            myRectangle.Width = 100;
            myRectangle.Height = 100;
            //蓝色填充
            myRectangle.Fill = Brushes.Orange;

            //动画
            DoubleAnimation myDoubleAnimation = new DoubleAnimation();
            //动画起始值和结束值
            myDoubleAnimation.From = 1.0;
            myDoubleAnimation.To = 0.0;
            //时间：5秒
            myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(5));
            //反序执行，为真
            myDoubleAnimation.AutoReverse = true;
            //重复方式，永久
            myDoubleAnimation.RepeatBehavior = RepeatBehavior.Forever;

            myStoryboard = new Storyboard();
            //添加动画到myStoryboard
            myStoryboard.Children.Add(myDoubleAnimation);
            //设置动画的目标对象
            Storyboard.SetTargetName(myDoubleAnimation, myRectangle.Name);
            //设置动画的目标属性是Rectangle.OpacityProperty（不透明度属性）
            Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(Rectangle.OpacityProperty));

            // Use the Loaded event to start the Storyboard.
            //使用Loaded方法启动Storyboard
            myRectangle.Loaded += new RoutedEventHandler(myRectangleLoaded);

            myPanel.Children.Add(myRectangle);
            //将Panel放置到窗口画面中
            this.Content = myPanel;
        }

        private void myRectangleLoaded(object sender, RoutedEventArgs e)
        {
            //开始Storyboard
            myStoryboard.Begin(this);
        }
    }
}
