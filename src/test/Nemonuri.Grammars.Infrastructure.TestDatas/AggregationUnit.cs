using System.Diagnostics;
using System.Text;

namespace Nemonuri.Grammars.Infrastructure.TestDatas;

[DebuggerDisplay("{DebuggerDisplay,nq}")]
public readonly record struct AggregationUnit
(
    NodeArrowId NodeArrowId, int Offset, int Length
)
{
    public string DebuggerDisplay => $"({NodeArrowId.Id},{Offset},{Length})";

    public string GetSliceDisplay<T>(IReadOnlyList<T> canon)
    {
        StringBuilder sb = new();
        sb.Append('[').Append(NodeArrowId.Id).Append('|');
        for (int i = Offset; i < Offset + Length; i++)
        {
            if (i > Offset) { sb.Append(','); }
            sb.Append(canon[i]);
        }
        sb.Append(']');
        return sb.ToString();
    }
}


