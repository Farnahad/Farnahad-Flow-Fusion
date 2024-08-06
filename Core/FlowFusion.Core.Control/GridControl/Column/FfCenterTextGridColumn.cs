using System.Windows;
using DevExpress.Xpf.Editors.Settings;

namespace FarnahadFlowFusion.Core.Control.GridControl.Column;

public class FfCenterTextGridColumn : FfTextGridColumn
{
    public FfCenterTextGridColumn()
    {
        HorizontalHeaderContentAlignment = HorizontalAlignment.Center;
        EditSettings.HorizontalContentAlignment = EditSettingsHorizontalAlignment.Center;
    }
}