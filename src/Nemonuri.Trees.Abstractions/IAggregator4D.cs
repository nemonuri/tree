
namespace Nemonuri.Trees;

public interface IAggregator4D
<TElement, TAggregation, TFlow, TFlowAggregation>
#if NET9_0_OR_GREATER
    where TElement : allows ref struct
    where TAggregation : allows ref struct
    where TFlow : allows ref struct
    where TFlowAggregation : allows ref struct
#endif
{
    TFlowAggregation InitialFlowAggregation { get; }

    TFlowAggregation AggregateFlow
    (
        TFlowAggregation flowAggregation,
        TFlow flow
    );

    TAggregation InitialAggregation { get; }

    TAggregation AggregateElement
    (
        TFlowAggregation flowAggregation,
        TAggregation siblingSequenceUnion,
        TAggregation siblingSequence,
        TAggregation childMatrix,
        TElement element
    );

    TAggregation AggregateSequence
    (
        TAggregation sequenceUnion,
        TAggregation sequence
    );
}
