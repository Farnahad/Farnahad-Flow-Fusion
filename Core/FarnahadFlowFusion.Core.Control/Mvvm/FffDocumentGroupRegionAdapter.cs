using System.Collections.Specialized;
using System.Windows;
using FarnahadFlowFusion.Core.Control.DockLayout;
using FarnahadFlowFusion.Core.Main.Mvvm;
using Prism.Regions;

namespace FarnahadFlowFusion.Core.Control.Mvvm;

public class FffDocumentGroupRegionAdapter : RegionAdapterBase<FffDocumentGroup>
{
    public FffDocumentGroupRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
        : base(regionBehaviorFactory)
    {
    }

    protected override void Adapt(IRegion region, FffDocumentGroup regionTarget)
    {
        region.Views.CollectionChanged += (_, args) =>
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                if (args.NewItems != null)
                {
                    foreach (FrameworkElement frameworkElement in args.NewItems)
                        regionTarget.Add(GetFffDocumentPanel(frameworkElement));
                }
            }
            else if (args.Action == NotifyCollectionChangedAction.Remove)
            {
                if (args.OldItems != null)
                {
                    foreach (FrameworkElement frameworkElement in args.OldItems)
                    {
                        var fffDocumentPanel = GetFffDocumentPanel(regionTarget, frameworkElement);
                        if (fffDocumentPanel != null)
                            regionTarget.Remove(fffDocumentPanel);
                    }
                }
            }
        };
    }

    protected override IRegion CreateRegion()
    {
        return new Region();
    }

    private FffDocumentPanel GetFffDocumentPanel(FrameworkElement content)
    {
        var viewModel = (ViewModel)content.DataContext;

        var fffDocumentPanel = new FffDocumentPanel
        {
            Caption = viewModel.Title,
            Content = content,
            IsActive = true
        };

        return fffDocumentPanel;
    }

    private FffDocumentPanel GetFffDocumentPanel(FffDocumentGroup fffDocumentGroup, FrameworkElement content)
    {
        foreach (var baseLayoutItem in fffDocumentGroup.Items)
        {
            var fffDocumentPanel = (FffDocumentPanel)baseLayoutItem;
            if (fffDocumentPanel != null && Equals(fffDocumentPanel.Content, content))
                return fffDocumentPanel;
        }

        return null;
    }
}