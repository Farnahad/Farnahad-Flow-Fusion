using System.Windows.Data;

namespace FarnahadFlowFusion.Core.Main.Application;

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