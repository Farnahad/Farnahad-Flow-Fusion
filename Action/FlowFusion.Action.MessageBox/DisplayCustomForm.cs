﻿using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Main.Variable;

namespace FarnahadFlowFusion.Action.MessageBox;

public class DisplayCustomForm : IAction
{
    public string Name => "Display custom form";

    public List<byte> CustomForm { get; set; }
    public Variable CustomFormData { get; set; }
    public Variable ButtonPressed { get; set; }

    public DisplayCustomForm()
    {

        CustomForm = new List<byte>();
        CustomFormData = new Variable();
        ButtonPressed = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        new global::System.Windows.Window().ShowDialog();

        CustomFormData.Value = "CustomFormData";
        ButtonPressed.Value = "ButtonPressed";

        sandBox.SetVariable(CustomFormData);
        sandBox.SetVariable(ButtonPressed);

        await Task.CompletedTask;
    }
}