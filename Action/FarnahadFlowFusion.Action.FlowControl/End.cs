﻿using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.FlowControl;

public class End : IAction
{
    public string Name => "End";

    public End()
    {
    }

    public async Task Execute(SandBox sandBox)
    {
        sandBox.SandBoxStatus = SandBoxStatus.Stopping;
        await Task.CompletedTask;
    }
}