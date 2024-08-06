using FlowFusion.DataAccess.Model.Core;

namespace FlowFusion.Core.Main.Event;

public class AfterModelEditedEventArgs
{
    public IDbModel DbModel { get; set; }
    public string ViewModelName { get; set; }
}