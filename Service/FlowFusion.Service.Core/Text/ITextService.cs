﻿namespace ManaErp.Service.Main.Text
{
    public interface ITextService : IService
    {
        string GetEditTitle(string modelName, string modelProperty);
        string GetAddTitle(string modelName);
        string GetListTitle(string modelName);
    }
}