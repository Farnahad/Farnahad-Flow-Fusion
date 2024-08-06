using DevExpress.Xpf.Editors.Settings;

namespace FlowFusion.Core.Control.PropertyGrid.Definition;

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