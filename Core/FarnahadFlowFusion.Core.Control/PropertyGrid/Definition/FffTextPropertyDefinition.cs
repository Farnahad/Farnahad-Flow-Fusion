using DevExpress.Xpf.Editors.Settings;

namespace FarnahadFlowFusion.Core.Control.PropertyGrid.Definition;

public class FffTextPropertyDefinition : FffPropertyDefinition
{
    public FffTextPropertyDefinition()
    {
        EditSettings = new TextEditSettings();
    }
}