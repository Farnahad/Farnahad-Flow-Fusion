namespace FarnahadFlowFusion.Action.Main;

public class Variable
{
    public string Name { get; set; }
    public object Value { get; set; }

    public Variable()
    {
    }

    public Variable(string name, object value)
    {
        Name = name;
        Value = value;
    }

    public override string ToString()
    {
        return $"{Name} = {Value}";
    }
}