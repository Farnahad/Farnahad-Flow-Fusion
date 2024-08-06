using DevExpress.Xpf.Docking;

namespace FlowFusion.Core.Control.DockLayout;

public class FfDocumentGroup : DocumentGroup
{
    public FfDocumentGroup()
    {
        ClosePageButtonShowMode = ClosePageButtonShowMode.InAllTabPageHeaders;
    }
}