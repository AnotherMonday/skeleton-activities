using AM.Core.ActivityDesignBase;
using AM.Skeleton.Activities.ErrorMessageExample;

namespace AM.Skeleton.Activities.Design.ErrorMessageExample
{
    [AnalystDesigner(
        typeof(WarningMessageActivity))] // Indicates that this Design will be used for the Analyst view in the composer
    public partial class WarningMessageAnalystDesigner
    {
        public WarningMessageAnalystDesigner()
        {
            InitializeComponent();
        }
    }
}