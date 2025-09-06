

namespace Nemonuri.Trees.Parsers;

public class SyntaxTree<TChar, TInfo> :
    ISyntaxTree<TChar, TInfo>, IRoseNode<IInformedString<TChar, TInfo>>
{
    public IInformedString<TChar, TInfo> Root => throw new NotImplementedException();

    public IChildrenProvider<IInformedString<TChar, TInfo>> ChildrenProvider => throw new NotImplementedException();

    public ITreeFactory<IInformedString<TChar, TInfo>> TreeFactory => throw new NotImplementedException();

    public IInformedString<TChar, TInfo> Value => throw new NotImplementedException();

    public IEnumerable<IRoseNode<IInformedString<TChar, TInfo>>> Children => throw new NotImplementedException();

    public TInfo Info => Value.Info;

    public IString<TChar> String => Value.String;
}