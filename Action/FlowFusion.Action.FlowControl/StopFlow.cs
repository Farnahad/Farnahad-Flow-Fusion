﻿using FarnahadFlowFusion.Action.FlowControl.StopFlowBase;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;

namespace FarnahadFlowFusion.Action.FlowControl;

public class StopFlow : IAction
{
    public string Name => "Stop flow";

    public EndFlow EndFlow { get; set; }
    public ActionInput ErrorMessage { get; set; }

    public StopFlow()
    {
        EndFlow = EndFlow.Successfully;
        ErrorMessage = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var errorMessageValue = await sandBox.EvaluateActionInput<string>(ErrorMessage);
        switch (EndFlow)
        {
            case EndFlow.Successfully:
                sandBox.Exception = null;
                break;
            case EndFlow.WithErrorMessage:
                sandBox.Exception = new Exception(errorMessageValue);
                break;
        }

        sandBox.SandBoxStatus = SandBoxStatus.Stopping;
    }
}