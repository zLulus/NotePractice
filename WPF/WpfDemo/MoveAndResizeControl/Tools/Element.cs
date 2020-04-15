using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfDemo.MoveAndResizeControl.Tools
{
    public class Element
    {
        #region Fields
        private bool isDragging = false;
        private bool isStretching = false;
        private bool stretchLeft = false;
        private bool stretchRight = false;
        private IInputElement inputElement = null;
        private double x, y = 0;
        private int zIndex = 0;
        #endregion

        #region Constructor
        public Element() { }
        #endregion

        #region Properties
        public IInputElement InputElement
        {
            get { return this.inputElement; }
            set
            {
                this.inputElement = value;
                this.isDragging = false;
                this.isStretching = false;
            }
        }
        public double X
        {
            get { return this.x; }
            set { this.x = value; }
        }
        public double Y
        {
            get { return this.y; }
            set { this.y = value; }
        }
        public int ZIndex
        {
            get { return this.zIndex; }
            set { this.zIndex = value; }
        }
        public bool IsDragging
        {
            get { return this.isDragging; }
            set
            {
                this.isDragging = value;
                this.isStretching = !this.isDragging;
            }
        }
        public bool IsStretching
        {
            get { return this.isStretching; }
            set
            {
                this.isStretching = value;
                this.IsDragging = !this.isStretching;
            }
        }
        public bool StretchLeft
        {
            get { return this.stretchLeft; }
            set { this.stretchLeft = value; this.stretchRight = !this.stretchLeft; }
        }
        public bool StretchRight
        {
            get { return this.stretchRight; }
            set { this.stretchRight = value; this.stretchLeft = !this.stretchRight; }
        }
        #endregion
    }
}
