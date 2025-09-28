using System.Text;
using System.Diagnostics;

namespace Nemonuri.Graphs.Infrastructure.TestDatas.IntNodes;

public class IntNodeParenthesizedStringfier : IHomogeneousSuccessorAggregator
<
    NullValue, NullValue, string,
    IntNode, IndexedIntNodeArrow, IndexedIntNodeArrow, IndexedIntNodeOutArrowSet
>
{
    // [Note] 
    // 후계자 중 의 맏이인가, 막내인가는 ...후계자 객체와 후계자 집합의 정보를 직접 대조해 보는 게 가장 안정적인 것 같다.
    // 이제 보니, '후계자 집합'이 일원소 집합인가도 확인할 필요가 있구나!
    // - PhaseSnapshot 에 '후계자 집합'도 포함시켜야겠네.
    // - 내 착각. 후계자 집합이 일원소 집합인가는, 여기서 확인할 필요 없어.

    private readonly StringBuilder _sb = new();
    public IntNodeParenthesizedStringfier()
    {
    }

    public NullValue EmptyPreviousAggregation => default;

    public NullValue AggregatePreviousToInitialNode(scoped ref NullValue mutableContext, NullValue source, IntNode value)
    {
        _sb.Clear();

        if (value.Children.Length > 0)
        {
            _sb.Append('(');
        }

        _sb.Append(value.Value);
        return default;
    }

    public NullValue AggregatePrevious(scoped ref NullValue mutableContext, NullValue source, PhaseSnapshot<IntNode, IndexedIntNodeArrow, IndexedIntNodeArrow, NullValue, string> value)
    {
        _sb.Append(' ');

        if (value.OutArrow.Index == 0)
        {
            _sb.Append('(');
        }

        if (value.OutArrow.Head.Children.Length > 0)
        {
            _sb.Append('(');
        }

        _sb.Append(value.OutArrow.Head.Value);
        
        return default;
    }

    public string EmptyPostAggregation => string.Empty;

    public string AggregatePost(scoped ref NullValue mutableContext, string source, PhaseSnapshot<IntNode, IndexedIntNodeArrow, IndexedIntNodeArrow, NullValue, string> value)
    {
        if
        (
            value.OutArrow.Index ==
            value.OutArrow.Tail.Children.Length - 1 /* 이걸 이렇게 간접이 아니라, OutArrowSet 을 통해 직접 비교할 수 있어야 함 */
        )
        {
            _sb.Append(')');
        }

        if (value.OutArrow.Head.Children.Length > 0)
        {
            _sb.Append('(');
        }

        _sb.Append(')');
        return EmptyPostAggregation;
    }

    public string AggregatePostToInitialNode(scoped ref NullValue mutableContext, string source, IntNode value)
    {
        if (value.Children.Length > 0)
        {
            _sb.Append(')');
        }
        
        return _sb.ToString();
    }

    public IndexedIntNodeArrow EmbedToInArrow(IndexedIntNodeArrow outArrow) => outArrow;

    public bool CanRunPhase(PhaseSnapshot<IntNode, IndexedIntNodeArrow, IndexedIntNodeArrow, NullValue, string> snapshot, AggregatingPhase phase)
    {
        return phase switch
        {
            AggregatingPhase.AggregatePrevious => true,
            AggregatingPhase.AssignPrevious => true,
            AggregatingPhase.AggregateAndAssignChildren => true,
            AggregatingPhase.AggregatePost => true,
            AggregatingPhase.AssignPost => true,

            _ => false
        };
    }

    public IndexedIntNodeOutArrowSet GetDirectSuccessorArrows(IntNode node) => new(node);
}
