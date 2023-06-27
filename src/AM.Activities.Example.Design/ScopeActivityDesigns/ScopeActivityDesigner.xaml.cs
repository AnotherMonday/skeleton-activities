using System.Activities.Presentation;
using System.Windows;
using AM.Activities.Example.ScopeExample;
using AM.ComposerActivitiesBridge.Attributes;

namespace AM.Activities.Example.Design.ScopeActivityDesigns
{
    /// <summary>
    ///     Scope activity logic.
    /// </summary>
    [DeveloperDesigner(
        typeof(ScopeActivity))] // Indicates that this Design will be used for the ScopeActivity in the composer
    public partial class ScopeActivityDesigner
    {
        private bool _onlyOnFirstLoad;

        public ScopeActivityDesigner()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Set the Flowchart to the expand state.
        /// </summary>
        /// <param name="e">
        ///     <see cref="RoutedEventArgs" />
        /// </param>
        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);

            if (_onlyOnFirstLoad) return;
            if (WorkflowItemPresenter.Item?.View is ActivityDesigner flowchart) flowchart.ExpandState = true;

            _onlyOnFirstLoad = true;
        }
    }
}