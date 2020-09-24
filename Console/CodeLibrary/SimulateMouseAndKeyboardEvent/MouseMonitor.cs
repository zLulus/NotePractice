using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CodeLibrary.SimulateMouseAndKeyboardEvent
{
    public class MouseMonitor
    {
        private const int MOUSEEVENTF_MOVE = 0x0001;   // 移动鼠标 
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;   // 模拟鼠标左键按下 
        private const int MOUSEEVENTF_LEFTUP = 0x0004;   // 模拟鼠标左键抬起 
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;   // 模拟鼠标右键按下 
        private const int MOUSEEVENTF_RIGHTUP = 0x0010;   // 模拟鼠标右键抬起 
        private const int MOUSEEVENTF_WHEEL = 0x0800;   // 模拟鼠标滚轮
        private const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;   // 模拟鼠标中键按下 
        private const int MOUSEEVENTF_MIDDLEUP = 0x0040;   // 模拟鼠标中键抬起 
        private const int MOUSEEVENTF_ABSOLUTE = 0x8000;   // 标示是否采用绝对坐标 

        [DllImport("user32")]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public static void move(int dx, int dy)
        {
            mouse_event(MOUSEEVENTF_MOVE, dx, dy, 0, 0);
        }

        public static void absMove(int x, int y)
        {
            mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, 500, 500, 0, 0);
        }

        public static void wheel(int roll)
        {
            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, roll, 0);
        }

        public static void leftSingle()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        public static void leftDouble()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        public static void right()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
        }

        public static void middle()
        {
            mouse_event(MOUSEEVENTF_MIDDLEUP | MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, 0);
        }
    }
}
