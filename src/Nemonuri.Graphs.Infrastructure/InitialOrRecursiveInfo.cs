using System.Diagnostics.CodeAnalysis;
using SumSharp;

namespace Nemonuri.Graphs.Infrastructure;

public readonly record struct InitialInfo<TNode>(TNode InitialNode);
public readonly record struct RecursiveInfo<TTail, TNode, TInArrow, TAggregation>(TInArrow InArrow, TAggregation PreviousAggregation)
    where TInArrow : IArrow<TTail, TNode>;


[UnionCase("Empty")]
[UnionCase("InitialInfo", $"{nameof(InitialInfo<TNode>)}<{nameof(TNode)}>", UnionCaseStorage.Inline)]
[UnionCase("RecursiveInfo",
    $"{nameof(RecursiveInfo<TTail, TNode, TInArrow, TAggregation>)}<{nameof(TTail)},{nameof(TNode)},{nameof(TInArrow)},{nameof(TAggregation)}>",
    UnionCaseStorage.Inline)]
public partial struct InitialOrRecursiveInfo<TTail, TNode, TInArrow, TAggregation>
    where TInArrow : IArrow<TTail, TNode>
{
    public bool TryGetNode([NotNullWhen(true)] out TNode? node)
    {
        if (IsInitialInfo)
        {
            return (node = AsInitialInfo.InitialNode) is not null;
        }
        else if (IsRecursiveInfo)
        {
            return (node = AsRecursiveInfo.InArrow.Head) is not null;
        }
        else
        {
            node = default;
            return false;
        }
    }

    public bool TryGetPreviousAggregation([NotNullWhen(true)] out TAggregation? previousAggregation) =>
    (
        previousAggregation = Match
        (
            RecursiveInfo: static pi => pi.PreviousAggregation,
            _: static () => default
        )
    ) is not null;

    public static implicit operator InitialOrRecursiveInfo<TTail, TNode, TInArrow, TAggregation>(TNode v) => InitialInfo(new(v));
    public static implicit operator InitialOrRecursiveInfo<TTail, TNode, TInArrow, TAggregation>((TInArrow InArrow, TAggregation PreviousAggregation) v) =>
        RecursiveInfo(new(v.InArrow, v.PreviousAggregation));
}

