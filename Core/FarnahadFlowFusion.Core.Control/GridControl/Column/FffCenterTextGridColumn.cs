using System.Windows;
using DevExpress.Xpf.Editors.Settings;

namespace FarnahadFlowFusion.Core.Control.GridControl.Column;

public class FffCenterTextGridColumn : FffTextGridColumn
{
    public FffCenterTextGridColumn()
    {
        HorizontalHeaderContentAlignment = HorizontalAlignment.Center;
        EditSettings.HorizontalContentAlignment = EditSettingsHorizontalAlignment.Center;
    }
}