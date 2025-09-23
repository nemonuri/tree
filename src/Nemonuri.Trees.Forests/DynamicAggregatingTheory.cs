using System.Diagnostics;

namespace Nemonuri.Trees.Forests;

public static class DynamicAggregatingTheory
{
    public static TAggregation Aggregate
    <
        TAggregator, TDynamicNavigator, TFlowConverter,
        TNode, TAggregation, TFlow, TFlowAggregation
    >
    (
        TAggregator aggregator,
        TDynamicNavigator navigator,
        TFlowConverter flowConverter,
        TNode node
    )
        where TAggregator : IAggregator4D<TNode, TAggregation, TFlow, TFlowAggregation>
        where TDynamicNavigator : IDynamicNavigator<TNode, TFlowAggregation, TAggregation>
        where TFlowConverter : IFlowConverter<TNode, MatrixAggregatingFlowInfo<TAggregation>, TFlow>
    {
        var flow = flowConverter.ConvertToFlow(node, new(aggregator.InitialAggregation, aggregator.InitialAggregation));
        var flowAggregation = aggregator.AggregateFlow(aggregator.InitialFlowAggregation, flow);

        var childMatrix = AggregateChildMatrix
        <
            TAggregator, TDynamicNavigator, TFlowConverter,
            TNode, TAggregation, TFlow, TFlowAggregation
        >
        (
            aggregator, navigator, flowConverter, flowAggregation, node
        );

        var siblingSequence = aggregator.AggregateElement
        (
            flowAggregation, aggregator.InitialAggregation, aggregator.InitialAggregation, childMatrix, node
        );

        var siblingSequenceUnion = aggregator.AggregateSequence
        (
            flowAggregation, aggregator.InitialAggregation, siblingSequence
        );

        return siblingSequenceUnion;
    }

    public static TAggregation AggregateChildMatrix
    <
        TAggregator, TDynamicNavigator, TFlowConverter,
        TNode, TAggregation, TFlow, TFlowAggregation
    >
    (
        TAggregator aggregator,
        TDynamicNavigator navigator,
        TFlowConverter flowConverter,
        TFlowAggregation flowAggregation,
        TNode node
    )
        where TAggregator : IAggregator4D<TNode, TAggregation, TFlow, TFlowAggregation>
        where TDynamicNavigator : IDynamicNavigator<TNode, TFlowAggregation, TAggregation>
        where TFlowConverter : IFlowConverter<TNode, MatrixAggregatingFlowInfo<TAggregation>, TFlow>
    {
        Debug.Assert(aggregator is not null);
        Debug.Assert(navigator is not null);
        Debug.Assert(flowConverter is not null);
        Debug.Assert(flowAggregation is not null);
        Debug.Assert(node is not null);

        var childSiblingSequenceUnion = aggregator.InitialAggregation;
        var childSiblingSequence = aggregator.InitialAggregation;
        int ordinalInChildSiblingSequenceUnion = -1;
        int ordinalInChildSiblingSequence = -1;

        while (true)
        {
            //--- get child node ---
            Debug.Assert(ordinalInChildSiblingSequenceUnion >= -1);
            Debug.Assert(ordinalInChildSiblingSequence >= -1);

            TNode? childNode;
            if (DoesNeedToGetFirstChild(ordinalInChildSiblingSequence))
            {
                //--- get first child ---
                if
                (
                    DoesNeedToGetFirstOfFirstChildren(ordinalInChildSiblingSequenceUnion) ?
                        navigator.TryGetFirstOfFirstChildrenInUnion(node, flowAggregation, out childNode) :
                        navigator.TryGetFirstOfNextChildrenInUnion(node, flowAggregation, childSiblingSequenceUnion, out childNode)
                )
                {
                    ordinalInChildSiblingSequenceUnion += 1;
                    ordinalInChildSiblingSequence = 0;

                    childSiblingSequence = aggregator.InitialAggregation;
                }
                else
                {
                    return childSiblingSequenceUnion;
                }
                //---|
            }
            else
            {
                //--- get next sibing ---
                if
                (
                    navigator.TryGetAlternativeNextSibling(node, flowAggregation, childSiblingSequence, out childNode) ||
                    navigator.TryGetDefaultNextSibling(node, flowAggregation, out childNode)
                )
                {
                    ordinalInChildSiblingSequence += 1;
                }
                else
                {
                    ordinalInChildSiblingSequence = -1;

                    childSiblingSequenceUnion = aggregator.AggregateSequence
                    (
                        flowAggregation, childSiblingSequenceUnion, childSiblingSequence
                    );

                    continue;
                }
                //---|
            }

            Debug.Assert(ordinalInChildSiblingSequenceUnion >= 0);
            Debug.Assert(ordinalInChildSiblingSequence >= 0);

            var flow = flowConverter.ConvertToFlow
            (
                node, childNode, new(childSiblingSequenceUnion, childSiblingSequence),
                ordinalInChildSiblingSequenceUnion, ordinalInChildSiblingSequence
            );
            flowAggregation = aggregator.AggregateFlow(flowAggregation, flow);

            var grandChildMatrix = AggregateChildMatrix
            <
                TAggregator, TDynamicNavigator, TFlowConverter,
                TNode, TAggregation, TFlow, TFlowAggregation
            >
            (
                aggregator, navigator, flowConverter, flowAggregation, childNode
            );

            childSiblingSequence = aggregator.AggregateElement
            (
                flowAggregation, childSiblingSequenceUnion, childSiblingSequence, grandChildMatrix, childNode
            );
            //---|
        }


        static bool DoesNeedToGetFirstChild(int ordinalInChildSiblingSequence) => ordinalInChildSiblingSequence < 0;
        static bool DoesNeedToGetFirstOfFirstChildren(int ordinalInChildSiblingSequenceUnion) => ordinalInChildSiblingSequenceUnion < 0;
    }
}

