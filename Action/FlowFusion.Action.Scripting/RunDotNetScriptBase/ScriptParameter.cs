namespace FlowFusion.Action.Scripting.RunDotNetScriptBase;

public class ScriptParameter
{
    public string Name { get; set; }
    public Type Type { get; set; }
    public Direction Direction { get; set; }
    public ActionInput InputValue { get; set; }
    public ActionInput OutputValue { get; set; }
}