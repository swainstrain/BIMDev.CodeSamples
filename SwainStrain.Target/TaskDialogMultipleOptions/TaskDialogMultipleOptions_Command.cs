using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using SwainStrain.Target.WPFThemeSwitcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwainStrain.Target.TaskDialogMultipleOptions
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class TaskDialogMultipleOptions_Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiApplication = commandData.Application;
            var document = uiApplication.ActiveUIDocument.Document;

            TaskDialog taskDialog = new TaskDialog("SwainStrain Task Dialog")
            {
                // basic text
                Title = "SwainStrain Task Dialog", // dialog window title
                MainInstruction = "This is the main instruction.",  // big primary instruction
                MainContent = "This is the main content.", // smaller descriptive text

                // dialog identification
                Id = "SwainStrain.SampleDialog", // custom dialog id
                TitleAutoPrefix = false, // don't prefix title with add-in name


                FooterText = "Footer text goes here.",// footer area text                
                ExpandedContent = "More details here.", // extra text area
                VerificationText = "Verification text shows here", //a checkbox the user can toggle
                //ExtraCheckBoxText = "Check this box if you agree", //ExtraCheckBoxText cannot be used together with VerificationText

                // buttons
                CommonButtons = TaskDialogCommonButtons.Yes | TaskDialogCommonButtons.No,
                DefaultButton = TaskDialogResult.Yes,

                // other features
                AllowCancellation = true,
                EnableMarqueeProgressBar = true
            };

            taskDialog.AddCommandLink(TaskDialogCommandLinkId.CommandLink1, "Option 1", "Option 1 supporting content");
            taskDialog.AddCommandLink(TaskDialogCommandLinkId.CommandLink2, "Option 2", "Option 2 supporting content");
            taskDialog.AddCommandLink(TaskDialogCommandLinkId.CommandLink3, "Option 3", "Option 3 supporting content");
            taskDialog.AddCommandLink(TaskDialogCommandLinkId.CommandLink4, "Option 4", "Option 4 supporting content");

            TaskDialogResult result = taskDialog.Show();

            bool wasVerified = taskDialog.WasVerificationChecked();
            //bool wasExtraChecked = taskDialog.WasExtraCheckBoxChecked();

            string userAction = string.Empty;

            switch (result)
            {
                case TaskDialogResult.Cancel:
                    userAction = "User cancelled the dialog.";
                    break;
                case TaskDialogResult.CommandLink1:
                    userAction = "User selected Option 1.";
                    break;
                case TaskDialogResult.CommandLink2:
                    userAction = "User selected Option 2.";
                    break;
                case TaskDialogResult.CommandLink3:
                    userAction = "User selected Option 3.";
                    break;
                case TaskDialogResult.CommandLink4:
                    userAction = "User selected Option 4.";
                    break;
                case TaskDialogResult.Yes:
                    userAction = "User clicked Yes.";
                    break;
                case TaskDialogResult.No:
                    userAction = "User clicked No.";
                    break;
                default:
                    userAction = $"User selected: {result}.";
                    break;
            }

            // Example: display outcome
            TaskDialog.Show(
                "Dialog Result",
                $"{userAction}\n\n" +
                $"Verification checked: {wasVerified}"
            //$"Extra checkbox checked: {wasExtraChecked}"
            );


            return Result.Succeeded;
        }
    }
    public class TaskDialogMultipleOptions_Availability : IExternalCommandAvailability
    {
        public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
        {
            return true;
        }
    }
}
