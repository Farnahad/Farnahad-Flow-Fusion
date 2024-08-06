using System.Windows;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Action.MessageBox.DisplayMessageBase;

namespace FlowFusion.Action.MessageBox;

public class DisplayMessage : IAction //XXXXXXXXXXXX
{
    public string Name => "Display message";

    public ActionInput MessageBoxTitle { get; set; }
    public ActionInput MessageToDisplay { get; set; }
    public MessageBoxIcon MessageBoxIcon { get; set; }
    public MessageBoxButtons MessageBoxButtons { get; set; }
    public DefaultButton DefaultButton { get; set; }
    public bool KeepMessageBoxAlwaysOnTop { get; set; }
    public bool CloseMessageBoxAutomatically { get; set; }
    public Variable ButtonPressed { get; set; }

    public DisplayMessage()
    {
        MessageBoxTitle = new ActionInput();
        MessageToDisplay = new ActionInput();
        MessageBoxIcon = MessageBoxIcon.None;
        MessageBoxButtons = MessageBoxButtons.Ok;
        DefaultButton = DefaultButton.FirstButton;
        KeepMessageBoxAlwaysOnTop = false;
        CloseMessageBoxAutomatically = false;
        ButtonPressed = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var messageBoxTitleValue = await sandBox.EvaluateActionInput<string>(MessageBoxTitle);
        var messageToDisplayValue = await sandBox.EvaluateActionInput<string>(MessageToDisplay);

        var messageBoxImage = MessageBoxIcon switch
        {
            MessageBoxIcon.Error => global::System.Windows.MessageBoxImage.Error,
            MessageBoxIcon.Information => global::System.Windows.MessageBoxImage.Information,
            MessageBoxIcon.None => global::System.Windows.MessageBoxImage.None,
            MessageBoxIcon.Question => global::System.Windows.MessageBoxImage.Question,
            MessageBoxIcon.Warning => global::System.Windows.MessageBoxImage.Warning,
            _ => global::System.Windows.MessageBoxImage.None
        };

        var messageBoxButton = global::System.Windows.MessageBoxButton.OK;
        switch (MessageBoxButtons)
        {
            case MessageBoxButtons.AboutRetryIgnore:
                //messageBoxButton = global::System.Windows.MessageBoxButton.AboutRetryIgnore;
                break;
            case MessageBoxButtons.Ok:
                messageBoxButton = global::System.Windows.MessageBoxButton.OK;
                break;
            case MessageBoxButtons.OkCancel:
                messageBoxButton = global::System.Windows.MessageBoxButton.OKCancel;
                break;
            case MessageBoxButtons.RetryCancel:
                //messageBoxButton = global::System.Windows.MessageBoxButton.RetryCancel;
                break;
            case MessageBoxButtons.YesNo:
                messageBoxButton = global::System.Windows.MessageBoxButton.YesNo;
                break;
            case MessageBoxButtons.YesNoCancel:
                messageBoxButton = global::System.Windows.MessageBoxButton.YesNoCancel;
                break;
        }

        var messageBoxResult = DefaultButton switch
        {
            DefaultButton.FirstButton => MessageBoxResult.OK,
            DefaultButton.SecondButton => MessageBoxResult.Yes,
            DefaultButton.ThirdButton => MessageBoxResult.None,
            _ => global::System.Windows.MessageBoxResult.None
        };

        if (KeepMessageBoxAlwaysOnTop)
        {
        }

        ButtonPressed.Value = global::System.Windows.MessageBox.Show(
            messageToDisplayValue, messageBoxTitleValue, messageBoxButton, messageBoxImage, messageBoxResult);

        if (CloseMessageBoxAutomatically)
        {
            //Timer timer = new Timer();
            //timer.Interval = 3000;
            //timer.Tick += (sender, args) =>
            //{
            //    timer.Stop();
            //    if (result == DialogResult.OK)
            //        Application.Exit();
            //};
            //timer.Start();
        }

        sandBox.SetVariable(ButtonPressed);
    }
}