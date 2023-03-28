using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        #region dsddd
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr FindWindow(string sClassName, string sAppName);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

        [DllImport("User32.Dll", EntryPoint = "PostMessageA")]
        static extern bool PostMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

        


        public enum WMessages : int
        {
            WM_LBUTTONDOWN = 0x201,
            WM_LBUTTONUP = 0x202,
            WM_KEYDOWN = 0x100,
            WM_KEYUP = 0x101,
            WH_KEYBOARD_LL = 13,
            WH_MOUSE_LL = 14,
        }
        #endregion

        private void CloseV()
        {
            var trayWnd = FindWindow("Shell_TrayWnd", null);
            var nullIntPtr = new IntPtr(0);

            if (trayWnd != nullIntPtr)
            {
                var trayNotifyWnd = FindWindowEx(trayWnd, nullIntPtr, "TrayNotifyWnd", null);
                if (trayNotifyWnd != nullIntPtr)
                {
                    var tIPBandWnd = FindWindowEx(trayNotifyWnd, nullIntPtr, "TIPBand", null);

                    if (tIPBandWnd != nullIntPtr)
                    {
                        PostMessage(tIPBandWnd, (UInt32)WMessages.WM_LBUTTONDOWN, 1, 65537);
                        PostMessage(tIPBandWnd, (UInt32)WMessages.WM_LBUTTONUP, 1, 65537);
                    }
                }
            }
        }

        private void Button1_OnClick(object sender, RoutedEventArgs e)
        {
            CloseV();
            //OnScreenKeyboard.Close();
        }

        private void Button2_OnClick(object sender, RoutedEventArgs e)
        {
            OnScreenKeyboard.Show();
        }

        private void TextBox1_OnLostFocus(object sender, RoutedEventArgs e)
        {
            OnScreenKeyboard.Hide();
        }

        private void TextBox1_OnGotFocus(object sender, RoutedEventArgs e)
        {
            OnScreenKeyboard.Show();
        }

        private void ButtonStartup_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => {
                while (true)
                {
                    try
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            OnScreenKeyboard.Show();
                            Thread.Sleep(10);
                        }
                        
                    }
                    catch (Exception ex)
                    {

                    }
                    //Thread.Sleep(1000);
                    OnScreenKeyboard.Hide();
                    //Thread.Sleep(1000);
                }
            });
        }
    }
}
