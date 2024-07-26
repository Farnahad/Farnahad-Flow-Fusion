using FarnahadFlowFusion.Action.Main;

namespace FarnahadFlowFusion.Action.FlowControl.OnBlockErrorBase;

public class SetVariable
{
    public int Order { get; set; }
    public Variable Variable { get; set; }
    public ActionInput Value { get; set; }
}