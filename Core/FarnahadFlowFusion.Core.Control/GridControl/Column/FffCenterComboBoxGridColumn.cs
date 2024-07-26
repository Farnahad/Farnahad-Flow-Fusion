using System.Windows;
using DevExpress.Xpf.Editors.Settings;

namespace FarnahadFlowFusion.Core.Control.GridControl.Column;

public class FffCenterComboBoxGridColumn : FffComboBoxGridColumn
{
    public FffCenterComboBoxGridColumn()
    {
        HorizontalHeaderContentAlignment = HorizontalAlignment.Center;
        EditSettings.HorizontalContentAlignment = EditSettingsHorizontalAlignment.Center;
    }
}