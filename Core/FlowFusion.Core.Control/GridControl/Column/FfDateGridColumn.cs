using System.Windows;
using DevExpress.Xpf.Editors.Settings;
using FlowFusion.Core.Main.Converter;

namespace FlowFusion.Core.Control.GridControl.Column;

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