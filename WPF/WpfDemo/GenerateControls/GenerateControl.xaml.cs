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

namespace WpfDemo.GenerateControls
{
    /// <summary>
    /// GenerateControl.xaml 的交互逻辑
    /// </summary>
    public partial class GenerateControl : UserControl
    {
        Point p;
        DateTime? lastPressTime = null;
        double X, Y;
        internal bool isDrag { get; set; }
        TranslateTransform translateTransform { get; set; }

        public GenerateControl()
        {
            InitializeComponent();

            isDrag = false;
            X = 0;
            Y = 0;
        }

        private void GenerateControl_Click(object sender, RoutedEventArgs e)
        {
            if (generateControlGrid.Children.Count != 0)
            {
                MessageBox.Show("请先清除控件");
                return;
            }
            TextBlock textBlock = new TextBlock();
            textBlock.Text = "Test";
            textBlock.Margin = new Thickness(p.X+ translateTransform.X, p.Y+ translateTransform.Y, 0, 0);
            //textBlock.SetValue(Canvas.LeftProperty, p.X);
            //textBlock.SetValue(Canvas.TopProperty, p.Y);
            generateControlGrid.Children.Add(textBlock);
        }

        private void GetLocation_Click(object sender, RoutedEventArgs e)
        {
            //generateControlGrid
            //ownUserControl
            // Get absolute location on screen of upper left corner of button

            Point locationFromScreen = this.locationTextBlock.PointToScreen(new Point(0, 0));

            // Transform screen point to WPF device independent point

            PresentationSource source = PresentationSource.FromVisual(generateControlGrid);

            System.Windows.Point targetPoints = source.CompositionTarget.TransformFromDevice.Transform(locationFromScreen);

            Point generateControlGridLocation = this.generateControlGrid.PointToScreen(new Point(0, 0));


            p = new Point(locationFromScreen.X - generateControlGridLocation.X, locationFromScreen.Y - generateControlGridLocation.Y);
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            generateControlGrid.Children.Clear();
        }

        
        private void locationTextBlock_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //increment z-Order and pass it to the current element, 
                //so that it stays on top of all other elements
                //递增z-Order并将其传递给当前元素，以使其停留在所有其他元素的顶部
                //border1.SetValue(Canvas.ZIndexProperty, 999);

                //区分长按拖动和仅仅点击控件
                if (lastPressTime != null && lastPressTime.Value.AddSeconds(1) >= DateTime.Now)
                {
                    //判断操作类型为拖拽移动
                    if (isDrag)
                    Drag(locationTextBlock);
                }
                lastPressTime = DateTime.Now;

            }
        }

        /// <summary>
        /// 拖拽移动
        /// </summary>
        /// <param name="sender"></param>
        private void Drag(object sender)
        {
            this.Cursor = Cursors.Hand;

            // Retrieve the current position of the mouse.
            //当前位置
            var newX = Mouse.GetPosition(locationTextBlock).X;
            var newY = Mouse.GetPosition(locationTextBlock).Y;
            var moveX = newX - X;
            var moveY = newY - Y;
            //xAndy.Content = $"{X},{Y}";
            //newXAndnewY.Content = $"{newX},{newY}";
            //moveXAndmoveY.Content = $"{moveX},{moveY}";

            MoveLocation(moveX, moveY);

            // Update the beginning position of the mouse
            //X = newX;
            //Y = newY;
        }

        private void MoveLocation(double moveX, double moveY)
        {
            // Reset the location of the object (add to sender's renderTransform newPosition minus currentElement's position
            var transformGroup = locationTextBlock.RenderTransform as TransformGroup;
            if (transformGroup == null)
                return;
            var translateTransforms = from transform in transformGroup.Children
                                      where transform.GetType().Name == nameof(TranslateTransform)
                                      select transform;

            translateTransform = translateTransforms.FirstOrDefault() as TranslateTransform;
            translateTransform.X += moveX;
            translateTransform.Y += moveY;
            Console.WriteLine($"{moveX},{moveY} {translateTransform.X},{translateTransform.Y}");
            //foreach (TranslateTransform tt in translateTransforms)
            //{
            //    tt.X += moveX;
            //    tt.Y += moveY;
            //    Console.WriteLine($"{moveX},{moveY} {tt.X},{tt.Y}");
            //}

            
        }

        private void locationTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isDrag = true;
        }

        private void locationTextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //isDrag = false;
        }

        private void locationTextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            var x = Mouse.GetPosition(locationTextBlock).X;
            var y = Mouse.GetPosition(locationTextBlock).Y;
            this.X = x;
            this.Y = y;
        }
    }
}
