using DevExpress.Xpf.Editors.Settings;

namespace FarnahadFlowFusion.Core.Control.PropertyGrid.Definition;

public class FfDoublePropertyDefinition : FfPropertyDefinition
{
    public FfDoublePropertyDefinition()
    {
        EditSettings = new SpinEditSettings
        {
            IsFloatValue = true,
            Mask = "N2"
        };
    }
}