using System.Data.SqlClient;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;

namespace FarnahadFlowFusion.Action.Database;

public class CloseSqlConnection : IAction
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