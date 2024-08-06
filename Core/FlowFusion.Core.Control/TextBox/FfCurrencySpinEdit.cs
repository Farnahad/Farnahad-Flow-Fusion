namespace FarnahadFlowFusion.Core.Control.TextBox;

public class FfCurrencySpinEdit : FfIntSpinEdit
{
    public FfCurrencySpinEdit()
    {
        MinValue = 0;
        Mask = "C0";
    }
}