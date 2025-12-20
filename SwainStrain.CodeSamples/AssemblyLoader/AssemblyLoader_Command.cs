using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using static UIFramework.Utility.DarkSky.Manifest;

namespace SwainStrain.CodeSamples.PostableCommands
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class Test_Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiApplication = commandData.Application;
            var document = uiApplication.ActiveUIDocument.Document;

            UIApplication uiApp = commandData.Application;
            UIDocument uiDoc = uiApp.ActiveUIDocument;

            string targetDir = @"C:\Users\letiz\source\repos\SwainStrain.CodeSamples\SwainStrain.Target\bin\2026Debug\net8.0-windows";

            string searchPattern = "SwainStrain.Target.*.dll";

            var files = Directory.GetFiles(targetDir, searchPattern);

            //string sourceDll =
            //    @"C:\Users\letiz\source\repos\SwainStrain.CodeSamples\SwainStrain.Target\bin\2026Debug\net8.0-windows\SwainStrain.Target.dll";

            string shadowDir =
                @"C:\RevitDev\Shadow";

            Directory.CreateDirectory(shadowDir);

            string shadowDll = Path.Combine(
                shadowDir,
                $"SwainStrain.Target_{DateTime.Now:yyyyMMdd_HHmmssfff}.dll");

            File.Copy(files[0], shadowDll, true);

            Assembly asm = Assembly.LoadFrom(shadowDll);

            Type appType = asm.GetTypes()
                .First(t =>
                    typeof(IExternalApplication)
                    .IsAssignableFrom(t));

            IExternalApplication app =
                (IExternalApplication)Activator.CreateInstance(appType);

            app.OnStartup(App.ThisApp.uIControlledApplication);

            return Result.Succeeded;
        }
    }
    public class Test_Availability : IExternalCommandAvailability
    {
        public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
        {
            return true;
        }
    }
}
