using System.Activities;
using System.Activities.Presentation.PropertyEditing;
using System.ComponentModel;
using System.Windows.Forms;
using AM.Activities.Common.BaseActivities;
using AM.Activities.Common.Design.Editors;
using AM.ComposerActivitiesBridge.Attributes;

namespace AM.Activities.Example
{
    public sealed class ExampleCodeActivity : AbstractCodeActivity
    {
        /// <summary>
        ///     Attributes can be set directly above the properties as shown here.
        /// </summary>
        [Category("Input")] // Specifies the name of the category in which to group the property.
        [DisplayName("Input Text*")] // Specifies the display name for this property.
        [RequiredArgument] // Specifies that this property needs to be set 
        [VariablePopup] // For this property, we show a variable dialog.
        [Description("This text will be shown in the output")] // Specifies a description for a property
        public InArgument<string> Text { get; set; }

        /// <summary>
        ///     InArguments can be extended with a custom editor to add more functionality. In this case it will show a FileDialog
        /// </summary>
        [Editor(typeof(FileBrowserDialogEditor<OpenFileDialog>), typeof(DialogPropertyValueEditor))]
        public InArgument<string> CustomEditor { get; set; }

        /// <summary>
        ///     Enums will be displayed as DropdownBox in the options panel
        /// </summary>
        public ExamplePropertyOptions Options { get; set; }

        /// <summary>
        ///     Attributes can also be set loaded though xml files, see AM.Activities.Example_metadata.xml for examples.
        /// </summary>
        public OutArgument<string> Output { get; set; }

        /// <summary>
        ///     If your activity returns a value, derive from <see cref="AbstractCodeActivity{T}" />
        ///     and return the value from the Execute method.
        /// </summary>
        /// <param name="context"></param>
        protected override void ExecuteActivity(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text property
            string text = context.GetValue(Text);

            // Obtain the runtime value of the CustomEditor property
            string customEditor = context.GetValue(CustomEditor);

            IExampleApplication exampleApplication = new ExampleApplication();
            string result = exampleApplication.SetText(text);

            exampleApplication.Option = Options;
            exampleApplication.ExampleEditor = customEditor;

            // Assigns the value to the Output property
            context.SetValue(Output, result);
        }
    }
}