﻿using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Workstation.ShutdownComputerBase;
using FarnahadFlowFusion.Service.Workstation.Workstation;

namespace FarnahadFlowFusion.Action.Workstation;

public class ShutdownComputer : IAction
{
    private readonly WorkstationService _workstationService;

    public string Name => "Shutdown computer";

    public ActionToPerform ActionToPerform { get; set; }
    public bool Force { get; set; }

    public ShutdownComputer()
    {
        _workstationService = new WorkstationService();

        ActionToPerform = ActionToPerform.Shutdown;
        Force = false;
    }

    public async Task Execute(SandBox sandBox)
    {
        switch (ActionToPerform)
        {
            case ActionToPerform.Hibernate:
                await _workstationService.Hibernate();
                break;
            case ActionToPerform.Restart:
                await _workstationService.Restart(Force);
                break;
            case ActionToPerform.Shutdown:
                await _workstationService.Shutdown(Force);
                break;
            case ActionToPerform.Sleep:
                _workstationService.Sleep(Force);
                break;
        }
    }
}