using FlowFusion.Service.Workstation.Workstation.Base;
using System.Media;
using FlowFusion.Core.Windows.Printer;
using FlowFusion.Core.Windows.Windows;
using FlowFusion.Core.Windows.Screen;

namespace FlowFusion.Service.Workstation.Workstation;

public class WorkstationService : IWorkstationService
{
    public void ControlScreenSaver(ScreenSaverAction screenSaverAction)
    {
        switch (screenSaverAction)
        {
            case ScreenSaverAction.Disable:
                WindowsUtility.DisableScreenSaver();
                break;
            case ScreenSaverAction.Enable:
                WindowsUtility.EnableScreenSaver();
                break;
            case ScreenSaverAction.Start:
                WindowsUtility.StartScreenSaver();
                break;
            case ScreenSaverAction.Stop:
                WindowsUtility.StopScreenSaver();
                break;
        }
    }

    public void EmptyRecycleBin()
    {
        WindowsUtility.EmptyRecycleBin();
    }

    public string GetDefaultPrinter()
    {
        return PrinterUtility.GetDefaultPrinterName();
    }

    public (int, int, int, int) GetScreenResolution(int monitorNumber)
    {
        throw new NotImplementedException();
    }

    public void LockWorkstation()
    {
        throw new NotImplementedException();
    }

    public void LogOfUser(bool force)
    {
        throw new NotImplementedException();
    }

    public void PlaySound(PlaySoundFrom playSoundFrom, SoundToPlay soundToPlay, string fileToPlay)
    {
        if (playSoundFrom == PlaySoundFrom.System)
        {
            switch (soundToPlay)
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
        else if (playSoundFrom == PlaySoundFrom.WavFile)
        {
            using var player = new SoundPlayer(fileToPlay);
            player.Play();
        }
    }

    public void PrintDocument(string documentToPrint)
    {
        try
        {
            var defaultPrinter = GetDefaultPrinter();
            PrinterUtility.PrintRawData(defaultPrinter, documentToPrint);
        }
        catch
        {
            // ignored
        }
    }

    public void SetDefaultPrinter(string printerName)
    {
        PrinterUtility._SetDefaultPrinter(printerName);
    }

    public void SetScreenResolution(int monitorNumber, int monitorWidth, int monitorHeight, int monitorBitCount,
        int monitorFrequency)
    {
        ScreenUtility.SetScreenResolution(monitorNumber, monitorWidth, monitorHeight, monitorBitCount, monitorFrequency);
    }

    public void ShowDesktop(ShowDesktopOperation showDesktopOperation)
    {
        switch (showDesktopOperation)
        {
            case ShowDesktopOperation.MinimizeAllWindowsShowDesktop:
                break;
            case ShowDesktopOperation.RestoreAllWindowsUndoShowDesktop:
                break;
        }
    }

    public async Task ShutdownComputer(ShutdownComputerActionToPerform shutdownComputerActionToPerform, bool force)
    {
        switch (shutdownComputerActionToPerform)
        {
            case ShutdownComputerActionToPerform.Hibernate:
                await WindowsUtility.Hibernate();
                break;
            case ShutdownComputerActionToPerform.Restart:
                await WindowsUtility.Restart(force);
                break;
            case ShutdownComputerActionToPerform.Shutdown:
                await WindowsUtility.Shutdown(force);
                break;
            case ShutdownComputerActionToPerform.Sleep:
                WindowsUtility.Sleep(force);
                break;
        }
    }

    public void TakeScreenshot(ScreenshotCapture screenshotCapture, SaveScreenshotTo saveScreenshotTo, int screenToCapture)
    {
    }
}