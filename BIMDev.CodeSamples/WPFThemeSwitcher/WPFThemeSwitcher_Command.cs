using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using BIMDev.CodeSamples.Views;

namespace BIMDev.CodeSamples.WPFThemeSwitcher
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class WPFThemeSwitcher_Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiApplication = commandData.Application;
            var document = uiApplication.ActiveUIDocument.Document;

            var window = new WPFThemeSwitcher_View(uiApplication);
            window.Show();

            return Result.Succeeded;
        }
    }
    public class WPFThemeSwitcher_Availability : IExternalCommandAvailability
    {
        public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
        {
            return true;
        }
    }
}






