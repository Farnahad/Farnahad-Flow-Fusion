using Prism.Commands;

namespace FarnahadFlowFusion.Core.Main.Command;

public class FffCommand : DelegateCommand
{
    public FffCommand(Action executeMethod) : base(executeMethod)
    {
    }

    public FffCommand(Action executeMethod, Func<bool> canExecuteMethod)
        : base(executeMethod, canExecuteMethod)
    {
    }
}

public class FffCommand<T> : DelegateCommand<T>
{
    public FffCommand(Action<T> executeMethod) : base(executeMethod)
    {
    }

    public FffCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        : base(executeMethod, canExecuteMethod)
    {
    }
}