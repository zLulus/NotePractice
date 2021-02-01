using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeLibrary.SimulateMouseAndKeyboardEvent
{
    public class SimulateMouseAndKeyboardEventDemo
    {
        //http://www.cnblogs.com/zengjfgit/p/5657068.html
        //1. C# 如何用按钮实现鼠标滚轮操作
        //    http://blog.csdn.net/jglie/article/details/6872333
        //2. c#  mouse_event 模拟鼠标点击事件 绝对位置
        //    http://blog.sina.com.cn/s/blog_71d894bd01013goa.html
        //3. C# Win32API 模拟鼠标移动及点击事件
        //    http://www.cnblogs.com/08shiyan/archive/2011/07/18/2109086.html
        //4. How to: Simulate Mouse and Keyboard Events in Code
        //    https://msdn.microsoft.com/en-us/library/ms171548.aspx
        //5. SendKeys Class
        //    https://msdn.microsoft.com/en-us/library/system.windows.forms.sendkeys.aspx
        //6. Virtual-Key Codes
        //    https://msdn.microsoft.com/zh-cn/library/dd375731(v=vs.85).aspx
        //7. C#中将字母/字符转换为键盘的key/键值/keycode
        //    http://www.crifan.com/convert_char_letter_to_key_keycode_in_csharp/
        //8. VkKeyScan function
        //    https://msdn.microsoft.com/en-us/library/ms646329(VS.85).aspx

        public static void Run()
        {
            KeyBoardMonitor.sendKey(Keys.NumPad1);
            //MouseMonitor.move(200, 200);
            //Thread.Sleep(1000);
            //MouseMonitor.absMove(0, 0);
        }
    }
}
