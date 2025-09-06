
namespace Nemonuri.Trees.Parsers;

public interface ISyntaxTree<TChar, TInfo> : ITree<IInformedString<TChar, TInfo>>, IInformedString<TChar, TInfo>
{
}
