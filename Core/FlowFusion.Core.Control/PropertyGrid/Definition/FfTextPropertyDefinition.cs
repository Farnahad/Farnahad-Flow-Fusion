using DevExpress.Xpf.Editors.Settings;

namespace FlowFusion.Core.Control.PropertyGrid.Definition;

public class FfTextPropertyDefinition : FfPropertyDefinition
{
    public FfTextPropertyDefinition()
    {
        EditSettings = new TextEditSettings();
    }
}