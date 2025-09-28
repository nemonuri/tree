namespace Nemonuri.Graphs.Infrastructure.TestDatas.IntNodes;

public class IntNodeAdder() : IHomogeneousSuccessorAggregator
<
    NullValue, NullValue, int,
    IntNode, IntNodeArrow, IntNodeArrow, IntNodeOutArrowSet
>
{
    public NullValue EmptyPreviousAggregation => default;

    public NullValue AggregatePreviousToInitialNode(scoped ref NullValue mutableContext, NullValue source, IntNode value)
    {
        return default;
    }

    public NullValue AggregatePrevious(scoped ref NullValue mutableContext, NullValue source, PhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, NullValue, int> value)
    {
        return default;
    }

    public int EmptyPostAggregation => 0;

    public int AggregatePostToInitialNode(scoped ref NullValue mutableContext, int source, IntNode value)
    {
        return source + value.Value;
    }

    public int AggregatePost(scoped ref NullValue mutableContext, int source, PhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, NullValue, int> value)
    {
        return source + value.OutArrow.Head.Value;
    }

    public IntNodeArrow EmbedToInArrow(IntNodeArrow outArrow)
    {
        return outArrow;
    }

    public bool CanRunPhase(PhaseSnapshot<IntNode, IntNodeArrow, IntNodeArrow, NullValue, int> snapshot, AggregatingPhase phase)
    {
        return phase switch
        {
            AggregatingPhase.AggregatePrevious
            or AggregatingPhase.AssignPrevious => false,

            AggregatingPhase.AggregateAndAssignChildren
            or AggregatingPhase.AggregatePost
            or AggregatingPhase.AssignPost => true,

            _ => false
        };
    }

    public IntNodeOutArrowSet GetDirectSuccessorArrows(IntNode node)
    {
        return new(node, [.. node.Children]);
    }
}