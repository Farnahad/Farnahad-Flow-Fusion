using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;

namespace FarnahadFlowFusion.Core.Control.Bar;

public class FffBarManager : BarManager
{
    public FffBarManager()
    {
        CreateStandardLayout = false;
        WorkspaceManager.SetIsEnabled(this, true);
    }
}