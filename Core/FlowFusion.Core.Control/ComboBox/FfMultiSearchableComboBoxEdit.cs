﻿using DevExpress.Xpf.Editors;

namespace FarnahadFlowFusion.Core.Control.ComboBox;

public class FfMultiSearchableComboBoxEdit : FfSearchableComboBoxEdit
{
    public FfMultiSearchableComboBoxEdit()
    {
        StyleSettings = new CheckedComboBoxStyleSettings();
    }
}