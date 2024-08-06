using FlowFusion.DataAccess.Model.Core;

namespace FlowFusion.Core.Main.Event;

public class AfterModelDeletedEventArgs
{
    public IDbModel DbModel { get; set; }
    public string ViewModelName { get; set; }
}