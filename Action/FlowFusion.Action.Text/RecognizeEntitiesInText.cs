using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Action.Text.RecognizeEntitiesInTextBase;

namespace FlowFusion.Action.Text;

public class RecognizeEntitiesInText : IAction //XXXXXXXXXXXX
{
    public string Name => "Recognize entities in text";

    public ActionInput TextToRecognizeFrom { get; set; }
    public EntityType EntityType { get; set; }
    public Language Language { get; set; }
    public Variable RecognizedEntities { get; set; }

    public RecognizeEntitiesInText()
    {
        TextToRecognizeFrom = new ActionInput();
        EntityType = EntityType.Datetime;
        Language = Language.English;
        RecognizedEntities = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var minimumVaXXXXXXXXXXXXlue = await sandBox.EvaluateActionInput<int>(XXXXXXXXXXXX);
        var XXXXXXXXXXXX = await sandBox.EvaluateActionInput<int>(XXXXXXXXXXXX);

        XXXXXXXXXXXX.Value = new Random().Next(XXXXXXXXXXXX, XXXXXXXXXXXX);

        sandBox.SetVariable(XXXXXXXXXXXX);
    }
}