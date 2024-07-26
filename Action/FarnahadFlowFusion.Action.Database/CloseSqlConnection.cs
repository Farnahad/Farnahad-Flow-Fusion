using System.Data.SqlClient;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.Database;

public class CloseSqlConnection : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Close SQL connection";

    public ActionInput SqlConnection { get; set; }

    public CloseSqlConnection()
    {
        _cSharpService = new CSharpService();

        SqlConnection = new ActionInput();
    }

    public async Task Execute(SandBox sandBox)
    {
        var sqlConnectionValue = await _cSharpService.EvaluateActionInput<SqlConnection>(sandBox, SqlConnection);

        sqlConnectionValue.Close();
    }
}