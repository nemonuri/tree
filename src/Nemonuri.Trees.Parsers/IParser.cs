namespace Nemonuri.Trees.Parsers;

public interface IParser<TChar> : IBinderRoseTree<IParser<TChar>, IParser<TChar>>, IParentTreeBindable<IParser<TChar>, IParser<TChar>>
{
    ISyntaxForest<TChar> Parse(IString<TChar> @string, int offset);
}

#if false
public class SumParser<TChar, TInfo> : IParser<TChar, TInfo>
{

}

#endif