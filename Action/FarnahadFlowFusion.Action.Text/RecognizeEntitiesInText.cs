using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Variable;
using FarnahadFlowFusion.Action.Text.RecognizeEntitiesInTextBase;

namespace FarnahadFlowFusion.Action.Text;

public class RecognizeEntitiesInText : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Recognize entities in text";

    public ActionInput TextToRecognizeFrom { get; set; }
    public EntityType EntityType { get; set; }
    public Language Language { get; set; }
    public Variable RecognizedEntities { get; set; }

    public RecognizeEntitiesInText()
    {
        _cSharpService = new CSharpService();

        TextToRecognizeFrom = new ActionInput();
        EntityType = EntityType.Datetime;
        Language = Language.English;
        RecognizedEntities = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var minimumVaXXXXXXXXXXXXlue = await _cSharpService.EvaluateActionInput<int>(sandBox, XXXXXXXXXXXX);
        var XXXXXXXXXXXX = await _cSharpService.EvaluateActionInput<int>(sandBox, XXXXXXXXXXXX);

        XXXXXXXXXXXX.Value = new Random().Next(XXXXXXXXXXXX, XXXXXXXXXXXX);

        sandBox.Variables.Add(XXXXXXXXXXXX);
    }
}