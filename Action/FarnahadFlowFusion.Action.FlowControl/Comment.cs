using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;

namespace FarnahadFlowFusion.Action.FlowControl;

public class Comment : IAction
{
    public string Name => "Comment";

    public string CommentText { get; set; }

    public Comment()
    {
        CommentText = "";
    }

    public async Task Execute(SandBox sandBox)
    {
        await Task.CompletedTask;
    }
}