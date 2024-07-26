namespace FarnahadFlowFusion.Core.Control.TextBox;

public class FffDoubleSpinEdit : FffSpinEdit
{
    public FffDoubleSpinEdit()
    {
        EditValueType = typeof(double);
        IsFloatValue = true;
        Mask = "N2";
    }
}