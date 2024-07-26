using System.Windows;
using DevExpress.Xpf.Editors.Settings;
using FarnahadFlowFusion.Core.Main.Converter;

namespace FarnahadFlowFusion.Core.Control.GridControl.Column;

public class FffDateTimeGridColumn : FffGridColumn
{
    public FffDateTimeGridColumn()
    {
        EditSettings = new TextEditSettings
        {
            DisplayTextConverter = new HijriDateTimeValueConverter(),
            HorizontalContentAlignment = EditSettingsHorizontalAlignment.Right,
            VerticalContentAlignment = VerticalAlignment.Center
        };
    }
}