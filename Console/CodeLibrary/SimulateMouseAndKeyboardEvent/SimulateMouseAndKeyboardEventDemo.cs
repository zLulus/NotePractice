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
        public static void Run()
        {
            KeyBoardMonitor.sendKey(Keys.NumPad1);
            //MouseMonitor.move(200, 200);
            //Thread.Sleep(1000);
            //MouseMonitor.absMove(0, 0);
        }
    }
}
