namespace FarnahadFlowFusion.Action.Main.Variable;

public class Variable
{
    public string Name { get; set; }
    public object Value { get; set; }
    public bool Enable { get; set; }
    public ValueType ValueType { get; set; }

    public Variable()
    {
        Enable = true;
    }

    public Variable(string name, object value) : base()
    {
        Name = name;
        Value = value;
    }

    public override string ToString()
    {
        return $"{Name} = {Value}";
    }
}