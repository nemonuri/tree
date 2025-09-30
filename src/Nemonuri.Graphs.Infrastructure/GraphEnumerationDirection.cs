using A = Nemonuri.Graphs.Infrastructure.AggregatingPhaseTheory;

namespace Nemonuri.Graphs.Infrastructure;

public enum GraphEnumerationDirection : int
{
    Unknown = 0,
    Previous = A.Previous,
    Post = A.Post
}

public readonly record struct GraphEnumerationItem<TNode>(TNode Node, GraphEnumerationDirection Direction);