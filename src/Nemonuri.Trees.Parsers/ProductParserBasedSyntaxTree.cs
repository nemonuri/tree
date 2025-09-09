
namespace Nemonuri.Trees.Parsers;

internal class ProductParserBasedSyntaxTree<TChar> : ISyntaxTree<TChar>
{
    public SyntaxTreeInfo<TChar> Value => throw new NotImplementedException();

    public IEnumerable<ISyntaxTree<TChar>> Children => throw new NotImplementedException();

    public bool TryGetBoundParent([NotNullWhen(true)] out ISyntaxTree<TChar>? parent)
    {
        throw new NotImplementedException();
    }

    public ISyntaxTree<TChar> BindParent(ISyntaxTree<TChar>? settingParent)
    {
        throw new NotImplementedException();
    }
}