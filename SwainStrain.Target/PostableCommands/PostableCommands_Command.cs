using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace SwainStrain.Target.PostableCommands
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class PostableCommands_Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiApplication = commandData.Application;
            var document = uiApplication.ActiveUIDocument.Document;

            UIApplication uiApp = commandData.Application;
            UIDocument uiDoc = uiApp.ActiveUIDocument;

            if (uiDoc == null)
            {
                TaskDialog.Show("Error", "No active document.");
                return Result.Failed;
            }

            // Ask user what they want to do
            TaskDialog dialog = new TaskDialog("Postable Command Example")
            {
                MainInstruction = "Choose a Revit command to launch",
                MainContent = "The selected command will be executed after this command finishes.",
                AllowCancellation = true,
                CommonButtons = TaskDialogCommonButtons.Cancel
            };

            dialog.AddCommandLink(
                TaskDialogCommandLinkId.CommandLink1,
                "Option 1 – Start Wall command");

            dialog.AddCommandLink(
                TaskDialogCommandLinkId.CommandLink2,
                "Option 2 – Start Align command");

            dialog.AddCommandLink(
                TaskDialogCommandLinkId.CommandLink3,
                "Option 3 – Start Rotate command");

            TaskDialogResult result = dialog.Show();

            PostableCommand? commandToPost = null;

            switch (result)
            {
                case TaskDialogResult.CommandLink1:
                    commandToPost = PostableCommand.ArchitecturalWall;
                    break;

                case TaskDialogResult.CommandLink2:
                    commandToPost = PostableCommand.Align;
                    break;

                case TaskDialogResult.CommandLink3:
                    commandToPost = PostableCommand.Rotate;
                    break;

                case TaskDialogResult.Cancel:
                    return Result.Cancelled;
            }

            if (commandToPost.HasValue)
            {
                RevitCommandId commandId =
                    RevitCommandId.LookupPostableCommandId(commandToPost.Value);

                if (uiApp.CanPostCommand(commandId))
                {
                    uiApp.PostCommand(commandId);
                    return Result.Succeeded;
                }
                else
                {
                    TaskDialog.Show(
                        "Unavailable",
                        $"The command '{commandToPost}' cannot be posted in the current context.");
                    return Result.Failed;
                }
            }

            return Result.Succeeded;
        }
    }
    public class PostableCommands_Availability : IExternalCommandAvailability
    {
        public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
        {
            return true;
        }
    }
}
