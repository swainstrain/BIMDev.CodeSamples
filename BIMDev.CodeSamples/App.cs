using Autodesk.Revit.UI;
using BIMDev.CodeSamples.WPFThemeSwitcher;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace BIMDev.CodeSamples
{
    public class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            Assembly executingAssembly = Assembly.GetExecutingAssembly();            

            string assemblyPath = executingAssembly.Location;

            application.CreateRibbonTab("BIMDevCodeSamples");
            RibbonPanel ribbonPanel = application.CreateRibbonPanel("BIMDevCodeSamples", "BIMDevCodeSamples");

            this.AddPushButton(ribbonPanel, "WPFThemeSwitcher", "WPF Theme \nSwitcher", assemblyPath,
                typeof(WPFThemeSwitcher_Command).FullName,
                "WPF Theme Switcher",
                "BIMDev.CodeSamples.Resources.WPFThemeSwitcher_Icon.png", typeof(WPFThemeSwitcher_Availability).FullName);

            Assembly.LoadFrom(Path.Combine(
    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
    "MaterialDesignThemes.Wpf.dll"));
            Assembly.LoadFrom(Path.Combine(
    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
    "MaterialDesignColors.dll"));
            Assembly.LoadFrom(Path.Combine(
    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
    "Microsoft.Xaml.Behaviors.dll"));

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

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (args.Name.Contains("resources"))
                return null;

            try
            {
                Assembly executingAssembly = Assembly.GetExecutingAssembly();
                AssemblyName assemblyName = new AssemblyName(args.Name);

                string path = Path.Combine(Path.GetDirectoryName(executingAssembly.Location), assemblyName.Name + ".dll");

                if (!File.Exists(path))
                {
                    string path2 = assemblyName.Name + ".dll";
                    if (assemblyName.CultureInfo?.Equals(CultureInfo.InvariantCulture) == false)
                    {
                        path2 = String.Format(@"{0}\{1}", assemblyName.CultureInfo, path2);
                    }

                    using (Stream stream = executingAssembly.GetManifestResourceStream(path2))
                    {
                        if (stream == null)
                            return null;

                        byte[] assemblyRawBytes = new byte[stream.Length];
                        stream.Read(assemblyRawBytes, 0, assemblyRawBytes.Length);
                        return Assembly.Load(assemblyRawBytes);
                    }
                }

                var assembly = Assembly.LoadFrom(path);
                return assembly;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}