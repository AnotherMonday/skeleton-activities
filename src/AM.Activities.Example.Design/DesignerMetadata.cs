using System.Activities.Presentation.Metadata;
using System.Reflection;

using AM.Activities.Common.Design.Metadata;

namespace AM.Activities.Example.Design
{
    /// <summary>
    ///     Types implementing IRegisterMetadata can be used to add custom metadata to activities at runtime.
    /// </summary>
    public class DesignerMetadata : IRegisterMetadata
    {
        public void Register()
        {
            AttributeTableBuilder builder = new AttributeTableBuilder();
            Assembly activities = Assembly.GetAssembly(typeof(ExampleCodeActivity));
            BaseDesignerMetadata.LoadMetadata(builder, activities);
            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
    }
}