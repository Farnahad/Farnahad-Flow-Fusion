using DevExpress.Xpf.Editors.Settings;

namespace FarnahadFlowFusion.Core.Control.PropertyGrid.Definition;

public class FfTextPropertyDefinition : FfPropertyDefinition
{
    public FfTextPropertyDefinition()
    {
        EditSettings = new TextEditSettings();
    }
}