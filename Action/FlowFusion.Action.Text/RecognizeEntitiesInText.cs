using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Text.Text;
using FlowFusion.Service.Text.Text.Base;

namespace FlowFusion.Action.Text;

public class RecognizeEntitiesInText(ITextService textService) : IAction
{
    public string Name => "Recognize entities in text";

    public ActionInput TextToRecognizeFrom { get; set; } = new();
    public EntityType EntityType { get; set; } = EntityType.Datetime;
    public Language Language { get; set; } = Language.English;
    public Variable RecognizedEntities { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var textToRecognizeFromValue = await sandBox.EvaluateActionInput<string>(TextToRecognizeFrom);

        RecognizedEntities.Value = textService.RecognizeEntitiesInText(textToRecognizeFromValue, EntityType, Language);

        sandBox.SetVariable(RecognizedEntities);
    }
}