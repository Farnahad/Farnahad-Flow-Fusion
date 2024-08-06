using DevExpress.Xpf.Ribbon.Internal;
using FlowFusion.Core.Control.Ribbon;
using Prism.Regions;

namespace FlowFusion.Core.Control.Mvvm;

public class FfRibbonControlRegionAdapter : RegionAdapterBase<FfRibbonControl>
{
    public FfRibbonControlRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
        : base(regionBehaviorFactory)
    {
    }

    protected override void Adapt(IRegion region, FfRibbonControl regionTarget)
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