using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using static UIFramework.Utility.DarkSky.Manifest;

namespace SwainStrain.Target.PostableCommands
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

            string sourceDll =
                @"C:\Users\letiz\source\repos\BIMTemplate\BIMTemplate\bin\2025Debug\net8.0-windows\BIMTemplate.dll";

            string shadowDir =
                @"C:\RevitDev\Shadow";

            Directory.CreateDirectory(shadowDir);

            string shadowDll = Path.Combine(
                shadowDir,
                $"MyPlugin_{DateTime.Now:yyyyMMdd_HHmmssfff}.dll");

            File.Copy(sourceDll, shadowDll, true);

            Assembly asm = Assembly.LoadFrom(shadowDll);

            //Type entryType = asm.GetTypes()
            //    .First(t =>
            //        typeof(IHotReloadEntry)
            //        .IsAssignableFrom(t));

            //var entry = (IHotReloadEntry)
            //    Activator.CreateInstance(entryType);

            //entry.Execute(commandData.Application);


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
