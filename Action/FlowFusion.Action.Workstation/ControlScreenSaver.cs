using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Workstation.ControlScreenSaverBase;
using FlowFusion.Service.Workstation.Workstation;

namespace FlowFusion.Action.Workstation;

public class ControlScreenSaver : IAction //XXXXXXXXXXXX
{
    private readonly WorkstationService _workstationService;

    public string Name => "Control screen saver";

    public ScreenSaverAction ScreenSaverAction { get; set; }

    public ControlScreenSaver()
    {
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