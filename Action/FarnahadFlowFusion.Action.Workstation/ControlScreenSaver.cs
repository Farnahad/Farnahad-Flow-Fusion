﻿using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Workstation.ControlScreenSaverBase;

namespace FarnahadFlowFusion.Action.Workstation;

public class ControlScreenSaver : IAction
{
    private readonly CSharpService _cSharpService;
    private readonly WorkstationService _workstationService;

    public string Name => "Control screen saver";

    public ScreenSaverAction ScreenSaverAction { get; set; }

    public ControlScreenSaver()
    {
        _cSharpService = new CSharpService();
        _workstationService = new WorkstationService();

        ScreenSaverAction = ScreenSaverAction.Enable;
    }

    public async Task Execute(SandBox sandBox)
    {
        switch (ScreenSaverAction)
        {
            case ScreenSaverAction.Disable:
                _workstationService.DisableScreenSaver();
                break;
            case ScreenSaverAction.Enable:
                _workstationService.EnableScreenSaver();
                break;
            case ScreenSaverAction.Start:
                _workstationService.StartScreenSaver();
                break;
            case ScreenSaverAction.Stop:
                _workstationService.StopScreenSaver();
                break;
        }

        await Task.CompletedTask;
    }
}