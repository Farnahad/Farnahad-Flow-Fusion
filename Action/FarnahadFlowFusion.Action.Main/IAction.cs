namespace FarnahadFlowFusion.Action.Main;

public interface IAction
{
    string Name { get; }
    Task Execute(SandBox sandBox);
}