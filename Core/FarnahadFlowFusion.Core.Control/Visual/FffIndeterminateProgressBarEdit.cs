using DevExpress.Xpf.Editors;

namespace FarnahadFlowFusion.Core.Control.Visual;

public class FffIndeterminateProgressBarEdit : FffProgressBarEdit
{
    public FffIndeterminateProgressBarEdit()
    {
        StyleSettings = new ProgressBarMarqueeStyleSettings();
    }
}