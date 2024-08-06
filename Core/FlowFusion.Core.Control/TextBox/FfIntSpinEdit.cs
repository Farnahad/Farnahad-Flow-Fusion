namespace FarnahadFlowFusion.Core.Control.TextBox;

public class FfIntSpinEdit : FfSpinEdit
{
    public FfIntSpinEdit()
    {
        EditValueType = typeof(int);
        IsFloatValue = false;
        Mask = "D0";
    }
}