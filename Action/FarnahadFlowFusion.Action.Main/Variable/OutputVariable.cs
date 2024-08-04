namespace FarnahadFlowFusion.Action.Main.Variable;

public class OutputVariable : Variable
{
    public string ExternalName { get; set; }
    public string Description { get; set; }
    public bool MarkAsSensitive { get; set; }

    public OutputVariable()
    {
    }

    public OutputVariable(string name, object value) : base(name, value)
    {
        Name = name;
        Value = value;
    }
}