using Prism.Commands;

namespace FarnahadFlowFusion.Core.Main.Command;

public class FfCommand<T> : DelegateCommand<T>
{
    public FfCommand(Action<T> executeMethod) : base(executeMethod)
    {
    }

    public FfCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        : base(executeMethod, canExecuteMethod)
    {
    }
}