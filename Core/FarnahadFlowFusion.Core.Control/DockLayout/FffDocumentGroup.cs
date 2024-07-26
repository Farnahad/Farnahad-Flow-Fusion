using DevExpress.Xpf.Docking;

namespace FarnahadFlowFusion.Core.Control.DockLayout;

public class FffDocumentGroup : DocumentGroup
{
    public FffDocumentGroup()
    {
        ClosePageButtonShowMode = ClosePageButtonShowMode.InAllTabPageHeaders;
    }
}