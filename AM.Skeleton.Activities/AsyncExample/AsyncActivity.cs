using System.Activities;
using System.Activities.Presentation.PropertyEditing;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AM.Common.Activities.BaseActivities;
using AM.Common.Activities.Design.Editors;
using AM.Core.ActivityDesignBase.Attributes;

namespace AM.Skeleton.Activities.AsyncExample
{
    /// <summary>
    ///     Activity that runs a Task asynchronously
    /// </summary>
    public class AsyncActivity : AbstractTaskAsyncCodeActivity
    {
        /// <summary>
        ///     Input Argument of the type sting to a file that will be processed asynchronously
        /// </summary>
        [Category("Input")] // Set the category name in the property panel for this Argument
        [VariableSelectionInputTextPopup] // For this attribute we use variable pop-up box for text 
        [Editor(typeof(FileBrowserDialogEditor),
            typeof(DialogPropertyValueEditor))] // Custom editor to show a file path control next to the expression textbox in the property panel
        public InArgument<string> FilePath { get; set; }

        /// <summary>
        ///     Asynchronous function that will process a file
        /// </summary>
        /// <param name="file">Path to the file that will be processed</param>
        private static async void HandleFileAsync(string file)
        {
            int count = 0;

            // Read the specified file asynchronously.
            using (StreamReader reader = new StreamReader(file))
            {
                string v = await reader.ReadToEndAsync();

                //Process the file data somehow.
                count += v.Length;

                // A slow-running computation.
                // Dummy code.
                for (int i = 0; i < 10000; i++)
                {
                    int x = v.GetHashCode();
                    if (x == 0) count--;
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Represents an asynchronous operation.</returns>
        protected override Task ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken)
        {
            string file = FilePath.Get(context);

            return Task.Factory.StartNew(() => HandleFileAsync(file), cancellationToken);
        }
    }
}