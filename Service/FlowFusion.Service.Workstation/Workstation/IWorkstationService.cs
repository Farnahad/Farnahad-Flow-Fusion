using FlowFusion.Service.Workstation.Workstation.Base;

namespace FlowFusion.Service.Workstation.Workstation;

public interface IWorkstationService
{
    void ControlScreenSaver(ScreenSaverAction screenSaverAction);
    void EmptyRecycleBin();
    string GetDefaultPrinter();
    (int, int, int, int) GetScreenResolution(int monitorNumber);
    void LockWorkstation();
    void LogOfUser(bool force);
    void PlaySound(PlaySoundFrom playSoundFrom, SoundToPlay soundToPlay, string fileToPlay);
    void PrintDocument(string documentToPrint);
    void SetDefaultPrinter(string printerName);
    void SetScreenResolution(int monitorNumber, int monitorWidth,
        int monitorHeight, int monitorBitCount, int monitorFrequency);
    void ShowDesktop(ShowDesktopOperation showDesktopOperation);
    Task ShutdownComputer(ShutdownComputerActionToPerform shutdownComputerActionToPerform, bool force);
    void TakeScreenshot(ScreenshotCapture screenshotCapture, SaveScreenshotTo saveScreenshotTo, int screenToCapture);
}