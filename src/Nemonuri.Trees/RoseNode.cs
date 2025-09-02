
namespace Nemonuri.Trees;

public class RoseNode<TElement> : IRoseNode<TElement>
{
    public TElement Value { get; }
    private readonly ImmutableList<IRoseNode<TElement>> _children;

    public RoseNode(TElement value, ImmutableList<IRoseNode<TElement>> children)
    {
        Value = value;
        _children = children;
    }

    public RoseNode(TElement value) : this(value, ImmutableList<IRoseNode<TElement>>.Empty)
    { }

    public RoseNode(TElement value, IEnumerable<TElement> internalChildren) :
        this
        (
            value,
            internalChildren.Select(static a => new RoseNode<TElement>(a)).Cast<IRoseNode<TElement>>().ToImmutableList()
        )
    { }

    public IEnumerable<IRoseNode<TElement>> Children => _children;

    public static implicit operator RoseNode<TElement>(TElement v) => new(v);
    
    public static implicit operator RoseNode<TElement>((TElement Value, RoseNode<TElement>[] Children) v) =>
        new(v.Value, v.Children.Cast<IRoseNode<TElement>>().ToImmutableList());
}
