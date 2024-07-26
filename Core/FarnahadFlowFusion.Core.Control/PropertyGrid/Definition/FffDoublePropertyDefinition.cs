using DevExpress.Xpf.Editors.Settings;

namespace FarnahadFlowFusion.Core.Control.PropertyGrid.Definition;

public class FffDoublePropertyDefinition : FffPropertyDefinition
{
    public FffDoublePropertyDefinition()
    {
        EditSettings = new SpinEditSettings
        {
            IsFloatValue = true,
            Mask = "N2"
        };
    }
}