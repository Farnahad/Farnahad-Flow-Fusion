using FlowFusion.Service.Conditionals.Conditionals.Base.CaseBase;

namespace FlowFusion.Service.Conditionals.Conditionals;

public interface IConditionalsService
{
    bool If(object firstOperand, Operator @operator, object secondOperand, bool ignoreCase);
}