using Autodesk.Revit.UI;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace BIMDev.CodeSamples.WPFThemeSwitcher
{
    public partial class WPFThemeSwitcher_View : Window
    {
        public WPFThemeSwitcher_View(UIApplication uiApp)
        {
            InitializeComponent();

            var helper = new WindowInteropHelper(this);
            helper.Owner = Process.GetCurrentProcess().MainWindowHandle;

            // Get the currently active UI theme (Dark or Light)
            UITheme theme = UIThemeManager.CurrentTheme;

            // Get the first merged resource dictionary (the one being swapped)
            var dictionary = Resources.MergedDictionaries.First();

            // Load the appropriate theme resource dictionary based on the active theme
            var source = theme == UITheme.Dark ?
                "pack://application:,,,/BIMDev.CodeSamples;component/Resources/ResourceDictionary_Dark.xaml" :
                "pack://application:,,,/BIMDev.CodeSamples;component/Resources/ResourceDictionary_Light.xaml";

            // Apply the resource dictionary to the window
            dictionary.Source = new Uri(source);

            // Obtain the window handle for Win32 interop operations
            IntPtr hWnd = new WindowInteropHelper(this).EnsureHandle();

            // Title bar background color for dark/light mode (hex values)
            var backGround = theme == UITheme.Dark ? 0x212121 : 0xF5F5F5;

            // Value must be passed as an int array for DwmSetWindowAttribute
            int[] colorstr = new int[] { backGround };

            // Set the window caption/title bar color using DWM API
            DwmSetWindowAttribute(hWnd, DWWMA_CAPTION_COLOR, colorstr, 4);
        }

        // P/Invoke declaration for modifying DWM window attributes
        [DllImport("DwmApi")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        // Attribute ID for caption/title bar color
        const int DWWMA_CAPTION_COLOR = 35;
    }
}
