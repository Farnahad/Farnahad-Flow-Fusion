using System.Windows;
using DevExpress.Xpf.Editors.Settings;

namespace FlowFusion.Core.Control.GridControl.Column;

public class FfCenterComboBoxGridColumn : FfComboBoxGridColumn
{
    public FfCenterComboBoxGridColumn()
    {
        HorizontalHeaderContentAlignment = HorizontalAlignment.Center;
        EditSettings.HorizontalContentAlignment = EditSettingsHorizontalAlignment.Center;
    }
}