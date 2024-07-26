using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.MessageBox;

public class DisplayCustomForm : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Display custom form";

    public List<byte> CustomForm { get; set; }
    public Variable CustomFormData { get; set; }
    public Variable ButtonPressed { get; set; }

    public DisplayCustomForm()
    {
        _cSharpService = new CSharpService();

        CustomForm = new List<byte>();
        CustomFormData = new Variable();
        ButtonPressed = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        new global::System.Windows.Window().ShowDialog();

        CustomFormData.Value = "CustomFormData";
        ButtonPressed.Value = "ButtonPressed";

        sandBox.Variables.Add(CustomFormData);
        sandBox.Variables.Add(ButtonPressed);

        await Task.CompletedTask;
    }
}