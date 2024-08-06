using System.Data.SqlClient;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;

namespace FlowFusion.Action.Database;

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