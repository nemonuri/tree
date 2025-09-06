namespace Nemonuri.Trees.Parsers;

public interface IParser<TChar, TInfo>
{
    IEnumerable<IReadOnlyList<ITree<IInformedString<TChar, TInfo>>>> Parse(IString<TChar> @string, int offset);
}

#if false
public class SumParser<TChar, TInfo> : IParser<TChar, TInfo>
{

}

public class ProductParser<TChar, TInfo> : IParser<TChar, TInfo>
{

}

public class RepeatParser<TChar, TInfo> : IParser<TChar, TInfo>
{ }
#endif