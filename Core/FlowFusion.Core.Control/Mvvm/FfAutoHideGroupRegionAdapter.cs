﻿using System.Collections.Specialized;
using System.Windows;
using FarnahadFlowFusion.Core.Control.DockLayout;
using FarnahadFlowFusion.Core.Main.Mvvm;
using Prism.Regions;

namespace FarnahadFlowFusion.Core.Control.Mvvm;

public class FfAutoHideGroupRegionAdapter : RegionAdapterBase<FfAutoHideGroup>
{
    public FfAutoHideGroupRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
        : base(regionBehaviorFactory)
    {
    }

    protected override void Adapt(IRegion region, FfAutoHideGroup regionTarget)
    {
        region.Views.CollectionChanged += (_, args) =>
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                if (args.NewItems != null)
                {
                    foreach (FrameworkElement frameworkElement in args.NewItems)
                    {
                        regionTarget.Add(GetFfLayoutPanel(frameworkElement));
                    }
                }
            }
            else if (args.Action == NotifyCollectionChangedAction.Remove)
            {
                if (args.OldItems != null)
                {
                    foreach (FrameworkElement frameworkElement in args.OldItems)
                    {
                        var ffLayoutPanel = GetFfLayoutPanel(regionTarget, frameworkElement);
                        if (ffLayoutPanel != null)
                            regionTarget.Remove(ffLayoutPanel);
                    }
                }
            }
        };
    }

    protected override IRegion CreateRegion()
    {
        return new Region();
    }

    private FfLayoutPanel GetFfLayoutPanel(FrameworkElement content)
    {
        var viewModel = (ViewModel)content.DataContext;

        var ffLayoutPanel = new FfLayoutPanel
        {
            Caption = viewModel.Title,
            Content = content,
            IsActive = true
        };

        return ffLayoutPanel;
    }

    private FfLayoutPanel GetFfLayoutPanel(FfAutoHideGroup ffAutoHideGroup, FrameworkElement content)
    {
        foreach (var baseLayoutItem in ffAutoHideGroup.Items)
        {
            var ffLayoutPanel = (FfLayoutPanel)baseLayoutItem;
            if (ffLayoutPanel != null && Equals(ffLayoutPanel.Content, content))
                return ffLayoutPanel;
        }

        return null;
    }
}