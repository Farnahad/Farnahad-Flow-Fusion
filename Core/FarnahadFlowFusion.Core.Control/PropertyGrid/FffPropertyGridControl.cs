using DevExpress.Xpf.PropertyGrid;

namespace FarnahadFlowFusion.Core.Control.PropertyGrid;

public class FffPropertyGridControl : PropertyGridControl
{
    public FffPropertyGridControl()
    {
        ShowProperties = ShowPropertiesMode.WithPropertyDefinitions;
        SortMode = PropertyGridSortMode.Definitions;
        ShowCategories = CategoriesShowMode.Hidden;
    }
}