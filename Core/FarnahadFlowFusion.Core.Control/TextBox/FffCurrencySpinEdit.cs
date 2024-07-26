namespace FarnahadFlowFusion.Core.Control.TextBox;

public class FffCurrencySpinEdit : FffIntSpinEdit
{
    public FffCurrencySpinEdit()
    {
        MinValue = 0;
        Mask = "C0";
    }
}