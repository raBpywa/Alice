using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace AliceSRV
{
    static class MouseHook
    {
        [DllImport("User32.dll")]
        static extern void mouse_event(MouseFlags dwFlags, int dx, int dy, int dwData, UIntPtr dwExtraInfo);

        //для удобства использования создаем перечисление с необходимыми флагами (константами), которые определяют действия мыши: 
        [Flags]
        enum MouseFlags
        {
            Move = 0x0001, LeftDown = 0x0002, LeftUp = 0x0004, RightDown = 0x0008,
            RightUp = 0x0010, Absolute = 0x8000
        };





        public static void Mouse_Move(int x,int y)
        {
            //и использование - клик правой примерно в центре экрана
           

            mouse_event(MouseFlags.Absolute | MouseFlags.Move, x*50, y*100, 0, UIntPtr.Zero);
           // mouse_event(MouseFlags.Absolute | MouseFlags.RightDown, x, y, 0, UIntPtr.Zero);
           // mouse_event(MouseFlags.Absolute | MouseFlags.RightUp, x, y, 0, UIntPtr.Zero);
        }
        public static void Mouse_DOWN(int x, int y)
        {
         mouse_event(MouseFlags.Absolute | MouseFlags.LeftDown, x, y, 0, UIntPtr.Zero);
        }
        public static void Mouse_UP(int x, int y)
        {
          mouse_event(MouseFlags.Absolute | MouseFlags.LeftUp, x, y, 0, UIntPtr.Zero);
        }
    }
}