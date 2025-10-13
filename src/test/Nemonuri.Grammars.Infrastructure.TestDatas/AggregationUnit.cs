using System.Diagnostics;

namespace Nemonuri.Grammars.Infrastructure.TestDatas;

[DebuggerDisplay("{DebuggerDisplay,nq}")]
public readonly record struct AggregationUnit
(
    NodeArrowId NodeArrowId, int Offset, int Length
)
{
    public string DebuggerDisplay => $"({NodeArrowId.Id},{Offset},{Length})";
}


