namespace FarnahadFlowFusion.Core.Control.ComboBox;

public class FffSearchableComboBoxEdit : FffComboBoxEdit
{
    public FffSearchableComboBoxEdit()
    {
        ValidateOnTextInput = false;
        IsTextEditable = true;
        FilterCondition = DevExpress.Data.Filtering.FilterCondition.Contains;

    }
}