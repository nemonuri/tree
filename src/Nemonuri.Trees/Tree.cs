
namespace Nemonuri.Trees;

/// <summary>The default implementation of <see cref="ITree{_}"/></summary>
/// <inheritdoc cref="ITree{_}" path="/typeparam"/>
public class Tree<TElement> :
    ITree<TElement>,
    IEquatable<Tree<TElement>>,
    ISupportChildren<ITree<TElement>>,
    ISupportParent<ITree<TElement>>
{
    private IEnumerable<ITree<TElement>>? _children;
    private readonly ITree<TElement>? _parent;

    public Tree
    (
        TElement root,
        IChildrenProvider<TElement> childrenProvider,
        ITreeFactory<TElement> treeFactory,
        ITree<TElement>? parent
    )
    {
        Guard.IsNotNull(root);
        Guard.IsNotNull(childrenProvider);

        Root = root;
        ChildrenProvider = childrenProvider;
        TreeFactory = treeFactory;
        _parent = parent;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Tree{_}"/> class.
    /// </summary>
    /// <param name="root"></param>
    /// <param name="treeWalker"></param>
    public Tree(TElement root, IChildrenProvider<TElement> childrenProvider, ITree<TElement>? parent = default) :
        this(root, childrenProvider, TreeFactory<TElement>.Instance, parent)
    { }

    /// <inheritdoc cref="ITree{_}.Root" />
    public TElement Root { get; }

    /// <inheritdoc cref="ITree{_}.ChildrenProvider" />
    public IChildrenProvider<TElement> ChildrenProvider { get; }

    public ITreeFactory<TElement> TreeFactory { get; }


    /// <inheritdoc cref="IEquatable{_}.Equals(_)" />
    public bool Equals(Tree<TElement>? other)
    {
        if (other is null) { return false; }

        return
            EqualityComparer<TElement>.Default.Equals(Root, other.Root) &&
            ChildrenProvider.Equals(other.ChildrenProvider);
    }

    /// <inheritdoc cref="Object.Equals(object?)" />
    public override bool Equals(object? obj)
    {
        if (obj is not Tree<TElement> tree)
        {
            return false;
        }

        return this.Equals(tree);
    }

    /// <summary>
    /// Gets the hash code of this <see cref="Tree{_,_,_,_}"/> instance.
    /// </summary>
    /// <inheritdoc cref="int.GetHashCode()" path="/returns"/>
    public override int GetHashCode()
    {
        return HashCode.Combine(Root, ChildrenProvider);
    }

    /// <inheritdoc cref="ISupportChildren{_}.Children" />
    public IEnumerable<ITree<TElement>> Children => _children ??= this.CreateChildren();

    /// <inheritdoc cref="ISupportParent{_}.TryGetParent(out _?)" />
    public bool TryGetParent([NotNullWhen(true)] out ITree<TElement>? parent)
    {
        parent = _parent;
        return parent is not null;
    }


}
