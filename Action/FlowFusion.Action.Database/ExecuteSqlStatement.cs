using System.Data;
using System.Data.SqlClient;
using FlowFusion.Action.Database.ExecuteSqlStatementBase;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;

namespace FlowFusion.Action.Database;

public class ExecuteSqlStatement : IAction
{
    public string Name => "Execute SQL statement";

    public GetConnectionBy GetConnectionBy { get; set; }
    public ActionInput SqlConnection { get; set; }
    public ActionInput ConnectionString { get; set; }
    public ActionInput SqlStatement { get; set; }
    public ActionInput Timeout { get; set; }
    public Variable QueryResult { get; set; }

    public ExecuteSqlStatement()
    {
        GetConnectionBy = GetConnectionBy.SqlConnectionVariable;
        SqlConnection = new ActionInput();
        ConnectionString = new ActionInput();
        SqlStatement = new ActionInput();
        Timeout = new ActionInput();
        QueryResult = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var sqlConnectionValue = await sandBox.EvaluateActionInput<string>(SqlConnection);
        var connectionStringValue = await sandBox.EvaluateActionInput<string>(ConnectionString);
        var sqlStatementValue = await sandBox.EvaluateActionInput<string>(SqlStatement);
        var timeoutValue = await sandBox.EvaluateActionInput<int>(Timeout);

        var sqlConnection = new SqlConnection();

        switch (GetConnectionBy)
        {
            case GetConnectionBy.ConnectionString:
                sqlConnection = new SqlConnection(connectionStringValue);
                break;
            case GetConnectionBy.SqlConnectionVariable:
                sqlConnection = await sandBox.EvaluateActionInput<SqlConnection>(SqlConnection);
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

        sandBox.SetVariable(QueryResult);
    }
}