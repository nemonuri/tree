#if false
using Nemonuri.Trees.Abstractions;

namespace Nemonuri.Trees.Parsers;

public class SyntaxTree<TChar, TInfo> :
    ISyntaxTree<TChar, TInfo>,
    ISupportChildren<ISyntaxTree<TChar, TInfo>>,
    ISupportParent<ISyntaxTree<TChar, TInfo>>
{
    private IEnumerable<ITree<TElement>>? _children;
    private readonly ITree<TElement>? _parent;

    public SyntaxTree(IInformedString<TChar, TInfo> value) : this(new RoseNode<ISyntaxTree<TChar, TInfo>>(value))
    { }

    public SyntaxTree(IInformedString<TChar, TInfo> value, IEnumerable<IInformedString<TChar, TInfo>> internalChildren) :
        this(new RoseNode<ISyntaxTree<TChar, TInfo>>(value, internalChildren))
    { }

    public IInformedString<TChar, TInfo> Root => this;

    public IChildrenProvider<IInformedString<TChar, TInfo>> ChildrenProvider => ChildrenProviderImpl.Instance;

    public ITreeFactory<IInformedString<TChar, TInfo>> TreeFactory => throw new NotImplementedException();

    public IInformedString<TChar, TInfo> Value => throw new NotImplementedException();

    public IEnumerable<IRoseNode<ISyntaxTree<TChar, TInfo>>> Children => throw new NotImplementedException();

    public TInfo Info => Value.Info;

    public IString<TChar> String => Value.String;

    private class ChildrenProviderImpl : IChildrenProvider<IInformedString<TChar, TInfo>>
    {
        public static readonly ChildrenProviderImpl Instance = new();

        private ChildrenProviderImpl() { }
        public IEnumerable<IInformedString<TChar, TInfo>> GetChildren(IInformedString<TChar, TInfo> source) =>
            source is ISupportChildren<IInformedString<TChar, TInfo>> supportChildren ? supportChildren.Children : [];
    }

    private class TreeFactoryImpl : ITreeFactory<IInformedString<TChar, TInfo>>
    {
        public static readonly TreeFactoryImpl Instance = new();

        private TreeFactoryImpl() { }

        public ITree<IInformedString<TChar, TInfo>> Create
        (
            IInformedString<TChar, TInfo> root,
            IChildrenProvider<IInformedString<TChar, TInfo>> childrenProvider,
            ITree<IInformedString<TChar, TInfo>>? parent
        )
        {
            throw new NotImplementedException();
        }
    }

    public bool TryGetParent([NotNullWhen(true)] out ISyntaxTree<TChar, TInfo>? parent)
    {
        throw new NotImplementedException();
    }

    IEnumerable<ISyntaxTree<TChar, TInfo>> ISupportChildren<ISyntaxTree<TChar, TInfo>>.Children => throw new NotImplementedException();
}
#endif