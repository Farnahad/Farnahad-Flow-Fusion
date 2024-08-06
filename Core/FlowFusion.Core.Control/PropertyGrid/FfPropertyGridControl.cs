using DevExpress.Xpf.PropertyGrid;

namespace FlowFusion.Core.Control.PropertyGrid;

public class FfPropertyGridControl : PropertyGridControl
{
    public FfPropertyGridControl()
    {
        ShowProperties = ShowPropertiesMode.WithPropertyDefinitions;
        SortMode = PropertyGridSortMode.Definitions;
        ShowCategories = CategoriesShowMode.Hidden;
    }
}