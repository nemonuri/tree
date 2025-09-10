
namespace Nemonuri.Trees.Parsers;

internal class ProductParserBasedSyntaxTree<TChar> : IBinderSyntaxTree<TChar>
{
    public SyntaxTreeInfo<TChar> Value => throw new NotImplementedException();

    public IEnumerable<IBinderSyntaxTree<TChar>> Children => throw new NotImplementedException();

    public bool TryGetBoundParent([NotNullWhen(true)] out IBinderSyntaxTree<TChar>? parent)
    {
        throw new NotImplementedException();
    }

    public IBinderSyntaxTree<TChar> BindParent(IBinderSyntaxTree<TChar>? settingParent)
    {
        throw new NotImplementedException();
    }
}