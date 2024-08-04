using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Variable;
using FarnahadFlowFusion.Action.Text.SplitTextBase;

namespace FarnahadFlowFusion.Action.Text;

public class SplitText : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Split text";

    public ActionInput TextToSplit { get; set; }
    public DelimiterType DelimiterType { get; set; }
    public StandardDelimiter StandardDelimiter { get; set; }
    public ActionInput Times { get; set; }
    public ActionInput CustomDelimiter { get; set; }
    public ActionInput SplitWidth { get; set; }
    public Variable TextList { get; set; }

    public SplitText()
    {
        _cSharpService = new CSharpService();

        TextToSplit = new ActionInput();
        DelimiterType = DelimiterType.Standard;
        StandardDelimiter = StandardDelimiter.Space;
        Times = new ActionInput();
        CustomDelimiter = new ActionInput();
        SplitWidth = new ActionInput();
        TextList = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var textToSplitValue = await _cSharpService.EvaluateActionInput<string>(sandBox, TextToSplit);
        var timesValue = await _cSharpService.EvaluateActionInput<int>(sandBox, Times);
        var customDelimiterValue = await _cSharpService.EvaluateActionInput<string>(sandBox, CustomDelimiter);
        var splitWidthValue = await _cSharpService.EvaluateActionInput<int>(sandBox, SplitWidth);

        var result = new List<string>();

        switch (DelimiterType)
        {
            case DelimiterType.Standard:
                string delimiter = "";

                switch (StandardDelimiter)
                {
                    case StandardDelimiter.NewLine:
                        delimiter = Environment.NewLine;
                        break;
                    case StandardDelimiter.Space:
                        delimiter = " ";
                        break;
                    case StandardDelimiter.Tab:
                        delimiter = "\t";
                        break;
                }
                result.AddRange(textToSplitValue.Split(new[] { delimiter }, StringSplitOptions.None));
                break;

            case DelimiterType.Custom:
                result.AddRange(textToSplitValue.Split(new[] { customDelimiterValue }, StringSplitOptions.None));
                break;

            case DelimiterType.NumberOfCharacters:
                for (int i = 0; i < textToSplitValue.Length; i += splitWidthValue)
                {
                    result.Add(textToSplitValue.Substring(i, Math.Min(splitWidthValue, textToSplitValue.Length - i)));
                }
                break;
        }

        result = result.GetRange(0, timesValue);


        TextList.Value = result;

        sandBox.Variables.Add(TextList);
    }
}