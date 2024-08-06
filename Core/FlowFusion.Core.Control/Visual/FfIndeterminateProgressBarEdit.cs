using DevExpress.Xpf.Editors;

namespace FarnahadFlowFusion.Core.Control.Visual;

public class FfIndeterminateProgressBarEdit : FfProgressBarEdit
{
    public FfIndeterminateProgressBarEdit()
    {
        StyleSettings = new ProgressBarMarqueeStyleSettings();
    }
}