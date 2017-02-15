﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        public void OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Obtain the window handle for WPF application  
                IntPtr mainWindowPtr = new WindowInteropHelper(this).Handle;
                HwndSource mainWindowSrc = HwndSource.FromHwnd(mainWindowPtr);
                mainWindowSrc.CompositionTarget.BackgroundColor = Color.FromArgb(0, 0, 0, 0);

                // Get System Dpi  
                System.Drawing.Graphics desktop = System.Drawing.Graphics.FromHwnd(mainWindowPtr);
                float DesktopDpiX = desktop.DpiX;
                float DesktopDpiY = desktop.DpiY;

                // Set Margins  
                NonClientRegionAPI.MARGINS margins = new NonClientRegionAPI.MARGINS();

                // Extend glass frame into client area  
                // Note that the default desktop Dpi is 96dpi. The  margins are  
                // adjusted for the system Dpi.  
                margins.cxLeftWidth = Convert.ToInt32(5 * (DesktopDpiX / 96));
                margins.cxRightWidth = Convert.ToInt32(5 * (DesktopDpiX / 96));
                margins.cyTopHeight = Convert.ToInt32(((int)topBar.ActualHeight + 5) * (DesktopDpiX / 96));
                margins.cyBottomHeight = Convert.ToInt32(5 * (DesktopDpiX / 96));

                int hr = NonClientRegionAPI.DwmExtendFrameIntoClientArea(mainWindowSrc.Handle, ref margins);
                //  
                if (hr < 0)
                {
                    //DwmExtendFrameIntoClientArea Failed  
                }
            }
            // If not Vista, paint background white.  
            catch (DllNotFoundException)
            {
                Application.Current.MainWindow.Background = Brushes.White;
            }
        }
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
