using DevExpress.Xpf.Editors;

namespace FlowFusion.Core.Control.Visual;

public class FfIndeterminateProgressBarEdit : FfProgressBarEdit
{
    public FfIndeterminateProgressBarEdit()
    {
        StyleSettings = new ProgressBarMarqueeStyleSettings();
    }
}