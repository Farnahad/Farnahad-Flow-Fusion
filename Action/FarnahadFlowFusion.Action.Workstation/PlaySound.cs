using System.Media;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Workstation.PlaySoundBase;
using FarnahadFlowFusion.Service.Workstation.Workstation;

namespace FarnahadFlowFusion.Action.Workstation;

public class PlaySound : IAction
{
    private readonly WorkstationService _workstationService;

    public string Name => "Play sound";

    public PlaySoundFrom PlaySoundFrom { get; set; }
    public SoundToPlay SoundToPlay { get; set; }
    public ActionInput FileToPlay { get; set; }

    public PlaySound()
    {
        _workstationService = new WorkstationService();

        PlaySoundFrom = PlaySoundFrom.System;
        SoundToPlay = SoundToPlay.Asterisk;
        FileToPlay = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        if (PlaySoundFrom == PlaySoundFrom.System)
        {
            switch (SoundToPlay)
            {
                case SoundToPlay.Asterisk:
                    SystemSounds.Asterisk.Play();
                    break;
                case SoundToPlay.Beep:
                    SystemSounds.Beep.Play();
                    break;
                case SoundToPlay.Exclamation:
                    SystemSounds.Exclamation.Play();
                    break;
                case SoundToPlay.Hand:
                    SystemSounds.Hand.Play();
                    break;
                case SoundToPlay.Question:
                    SystemSounds.Question.Play();
                    break;
            }
        }
        else if (PlaySoundFrom == PlaySoundFrom.WavFile)
        {
            var fileToPlayValue = await sandBox.EvaluateActionInput<string>(FileToPlay);
            using var player = new SoundPlayer(fileToPlayValue);
            player.Play();
        }
    }
}