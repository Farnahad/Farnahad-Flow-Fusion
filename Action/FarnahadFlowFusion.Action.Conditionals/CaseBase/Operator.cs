// ReSharper disable InvalidXmlDocComment

namespace FarnahadFlowFusion.Action.Conditionals.CaseBase;

public enum Operator
{
    /// <summary>
    /// Contains
    /// </summary>
    Contains,
    /// <summary>
    /// Does not contain
    /// </summary>
    DoesNotContain,
    /// <summary>
    /// Does not end with
    /// </summary>
    DoesNotEndsWith,
    /// <summary>
    /// Does not start with
    /// </summary>
    DoesNotStartWith,
    /// <summary>
    /// Ends with
    /// </summary>
    EndsWith,
    /// <summary>
    /// Equal to (=)
    /// </summary>
    EqualTo,
    /// <summary>
    /// Grater than (>)
    /// </summary>
    GraterThan,
    /// <summary>
    /// Grater than or equal to (>=)
    /// </summary>
    GraterThanOrEqualTo,
    /// <summary>
    /// Is blank
    /// </summary>
    IsBlank,
    /// <summary>
    /// Is empty
    /// </summary>
    IsEmpty,
    /// <summary>
    /// Is not blank
    /// </summary>
    IsNotBlank,
    /// <summary>
    /// Is not empty
    /// </summary>
    IsNotEmpty,
    /// <summary>
    /// Less than (<)
    /// </summary>
    LessThan,
    /// <summary>
    /// Less than or equal to (<=)
    /// </summary>
    LessThanOrEqualTo,
    /// <summary>
    /// Not Equal To (<>)
    /// </summary>
    NotEqualTo,
    /// <summary>
    /// Starts with
    /// </summary>
    StartsWith
}