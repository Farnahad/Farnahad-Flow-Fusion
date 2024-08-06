namespace FlowFusion.DataAccess.Model.Core;

[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
sealed class DbModelLabelAttribute : Attribute
{
    public string Label { get; }

    public DbModelLabelAttribute(string label)
    {
        Label = label;
    }
}