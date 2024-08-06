namespace FlowFusion.Action.Main.Variable;

public class ListVariable : Variable
{
    public ListVariable(string name) : base(name, new List<object>())
    {
    }

    public ListVariable(string name, List<object> values) : base(name, values)
    {
    }

    public List<object> GetItems()
    {
        return (List<object>)Value;
    }

    public void Add(object value)
    {
        GetItems().Add(value);
    }

    public void Remove(object value)
    {
        GetItems().Remove(value);
    }
}