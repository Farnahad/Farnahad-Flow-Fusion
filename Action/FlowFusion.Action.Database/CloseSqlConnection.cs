using System.Data.SqlClient;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;

namespace FlowFusion.Action.Database;

public class CloseSqlConnection : IAction //XXXXXXXXXXXX
{
    public string Name => "Close SQL connection";

    public ActionInput SqlConnection { get; set; }

    public CloseSqlConnection()
    {
        SqlConnection = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var sqlConnectionValue = await sandBox.EvaluateActionInput<SqlConnection>(SqlConnection);

        sqlConnectionValue.Close();
    }
}