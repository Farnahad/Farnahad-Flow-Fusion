using System.Data.SqlClient;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.Database;

public class OpenSqlConnection : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Open SQL connection";

    public ActionInput ConnectionString { get; set; }
    public Variable SqlConnection { get; set; }

    public OpenSqlConnection()
    {
        _cSharpService = new CSharpService();

        ConnectionString = new ActionInput();
        SqlConnection = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var connectionStringValue = await _cSharpService.EvaluateActionInput<string>(sandBox, ConnectionString);

        var sqlConnection = new SqlConnection(connectionStringValue);
        sqlConnection.Open();

        SqlConnection.Value = sqlConnection;

        sandBox.Variables.Add(SqlConnection);
    }
}