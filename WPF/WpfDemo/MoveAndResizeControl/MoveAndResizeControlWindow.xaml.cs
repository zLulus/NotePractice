using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WpfDemo.MoveAndResizeControl.Dialogs;
using WpfDemo.MoveAndResizeControl.Tools;

namespace WpfDemo.MoveAndResizeControl
{
    /// <summary>
    /// MoveAndResizeControlWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MoveAndResizeControlWindow : Window
    {
        private Element current = new Element();
        public MoveAndResizeControlWindow()
        {
            InitializeComponent();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.current.X = Mouse.GetPosition(this.canvas).X;
            this.current.Y = Mouse.GetPosition(this.canvas).Y;

            if (this.current.InputElement != null)
                this.current.InputElement.CaptureMouse();

            if (!this.current.IsStretching)
                this.current.IsDragging = true;
        }
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            if (this.current.InputElement != null)
                this.current.InputElement.ReleaseMouseCapture();

            this.Cursor = Cursors.Arrow;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed &&
                 current.InputElement != null)
            {
                //increment z-Order and pass it to the current element, 
                //so that it stays on top of all other elements
                //递增z-Order并将其传递给当前元素，以使其停留在所有其他元素的顶部
                ((Border)this.current.InputElement).SetValue(Canvas.ZIndexProperty, this.current.ZIndex++);

                //判断操作类型为拖拽移动
                if (this.current.IsDragging)
                    Drag(sender);

                //缩放大小
                if (this.current.IsStretching)
                    Stretch(sender);
            }
        }

        private void border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //capture the last highest z index before pointing to new current element
            //在指向新的当前元素之前捕获最后的最高z索引
            int newZIndex = (int)((Border)sender).GetValue(Canvas.ZIndexProperty);
            this.current.ZIndex = newZIndex > this.current.ZIndex ? newZIndex : this.current.ZIndex;

            //capture the new current element
            //捕获新的当前元素
            this.current.InputElement = (IInputElement)sender;
        }

        private void border_MouseLeave(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                return;

            // get coordinates
            //获得坐标
            //计算Border
            Border border = (Border)sender;
            var rightLimit = border.ActualWidth - border.Padding.Right;
            var bottomLimit = border.ActualHeight - border.Padding.Bottom;
            //获得鼠标点击(与特定元素的相对位置)
            var x = Mouse.GetPosition((IInputElement)sender).X;
            var y = Mouse.GetPosition((IInputElement)sender).Y;

            // figure out stretching directions - only to Right, Bottom 
            //找出缩放的方向-仅向右方、底部
            bool stretchRight = (x >= rightLimit && x < border.ActualWidth) ? true : false;
            bool stretchBottom = (y >= bottomLimit && y < border.ActualHeight) ? true : false;

            // update current element
            //更新当前元素
            this.current.InputElement = (IInputElement)sender;
            //记录鼠标当前位置
            this.current.X = x;
            this.current.Y = y;
            //默认为“缩放”操作
            this.current.IsStretching = true;

            //set cursor to show stretch direction 
            //设置光标显示
            //右&底部，光标显示右下箭头
            if (stretchRight && stretchBottom)
            {
                this.Cursor = Cursors.SizeNWSE;
                return;
            }
            //右
            else if (stretchRight && !stretchBottom)
            {
                this.Cursor = Cursors.SizeWE;
                return;
            }
            //底部
            else if (stretchBottom && !stretchRight)
            {
                this.Cursor = Cursors.SizeNS;
                return;
            }
            //非缩放操作，显示箭头光标，设置IsStretching=false
            else //no stretch
            {
                this.Cursor = Cursors.Arrow;
                this.current.IsStretching = false;
            }

            SetLabels(border,x,y);
        }

        /// <summary>
        /// 修复无边框Border缩放大小的问题(在全范围内检查x,y的变化)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void border_MouseLeave_WithoutBorder(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                return;
            Console.WriteLine("border_MouseLeave_WithoutBorder");
            if (this.current.InputElement != null && this.current.InputElement is Border)
            {
                Console.WriteLine("border_MouseLeave_WithoutBorder Enter");
                // get coordinates
                //获得坐标
                //计算Border
                Border border = (Border)this.current.InputElement;
                var rightLimit = border.ActualWidth - border.Padding.Right;
                var bottomLimit = border.ActualHeight - border.Padding.Bottom;
                //获得鼠标点击
                var x = Mouse.GetPosition((IInputElement)border).X;
                var y = Mouse.GetPosition((IInputElement)border).Y;

                // figure out stretching directions - only to Right, Bottom 
                //找出缩放的方向-仅向右方、底部
                bool stretchRight = (x >= rightLimit && x < border.ActualWidth) ? true : false;
                bool stretchBottom = (y >= bottomLimit && y < border.ActualHeight) ? true : false;

                // update current element
                //更新当前元素
                //this.current.InputElement = (IInputElement)sender;
                //this.current.X = x;
                //this.current.Y = y;
                //默认为“缩放”操作
                this.current.IsStretching = true;

                //set cursor to show stretch direction 
                //设置光标显示
                //右&底部，光标显示右下箭头
                if (stretchRight && stretchBottom)
                {
                    this.Cursor = Cursors.SizeNWSE;
                    return;
                }
                //右
                else if (stretchRight && !stretchBottom)
                {
                    this.Cursor = Cursors.SizeWE;
                    return;
                }
                //底部
                else if (stretchBottom && !stretchRight)
                {
                    this.Cursor = Cursors.SizeNS;
                    return;
                }
                //非缩放操作，显示箭头光标，设置IsStretching=false
                else //no stretch
                {
                    this.Cursor = Cursors.Arrow;
                    this.current.IsStretching = false;
                }

                SetLabels(border, x, y);
            }
            
        }

        /// <summary>
        /// 显示拖拽移动、缩放大小的计算
        /// </summary>
        /// <param name="border"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void SetLabels(Border border,double x,double y)
        {
            actualWidthTextBlock.Text = $"ActualWidth:{border.ActualWidth}";
            actualHeightTextBlock.Text = $"ActualHeight:{border.ActualHeight}";
            paddingRightTextBlock.Text= $"Padding.Right:{border.Padding.Right}";
            paddingBottomTextBlock.Text = $"Padding.Bottom:{border.Padding.Bottom}";
            xTextBlock.Text = $"x:{x}";
            yTextBlock.Text = $"y:{y}";
            var rightLimit = border.ActualWidth - border.Padding.Right;
            rightLimitBlock.Text = $"rightLimit:{rightLimit}";
            var bottomLimit = border.ActualHeight - border.Padding.Bottom;
            bottomLimitBlock.Text = $"bottomLimit:{bottomLimit}";
            stretchRightBlock.Text = $"stretchRight    x >= rightLimit:{x >= rightLimit},x < border.ActualWidth:{x < border.ActualWidth}";
            stretchBottomBlock.Text = $"stretchBottom    y >= bottomLimit:{y >= bottomLimit},y < border.ActualHeight:{y < border.ActualHeight}";
        }

        private void border_MouseEnter(object sender, MouseEventArgs e)
        {
            Border border = (Border)sender;

            var rightLimit = border.ActualWidth - border.Padding.Right;
            var bottomLimit = border.ActualHeight - border.Padding.Bottom;

            var x = Mouse.GetPosition((IInputElement)sender).X;
            var y = Mouse.GetPosition((IInputElement)sender).Y;

            if (x < rightLimit && y < bottomLimit)
                this.Cursor = Cursors.Arrow;

            SetLabels(border, x, y);
        }

        /// <summary>
        /// 拖拽移动
        /// </summary>
        /// <param name="sender"></param>
        private void Drag(object sender)
        {
            this.Cursor = Cursors.Hand;

            // Retrieve the current position of the mouse.
            var newX = Mouse.GetPosition((IInputElement)sender).X;
            var newY = Mouse.GetPosition((IInputElement)sender).Y;

            // Reset the location of the object (add to sender's renderTransform newPosition minus currentElement's position
            var transformGroup = ((UIElement)this.current.InputElement).RenderTransform as TransformGroup;
            if (transformGroup == null)
                return;

            var translateTransforms = from transform in transformGroup.Children
                                      where transform.GetType().Name == "TranslateTransform"
                                      select transform;

            foreach (TranslateTransform tt in translateTransforms)
            {
                tt.X += newX - current.X;
                tt.Y += newY - current.Y;
            }

            // Update the beginning position of the mouse
            //更新鼠标的位置
            current.X = newX;
            current.Y = newY;
        }

        /// <summary>
        /// 缩放大小
        /// </summary>
        /// <param name="sender"></param>
        private void Stretch(object sender)
        {

            // Retrieve the current position of the mouse.
            var mousePosX = Mouse.GetPosition((IInputElement)sender).X;
            var mousePosY = Mouse.GetPosition((IInputElement)sender).Y;


            //get coordinates
            Border border = (Border)this.current.InputElement;
            var xDiff = mousePosX - this.current.X;
            var yDiff = mousePosY - this.current.Y;
            var width = ((Border)this.current.InputElement).Width;
            var heigth = ((Border)this.current.InputElement).Height;


            //make sure not to resize to negative width or heigth
            xDiff = (border.Width + xDiff) > border.MinWidth ? xDiff : border.MinWidth;
            yDiff = (border.Height + yDiff) > border.MinHeight ? yDiff : border.MinHeight;


            // stretchRight && stretchBottom ?
            if (this.Cursor == Cursors.SizeNWSE)
            {
                ((Border)this.current.InputElement).Width += xDiff;
                ((Border)this.current.InputElement).Height += yDiff;
            }
            // stretchRight ? 
            else if (this.Cursor == Cursors.SizeWE)
                ((Border)this.current.InputElement).Width += xDiff;

            // stretchBottom ?
            else if (this.Cursor == Cursors.SizeNS)
                ((Border)this.current.InputElement).Height += yDiff;

            //no stretch
            else
            {
                this.Cursor = Cursors.Arrow;
                this.current.IsStretching = false;
            }

            // update current coordinates with the latest postion of the mouse
            this.current.X = mousePosX;
            this.current.Y = mousePosY;
        }

        /// <summary>
        /// 打开弹窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModifyTextClick(object sender, RoutedEventArgs e)
        {
            ModifyTextDialog dialog = new ModifyTextDialog(myTextBlock.Text);
            var r= dialog.ShowDialog();
            if (r.HasValue && r.Value==true)
            {
                myTextBlock.Text = dialog.myTextBox.Text;
            }
        }

        /// <summary>
        /// 删除控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            MenuItem mnu = sender as MenuItem;
            var border = ((ContextMenu)mnu.Parent).PlacementTarget as Border;
            canvas.Children.Remove(border);
        }
    }
}
