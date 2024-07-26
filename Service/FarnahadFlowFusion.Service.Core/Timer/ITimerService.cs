using System;
using ManaErp.Core.Main.Mvvm;

namespace ManaErp.Service.Main.Timer
{
    public interface ITimerService : IService
    {
        void SetConfig(Action action, int intervalSeconds);
        void Start();
        void Stop();
    }
}