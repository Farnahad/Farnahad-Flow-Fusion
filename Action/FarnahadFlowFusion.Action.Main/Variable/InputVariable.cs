namespace FarnahadFlowFusion.Action.Main.Variable;

public class InputVariable : Variable
{
    public object DefaultValue { get; set; }
    public string ExternalName { get; set; }
    public string Description { get; set; }
    public bool MarkAsSensitive { get; set; }
    public bool MarkAsOptional { get; set; }

    public InputVariable()
    {
    }

    public InputVariable(string name, object value) : base(name, value)
    {
        Name = name;
        Value = value;
    }
}