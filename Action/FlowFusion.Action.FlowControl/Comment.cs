using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;

namespace FlowFusion.Action.FlowControl;

public class Comment : IAction //XXXXXXXXXXXX
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