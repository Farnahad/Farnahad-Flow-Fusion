using FarnahadFlowFusion.DataAccess.Model.Core;

namespace FarnahadFlowFusion.Core.Main.Event;

public class AfterModelDeletedEventArgs
{
    public IDbModel DbModel { get; set; }
    public string ViewModelName { get; set; }
}