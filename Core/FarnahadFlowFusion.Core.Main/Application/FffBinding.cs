using System.Windows.Data;

namespace FarnahadFlowFusion.Core.Main.Application;

public class FffBinding : Binding
{
    public FffBinding()
    {
            Mode = BindingMode.TwoWay;
            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            ValidatesOnDataErrors = true;
            NotifyOnSourceUpdated = true;
        }
}