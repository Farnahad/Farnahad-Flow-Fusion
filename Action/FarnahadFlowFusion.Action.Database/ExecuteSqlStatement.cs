using System.Data;
using System.Data.SqlClient;
using FarnahadFlowFusion.Action.Database.ExecuteSqlStatementBase;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Variable;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.Database;

public class ExecuteSqlStatement : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Execute SQL statement";

    public GetConnectionBy GetConnectionBy { get; set; }
    public ActionInput SqlConnection { get; set; }
    public ActionInput ConnectionString { get; set; }
    public ActionInput SqlStatement { get; set; }
    public ActionInput Timeout { get; set; }
    public Variable QueryResult { get; set; }

    public ExecuteSqlStatement()
    {
        _cSharpService = new CSharpService();

        GetConnectionBy = GetConnectionBy.SqlConnectionVariable;
        SqlConnection = new ActionInput();
        ConnectionString = new ActionInput();
        SqlStatement = new ActionInput();
        Timeout = new ActionInput();
        QueryResult = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var sqlConnectionValue = await _cSharpService.EvaluateActionInput<string>(sandBox, SqlConnection);
        var connectionStringValue = await _cSharpService.EvaluateActionInput<string>(sandBox, ConnectionString);
        var sqlStatementValue = await _cSharpService.EvaluateActionInput<string>(sandBox, SqlStatement);
        var timeoutValue = await _cSharpService.EvaluateActionInput<int>(sandBox, Timeout);

        var sqlConnection = new SqlConnection();

        switch (GetConnectionBy)
        {
            case GetConnectionBy.ConnectionString:
                sqlConnection = new SqlConnection(connectionStringValue);
                break;
            case GetConnectionBy.SqlConnectionVariable:
                sqlConnection = await _cSharpService.EvaluateActionInput<SqlConnection>(sandBox, SqlConnection);
                break;
        }

        if (sqlConnection.State != ConnectionState.Open)
            sqlConnection.Open();

        var dataTable = new DataTable();

        await using (SqlCommand command = new SqlCommand(sqlStatementValue, sqlConnection))
        {
            await using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                dataTable.Load(reader);
            }
        }

        QueryResult.Value = dataTable;

        sandBox.Variables.Add(QueryResult);
    }
}