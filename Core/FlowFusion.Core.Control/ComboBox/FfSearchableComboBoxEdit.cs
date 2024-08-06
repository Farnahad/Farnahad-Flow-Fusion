namespace FarnahadFlowFusion.Core.Control.ComboBox;

public class FfSearchableComboBoxEdit : FfComboBoxEdit
{
    public FfSearchableComboBoxEdit()
    {
        ValidateOnTextInput = false;
        IsTextEditable = true;
        FilterCondition = DevExpress.Data.Filtering.FilterCondition.Contains;

    }
}