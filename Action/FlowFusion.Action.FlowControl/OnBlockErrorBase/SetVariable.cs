using FlowFusion.Action.Main.Variable;

namespace FlowFusion.Action.FlowControl.OnBlockErrorBase;

public class SetVariable
{
    public int Order { get; set; }
    public Variable Variable { get; set; }
    public ActionInput Value { get; set; }
}