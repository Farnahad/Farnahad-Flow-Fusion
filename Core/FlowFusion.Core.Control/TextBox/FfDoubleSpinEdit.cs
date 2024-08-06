namespace FarnahadFlowFusion.Core.Control.TextBox;

public class FfDoubleSpinEdit : FfSpinEdit
{
    public FfDoubleSpinEdit()
    {
        EditValueType = typeof(double);
        IsFloatValue = true;
        Mask = "N2";
    }
}