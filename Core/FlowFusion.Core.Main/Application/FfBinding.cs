using System.Windows.Data;

namespace FlowFusion.Core.Main.Application;

public class FfBinding : Binding
{
    public FfBinding()
    {
            Mode = BindingMode.TwoWay;
            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            ValidatesOnDataErrors = true;
            NotifyOnSourceUpdated = true;
        }
}