﻿using DevExpress.Xpf.Editors.Validation;

namespace FarnahadFlowFusion.Core.Control.TextBox;

public abstract class FfMaskTextEdit : FfTextEdit
{
    public FfMaskTextEdit()
    {
        NullText = "Empty";
        MaskUseAsDisplayFormat = true;
        MaskIgnoreBlank = false;
        InvalidValueBehavior = InvalidValueBehavior.AllowLeaveEditor;
    }
}