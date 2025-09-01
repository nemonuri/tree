namespace Nemonuri.Trees;

public class RoseTreeNode<T> : ISupportChildren<RoseTreeNode<T>>
{
    public T? Value { get; }

    public ImmutableArray<RoseTreeNode<T>> Children { get; }

    public RoseTreeNode(T? value, ImmutableArray<RoseTreeNode<T>> children)
    {
        Value = value;
        Children = children;
    }

    public RoseTreeNode(T? value) : this(value, ImmutableArray<RoseTreeNode<T>>.Empty)
    { }

    public RoseTreeNode<T> WithValue(T? value) => new(value, Children);

    public RoseTreeNode<T> WithChildren(params ImmutableArray<RoseTreeNode<T>> children) => new(Value, children);

    public RoseTreeNode<T> WithChildrenValues(params IEnumerable<T> childrenValues) =>
        WithChildren([.. childrenValues.Select(static a => new RoseTreeNode<T>(a))]);

    IEnumerable<RoseTreeNode<T>> ISupportChildren<RoseTreeNode<T>>.Children => Children;
}
