using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;

namespace FlowFusion.Core.Control.Bar;

public class FfBarManager : BarManager
{
    public FfBarManager()
    {
        CreateStandardLayout = false;
        WorkspaceManager.SetIsEnabled(this, true);
    }
}