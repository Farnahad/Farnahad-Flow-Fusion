namespace FarnahadFlowFusion.Core.Control.TextBox;

public class FffIntSpinEdit : FffSpinEdit
{
    public FffIntSpinEdit()
    {
        EditValueType = typeof(int);
        IsFloatValue = false;
        Mask = "D0";
    }
}