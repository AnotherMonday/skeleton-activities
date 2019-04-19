﻿using AM.Core.ActivityDesignBase;

namespace AM.Skeleton.Activities.Design
{
    /// <summary>
    ///     The Attributes declare that this Design should be used for the Analyst or the Developer view.
    ///     These Attributes can also be declared independently to have independent designs for each view
    /// </summary>
    [AnalystDesigner(typeof(AsyncActivityDesign))]
    [DeveloperDesigner(typeof(AsyncActivityDesign))]
    public partial class AsyncActivityDesign
    {
        public AsyncActivityDesign()
        {
            InitializeComponent();
        }
    }
}