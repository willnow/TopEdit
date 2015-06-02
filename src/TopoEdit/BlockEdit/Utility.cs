using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TopoEdit
{
    internal class Utility
    {
        internal static Rectangle ConvertRect(RectangleF rectF)
        {
            return new Rectangle(ConvertPos(rectF.Location), ConvertSize(rectF.Size));
        }

        internal static Point ConvertPos(PointF pointF)
        {
            return new Point((int)Math.Round(pointF.X), (int)Math.Round(pointF.Y));
        }

        internal static Size ConvertSize(SizeF sizeF)
        {
            return new Size((int)Math.Round(sizeF.Width), (int)Math.Round(sizeF.Height));
        }

        internal static RectangleF ConvertRect(Rectangle rect)
        {
            return new RectangleF(ConvertPos(rect.Location), ConvertSize(rect.Size));
        }

        internal static PointF ConvertPos(Point point)
        {
            return new PointF((float)point.X, (float)point.Y);
        }

        internal static SizeF ConvertSize(Size size)
        {
            return new SizeF((float)size.Width, (float)size.Height);
        }

        internal static void ConvertValue(ref float value)
        {
            value = (int)Math.Round(value);
        }

        internal static PointF GetCenter(RectangleF rect)
        {
            return new PointF(rect.Left + rect.Width / 2.0F, rect.Top + rect.Height / 2.0F);
        }

        public static RectangleF Union(RectangleF cRectA, RectangleF cRectB)
        {
            if (cRectA.IsEmpty)
            {
                return cRectB;
            }
            if (cRectB.IsEmpty)
            {
                return cRectA;
            }
            return RectangleF.Union(cRectA, cRectB);
        }

        public static Rectangle AdjustRect(RectangleF rect, int adjustLen)
        {
            return AdjustRect(ConvertRect(rect), adjustLen);
        }

        public static Rectangle AdjustRect(Rectangle rect, int adjustLen)
        {
            Rectangle adjustRect = rect;

            adjustRect.X -= adjustLen;
            adjustRect.Y -= adjustLen;

            adjustRect.Width += 2 * adjustLen;
            adjustRect.Height += 2 * adjustLen;

            return adjustRect;
        }


        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "GetKeyState")]
        private static extern int GetKeyState(
        int nVirtKey // Long，欲测试的虚拟键键码。对字母、数字字符（A-Z、a-z、0-9），用它们实际的ASCII值  
        );

        public const int KEYEVENTF_KEYUP = 0x02;
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "keybd_event")]
        private static extern void keybd_event(
         byte bVk,       // Byte，欲模拟的虚拟键码  
         byte bScan,     // Byte，键的OEM扫描码  
         int dwFlags,    // Long，零；或设为下述两个标志之一 KEYEVENTF_EXTENDEDKEY 指出是一个扩展键，而且在前面冠以0xE0代码 KEYEVENTF_KEYUP 模拟松开一个键  
        int dwExtraInfo // Long，通常不用的一个值。api函数GetMessageExtraInfo可取得这个值。允许使用的值取决于特定的驱动程序  
        );

        public static bool GetKeyState(System.Windows.Forms.Keys keys)
        {
            return ((GetKeyState((int)keys) & 0x8000) != 0) ? true : false;
        }

        public static List<string> GetAllInstalledFont()
        {
            //获取系统已经安装的字体
            List<string> fonts = new List<string>();
            System.Drawing.Text.InstalledFontCollection font = new System.Drawing.Text.InstalledFontCollection();
            FontFamily[] fontFamilies = font.Families;

            for (int i = 0; i < fontFamilies.Length; i++)
            {
                fonts.Add(fontFamilies[i].Name);
            }
            return fonts;
        }
    }
}
