using System.Collections.Specialized;
using System.Windows;
using FarnahadFlowFusion.Core.Control.DockLayout;
using FarnahadFlowFusion.Core.Main.Mvvm;
using Prism.Regions;

namespace FarnahadFlowFusion.Core.Control.Mvvm;

public class FffAutoHideGroupRegionAdapter : RegionAdapterBase<FffAutoHideGroup>
{
    public FffAutoHideGroupRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
        : base(regionBehaviorFactory)
    {
    }

    protected override void Adapt(IRegion region, FffAutoHideGroup regionTarget)
    {
        region.Views.CollectionChanged += (_, args) =>
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                if (args.NewItems != null)
                {
                    foreach (FrameworkElement frameworkElement in args.NewItems)
                    {
                        regionTarget.Add(GetFffLayoutPanel(frameworkElement));
                    }
                }
            }
            else if (args.Action == NotifyCollectionChangedAction.Remove)
            {
                if (args.OldItems != null)
                {
                    foreach (FrameworkElement frameworkElement in args.OldItems)
                    {
                        var fffLayoutPanel = GetFffLayoutPanel(regionTarget, frameworkElement);
                        if (fffLayoutPanel != null)
                            regionTarget.Remove(fffLayoutPanel);
                    }
                }
            }
        };
    }

    protected override IRegion CreateRegion()
    {
        return new Region();
    }

    private FffLayoutPanel GetFffLayoutPanel(FrameworkElement content)
    {
        var viewModel = (ViewModel)content.DataContext;

        var fffLayoutPanel = new FffLayoutPanel
        {
            Caption = viewModel.Title,
            Content = content,
            IsActive = true
        };

        return fffLayoutPanel;
    }

    private FffLayoutPanel GetFffLayoutPanel(FffAutoHideGroup fffAutoHideGroup, FrameworkElement content)
    {
        foreach (var baseLayoutItem in fffAutoHideGroup.Items)
        {
            var fffLayoutPanel = (FffLayoutPanel)baseLayoutItem;
            if (fffLayoutPanel != null && Equals(fffLayoutPanel.Content, content))
                return fffLayoutPanel;
        }

        return null;
    }
}