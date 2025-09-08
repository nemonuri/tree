using Nemonuri.Trees.Abstractions;

namespace Nemonuri.Trees.Parsers;

public interface IParser<TChar> : ITree<NullAggregation, IParser<TChar>>
{
    ISyntaxForest<TChar> Parse(IString<TChar> @string, int offset);
}

#if false
public class SumParser<TChar, TInfo> : IParser<TChar, TInfo>
{

}

#endif