using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;

namespace FlowFusion.Service.FlowControl.FlowControl.Base;

public class SetVariable
{
    public int Order { get; set; }
    public Variable Variable { get; set; }
    public ActionInput Value { get; set; }
}