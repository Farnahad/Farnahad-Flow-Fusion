using Prism.Commands;

namespace FarnahadFlowFusion.Core.Main.Command;

public class FfCommand : DelegateCommand
{
    public FfCommand(Action executeMethod) : base(executeMethod)
    {
    }

    public FfCommand(Action executeMethod, Func<bool> canExecuteMethod)
        : base(executeMethod, canExecuteMethod)
    {
    }
}