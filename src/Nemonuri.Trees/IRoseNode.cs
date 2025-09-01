
namespace Nemonuri.Trees;

public interface IRoseNode<TElement> : ISupportChildren<IRoseNode<TElement>>
{
    TElement Value { get; }
}