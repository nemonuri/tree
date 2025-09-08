namespace Nemonuri.Trees.Parsers;


public class RepeatParser<TChar, TInfo> : IParser<TChar, TInfo>
{
    private readonly MinMax _minMax;
    private readonly IParser<TChar, TInfo> _internalParser;

    public RepeatParser(MinMax minMax, IParser<TChar, TInfo> internalParser)
    {
        Guard.IsNotNull(internalParser);

        _minMax = minMax;
        _internalParser = internalParser;
    }

    public IEnumerable<ITree<IInformedString<TChar, TInfo>>> Parse(IString<TChar> @string, int offset)
    {
        int repeatCount = 0;
        int positionInString = 0;


    }
}
