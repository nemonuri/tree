
namespace Nemonuri.Trees.Parsers;

#if false
public interface ISyntaxTree<TChar, TInfo> : ITree<IInformedString<TChar, TInfo>>, IInformedString<TChar, TInfo>
{
}
#endif

public class SyntaxRoseNode<TChar, TInfo> : IRoseNode<IInformedString<TChar, TInfo>>
{
    private readonly RoseNode<IInformedString<TChar, TInfo>> _internalRoseNode;

    private SyntaxRoseNode(RoseNode<IInformedString<TChar, TInfo>> internalRoseNode)
    {
        Guard.IsNotNull(internalRoseNode);
        _internalRoseNode = internalRoseNode;
    }

    public SyntaxRoseNode(IInformedString<TChar, TInfo> value) : this(new RoseNode<IInformedString<TChar, TInfo>>(value))
    { }

    public SyntaxRoseNode(IInformedString<TChar, TInfo> value, IEnumerable<IInformedString<TChar, TInfo>> internalChildren) :
        this(new RoseNode<IInformedString<TChar, TInfo>>(value, internalChildren))
    { }

    public IInformedString<TChar, TInfo> Value => _internalRoseNode.Value;

    public IEnumerable<IRoseNode<IInformedString<TChar, TInfo>>> Children => _internalRoseNode.Children;
}