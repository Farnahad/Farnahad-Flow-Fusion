using System.Data.SqlClient;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Main.Variable;

namespace FarnahadFlowFusion.Action.Database;

public class OpenSqlConnection : IAction
{
    public string Name => "Open SQL connection";

    public ActionInput ConnectionString { get; set; }
    public Variable SqlConnection { get; set; }

    public OpenSqlConnection()
    {

        ConnectionString = new ActionInput();
        SqlConnection = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var connectionStringValue = await sandBox.EvaluateActionInput<string>(ConnectionString);

        var sqlConnection = new SqlConnection(connectionStringValue);
        sqlConnection.Open();

        SqlConnection.Value = sqlConnection;

        sandBox.SetVariable(SqlConnection);
    }
}