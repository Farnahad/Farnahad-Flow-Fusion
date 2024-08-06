using DevExpress.Xpf.Docking;

namespace FarnahadFlowFusion.Core.Control.DockLayout;

public class FfDocumentGroup : DocumentGroup
{
    public FfDocumentGroup()
    {
        ClosePageButtonShowMode = ClosePageButtonShowMode.InAllTabPageHeaders;
    }
}