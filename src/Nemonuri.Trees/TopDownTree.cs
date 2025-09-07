
namespace Nemonuri.Trees;

/// <summary>The default implementation of <see cref="ITree{_}"/></summary>
/// <inheritdoc cref="ITree{_}" path="/typeparam"/>
internal class TopDownTree<TElement> :
    ITree<TElement>
{
    /// <inheritdoc cref="ITree{_}.Value" />
    public TElement Value { get; }
    private readonly IChildrenProvider<TElement> _childrenProvider;
    private readonly ITopDownTreeFactory<TElement> _childToTreeFactory;
    private readonly ITree<TElement>? _parent;

    private IEnumerable<ITree<TElement>>? _children;
    

    public TopDownTree
    (
        TElement value,
        IChildrenProvider<TElement> childrenProvider,
        ITopDownTreeFactory<TElement> childToTreeFactory,
        ITree<TElement>? parent
    )
    {
        Guard.IsNotNull(value);
        Guard.IsNotNull(childrenProvider);

        Value = value;
        _childrenProvider = childrenProvider;
        _childToTreeFactory = childToTreeFactory;
        _parent = parent;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TopDownTree{_}"/> class.
    /// </summary>
    /// <param name="root"></param>
    /// <param name="treeWalker"></param>
    public TopDownTree(TElement root, IChildrenProvider<TElement> childrenProvider, ITree<TElement>? parent = default) :
        this(root, childrenProvider, TopDownTreeFactory<TElement>.Instance, parent)
    { }

    /// <inheritdoc cref="ISupportChildren{_}.Children" />
    public IEnumerable<ITree<TElement>> Children => _children ??=
        _childrenProvider.GetChildren(Value)
            .Select(childValue => _childToTreeFactory.Create(childValue, _childrenProvider, _childToTreeFactory, this));

    /// <inheritdoc cref="ISupportParent{_}.TryGetParent(out _?)" />
    public bool TryGetParent([NotNullWhen(true)] out ITree<TElement>? parent)
    {
        parent = _parent;
        return parent is not null;
    }
}
