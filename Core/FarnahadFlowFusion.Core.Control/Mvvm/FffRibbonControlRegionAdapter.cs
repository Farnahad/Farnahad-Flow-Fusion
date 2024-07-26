using DevExpress.Xpf.Ribbon.Internal;
using FarnahadFlowFusion.Core.Control.Ribbon;
using Prism.Regions;

namespace FarnahadFlowFusion.Core.Control.Mvvm;

public class FffRibbonControlRegionAdapter : RegionAdapterBase<FffRibbonControl>
{
    public FffRibbonControlRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
        : base(regionBehaviorFactory)
    {
    }

    protected override void Adapt(IRegion region, FffRibbonControl regionTarget)
    {
        region.Views.CollectionChanged += (_, e) =>
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                if (e.NewItems != null)
                {
                    foreach (IRibbonItem element in e.NewItems)
                        regionTarget.Items.Add(element);
                }
            }

            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                if (e.OldItems != null)
                {
                    foreach (IRibbonItem element in e.OldItems)
                        regionTarget.Items.Remove(element);
                }
            }
        };
    }

    protected override IRegion CreateRegion()
    {
        return new AllActiveRegion();
    }
}