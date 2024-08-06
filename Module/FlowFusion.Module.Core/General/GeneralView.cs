using System.Windows.Controls;

namespace ManaErp.Module.Core.General
{
    public class GeneralView : View
    {
        public override void SetTitle(string title)
        {
            base.SetTitle(title);

            if (string.IsNullOrWhiteSpace(title) == false)
            {
                if (Parent is ContentPresenter contentPresenter)
                {
                    if (contentPresenter.Parent is MeDocumentPanel meDocumentPanel)
                    {
                        meDocumentPanel.Caption = title;
                    }
                }

                if (Parent is MeDxWindow meDxWindow)
                {
                    meDxWindow.Title = title;
                }
            }
        }
    }
}