using System;
using System.Linq;
using System.Windows;
using Autodesk.Revit.UI;

namespace BIMDev.CodeSamples.Views
{
    public partial class WPFThemeSwitcher_View : Window
    {
        public WPFThemeSwitcher_View(UIApplication uiApp)
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            UITheme theme = UIThemeManager.CurrentTheme;

            var dictionary = Resources.MergedDictionaries.First();

            var source = theme == UITheme.Dark ?
                "pack://application:,,,/Resources/ResourceDictionary_Dark.xaml" :
                "pack://application:,,,/Resources/ResourceDictionary_Light.xaml";

            dictionary.Source = new Uri(source);
        }
    }
}
