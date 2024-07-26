using System;
using ManaErp.Core.Main.Mvvm;

namespace ManaErp.Service.Main.Session
{
    public interface ISessionService : IService
    {
        ISessionService Instance { get; }
        void RunOnUiThread(Action action);
        void ExitApplication();
    }
}