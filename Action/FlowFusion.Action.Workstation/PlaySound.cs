using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Service.Workstation.Workstation;
using FlowFusion.Service.Workstation.Workstation.Base;

namespace FlowFusion.Action.Workstation;

public class PlaySound(IWorkstationService workstationService) : IAction
{
    public string Name => "Play sound";

    public PlaySoundFrom PlaySoundFrom { get; set; } = PlaySoundFrom.System;
    public SoundToPlay SoundToPlay { get; set; } = SoundToPlay.Asterisk;
    public ActionInput FileToPlay { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var fileToPlayValue = await sandBox.EvaluateActionInput<string>(FileToPlay);

        workstationService.PlaySound(PlaySoundFrom, SoundToPlay, fileToPlayValue);
    }
}