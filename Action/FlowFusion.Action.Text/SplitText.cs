using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Action.Text.SplitTextBase;

namespace FlowFusion.Action.Text;

public class SplitText : IAction
{
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
        var textToSplitValue = await sandBox.EvaluateActionInput<string>(TextToSplit);
        var timesValue = await sandBox.EvaluateActionInput<int>(Times);
        var customDelimiterValue = await sandBox.EvaluateActionInput<string>(CustomDelimiter);
        var splitWidthValue = await sandBox.EvaluateActionInput<int>(SplitWidth);

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

        sandBox.SetVariable(TextList);
    }
}