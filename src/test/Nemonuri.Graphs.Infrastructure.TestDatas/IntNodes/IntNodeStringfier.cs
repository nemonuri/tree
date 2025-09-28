using System.Text;
using System.Diagnostics;

namespace Nemonuri.Graphs.Infrastructure.TestDatas.IntNodes;

public class IntNodeStringfier : IHomogeneousSuccessorAggregator
<
    NullValue, NullValue, string,
    IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet
>
{
    // [Note] 오직 '부작용' 만으로 동작한다.

    private readonly StringBuilder _sb = new();
    public IntNodeStringfier() { }

    public NullValue EmptyPreviousAggregation => default;

    public NullValue AggregatePreviousToInitialNode(scoped ref NullValue mutableContext, NullValue source, IntNode value)
    {
        _sb.Clear();
        Debug.WriteLine($"{nameof(AggregatePostToInitialNode)} {_sb.ToString()} @ {value.Value}");
        _sb.Append(value.Value);
        return default;
    }

    public NullValue AggregatePrevious(scoped ref NullValue mutableContext, NullValue source, PhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, NullValue, string> value)
    {
        Debug.WriteLine($"{nameof(AggregatePrevious)} {_sb.ToString()} @ ,{value.OutArrow.Head.Value}");
        _sb.Append(',').Append(value.OutArrow.Head.Value);
        return default;
    }

    public string EmptyPostAggregation => string.Empty;

    public string AggregatePostToInitialNode(scoped ref NullValue mutableContext, string source, IntNode value)
    {
        Debug.WriteLine($"{nameof(AggregatePostToInitialNode)} {_sb.ToString()}");
        return _sb.ToString();
    }

    public string AggregatePost(scoped ref NullValue mutableContext, string source, PhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, NullValue, string> value)
    {
        throw new InvalidOperationException();
    }

    public IntNodeArrow EmbedToInArrow(IntNodeArrow outArrow) => outArrow;

    public bool CanRunPhase(PhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, NullValue, string> snapshot, AggregatingPhase phase)
    {
        return phase switch
        {
            AggregatingPhase.AggregatePrevious => true,
            AggregatingPhase.AssignPrevious => true,

            AggregatingPhase.AggregateAndAssignChildren => true,
            AggregatingPhase.AggregatePost => false,
            AggregatingPhase.AssignPost => false,

            _ => false
        };
    }

    public IntNodeOutArrowSet GetDirectSuccessorArrows(IntNode node) => new(node);
}
