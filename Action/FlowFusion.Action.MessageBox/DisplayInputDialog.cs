﻿using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Main.Variable;
using FarnahadFlowFusion.Action.MessageBox.DisplayInputDialogBase;

namespace FarnahadFlowFusion.Action.MessageBox;

public class DisplayInputDialog : IAction
{
    public string Name => "Display input dialog";

    public ActionInput InputDialogTitle { get; set; }
    public ActionInput InputDialogMessage { get; set; }
    public ActionInput DefaultValue { get; set; }
    public InputType InputType { get; set; }
    public bool KeepInputDialogAlwaysOnTop { get; set; }
    public Variable UerInput { get; set; }
    public Variable ButtonPressed { get; set; }

    public DisplayInputDialog()
    {
        InputDialogTitle = new ActionInput();
        InputDialogMessage = new ActionInput();
        DefaultValue = new ActionInput();
        InputType = InputType.SingleLine;
        KeepInputDialogAlwaysOnTop = false;
        UerInput = new Variable();
        ButtonPressed = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var inputDialogTitleValue = await sandBox.EvaluateActionInput<string>(InputDialogTitle);
        var inputDialogMessageValue = await sandBox.EvaluateActionInput<string>(InputDialogMessage);
        var defaultValueValue = await sandBox.EvaluateActionInput<string>(DefaultValue);

        switch (InputType)
        {
            case InputType.Multiline:
                break;
            case InputType.Password:
                break;
            case InputType.SingleLine:
                break;
        }

        if (KeepInputDialogAlwaysOnTop)
        {
        }

        UerInput.Value = "UerInput";
        ButtonPressed.Value = "ButtonPressed";

        sandBox.SetVariable(UerInput);
        sandBox.SetVariable(ButtonPressed);
    }
}