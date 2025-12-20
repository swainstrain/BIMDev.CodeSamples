using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace SwainStrain.Target.DockablePane
{
    /// <summary>
    /// Interaction logic for DockablePane_Page.xaml
    /// </summary>
    public partial class DockablePane_Page : Page, IDockablePaneProvider
    {
        public DockablePane_Page()
        {
            InitializeComponent();

            UITheme theme = UIThemeManager.CurrentTheme;

            var dictionary = Resources.MergedDictionaries.First();

            var source = theme == UITheme.Dark ?
                "pack://application:,,,/SwainStrain.Target;component/Resources/ResourceDictionary_Dark.xaml" :
                "pack://application:,,,/SwainStrain.Target;component/Resources/ResourceDictionary_Light.xaml";

            dictionary.Source = new Uri(source);
        }

        public void SetupDockablePane(DockablePaneProviderData data)
        {
            data.FrameworkElement = this as FrameworkElement;
            data.InitialState = new DockablePaneState
            {
                DockPosition = DockPosition.Right,
            };
        }
    }
}
