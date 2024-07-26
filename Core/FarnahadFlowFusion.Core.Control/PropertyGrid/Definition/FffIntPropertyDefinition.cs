using DevExpress.Xpf.Editors.Settings;

namespace FarnahadFlowFusion.Core.Control.PropertyGrid.Definition;

public class FffIntPropertyDefinition : FffPropertyDefinition
{
    public FffIntPropertyDefinition()
    {
        EditSettings = new SpinEditSettings
        {
            IsFloatValue = false,
            Mask = "N2"
        };
    }
}