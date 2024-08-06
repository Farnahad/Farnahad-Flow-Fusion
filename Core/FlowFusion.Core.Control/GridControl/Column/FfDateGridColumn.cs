using System.Windows;
using DevExpress.Xpf.Editors.Settings;
using FarnahadFlowFusion.Core.Main.Converter;

namespace FarnahadFlowFusion.Core.Control.GridControl.Column;

public class FfDateGridColumn : FfGridColumn
{
    public FfDateGridColumn()
    {
        EditSettings = new TextEditSettings
        {
            DisplayTextConverter = new HijriDateValueConverter(),
            HorizontalContentAlignment = EditSettingsHorizontalAlignment.Right,
            VerticalContentAlignment = VerticalAlignment.Center
        };
    }
}