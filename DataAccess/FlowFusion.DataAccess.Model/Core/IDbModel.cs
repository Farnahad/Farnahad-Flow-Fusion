namespace FlowFusion.DataAccess.Model.Core;

public interface IDbModel
{
    int Id { get; }
    byte[] RowVersion { get; }
    string GetModelName();
    string GetPluralModelName();
    string ToString();
}