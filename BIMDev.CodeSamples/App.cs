using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;
using Autodesk.Revit.UI;
using BIMDev.CodeSamples.WPFThemeSwitcher;

namespace BIMDev.CodeSamples
{
    public class App : IExternalApplication
    {

        public Result OnStartup(UIControlledApplication application)
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();

            string assemblyPath = executingAssembly.Location;

            application.CreateRibbonTab("BIMDevCodeSamples");
            RibbonPanel ribbonPanel = application.CreateRibbonPanel("BIMDevCodeSamples", "BIMDevCodeSamples");

            this.AddPushButton(ribbonPanel, "WPFThemeSwitcher", "WPF Theme \nSwitcher", assemblyPath,
                typeof(WPFThemeSwitcher_Command).FullName,
                "WPF Theme Switcher",
                "BIMDev.CodeSamples.Resources.WPFThemeSwitcher_Icon.png", typeof(WPFThemeSwitcher_Availability).FullName);

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        private void AddPushButton(RibbonPanel panel, string name, string text, string assemblyName, string className, string ToolTip, string imagePath, string availabilityClassName)
        {
            PushButtonData button = new PushButtonData(name, text, assemblyName, className);
            button.ToolTip = ToolTip;
            BitmapSource bitmap4 = GetEmbeddedImage(imagePath);
            button.Image = bitmap4;
            button.LargeImage = bitmap4;
            button.AvailabilityClassName = availabilityClassName;
            panel.AddItem(button);
        }

        public BitmapSource GetEmbeddedImage(string name)
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                Stream stream = assembly.GetManifestResourceStream(name);
                return BitmapFrame.Create(stream);
            }
            catch
            {
                return null;
            }
        }
    }
}