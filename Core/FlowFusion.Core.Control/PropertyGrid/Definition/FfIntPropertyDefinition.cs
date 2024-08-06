using DevExpress.Xpf.Editors.Settings;

namespace FarnahadFlowFusion.Core.Control.PropertyGrid.Definition;

public class FfIntPropertyDefinition : FfPropertyDefinition
{
    public FfIntPropertyDefinition()
    {
        EditSettings = new SpinEditSettings
        {
            IsFloatValue = false,
            Mask = "N2"
        };
    }
}