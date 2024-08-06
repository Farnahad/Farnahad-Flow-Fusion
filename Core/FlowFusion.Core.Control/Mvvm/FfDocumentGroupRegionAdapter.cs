using System.Collections.Specialized;
using System.Windows;
using FarnahadFlowFusion.Core.Control.DockLayout;
using FarnahadFlowFusion.Core.Main.Mvvm;
using Prism.Regions;

namespace FarnahadFlowFusion.Core.Control.Mvvm;

public class FfDocumentGroupRegionAdapter : RegionAdapterBase<FfDocumentGroup>
{
    public FfDocumentGroupRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
        : base(regionBehaviorFactory)
    {
    }

    protected override void Adapt(IRegion region, FfDocumentGroup regionTarget)
    {
        region.Views.CollectionChanged += (_, args) =>
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                if (args.NewItems != null)
                {
                    foreach (FrameworkElement frameworkElement in args.NewItems)
                        regionTarget.Add(GetFfDocumentPanel(frameworkElement));
                }
            }
            else if (args.Action == NotifyCollectionChangedAction.Remove)
            {
                if (args.OldItems != null)
                {
                    foreach (FrameworkElement frameworkElement in args.OldItems)
                    {
                        var ffDocumentPanel = GetFfDocumentPanel(regionTarget, frameworkElement);
                        if (ffDocumentPanel != null)
                            regionTarget.Remove(ffDocumentPanel);
                    }
                }
            }
        };
    }

    protected override IRegion CreateRegion()
    {
        return new Region();
    }

    private FfDocumentPanel GetFfDocumentPanel(FrameworkElement content)
    {
        var viewModel = (ViewModel)content.DataContext;

        var ffDocumentPanel = new FfDocumentPanel
        {
            Caption = viewModel.Title,
            Content = content,
            IsActive = true
        };

        return ffDocumentPanel;
    }

    private FfDocumentPanel GetFfDocumentPanel(FfDocumentGroup ffDocumentGroup, FrameworkElement content)
    {
        foreach (var baseLayoutItem in ffDocumentGroup.Items)
        {
            var ffDocumentPanel = (FfDocumentPanel)baseLayoutItem;
            if (ffDocumentPanel != null && Equals(ffDocumentPanel.Content, content))
                return ffDocumentPanel;
        }

        return null;
    }
}