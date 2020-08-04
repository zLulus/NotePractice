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
using WpfDemo.MoveAndResizeControl.Tools;

namespace WpfDemo.MoveAndResizeControl
{
    /// <summary>
    /// MoveAndResizeControlWindow2.xaml 的交互逻辑
    /// </summary>
    public partial class MoveAndResizeControlWindow2 : UserControl
    {
        //http://denismorozov.blogspot.com/2008/01/how-to-resize-wpf-controls-at-runtime.html

        private Element current = new Element();
        public MoveAndResizeControlWindow2()
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
                ((Border)this.current.InputElement).SetValue(Canvas.ZIndexProperty, this.current.ZIndex++);

                if (this.current.IsDragging)
                    Drag(sender);

                if (this.current.IsStretching)
                    Stretch(sender);
            }
        }

        private void border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //capture the last highest z index before pointing to new current element
            int newZIndex = (int)((Border)sender).GetValue(Canvas.ZIndexProperty);
            this.current.ZIndex = newZIndex > this.current.ZIndex ? newZIndex : this.current.ZIndex;

            //capture the new current element
            this.current.InputElement = (IInputElement)sender;
        }
        private void border_MouseLeave(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                return;

            // get coordinates
            Border border = (Border)sender;
            var rightLimit = border.ActualWidth - border.Padding.Right;
            var bottomLimit = border.ActualHeight - border.Padding.Bottom;
            var x = Mouse.GetPosition((IInputElement)sender).X;
            var y = Mouse.GetPosition((IInputElement)sender).Y;

            // figure out stretching directions - only to Right, Bottom 
            bool stretchRight = (x >= rightLimit && x < border.ActualWidth) ? true : false;
            bool stretchBottom = (y >= bottomLimit && y < border.ActualHeight) ? true : false;

            // update current element
            this.current.InputElement = (IInputElement)sender;
            this.current.X = x;
            this.current.Y = y;
            this.current.IsStretching = true;

            //set cursor to show stretch direction 
            if (stretchRight && stretchBottom)
            {
                this.Cursor = Cursors.SizeNWSE;
                return;
            }
            else if (stretchRight && !stretchBottom)
            {
                this.Cursor = Cursors.SizeWE;
                return;
            }
            else if (stretchBottom && !stretchRight)
            {
                this.Cursor = Cursors.SizeNS;
                return;
            }
            else //no stretch
            {
                this.Cursor = Cursors.Arrow;
                this.current.IsStretching = false;
            }
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
        }

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
            current.X = newX;
            current.Y = newY;
        }
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
    }


}
