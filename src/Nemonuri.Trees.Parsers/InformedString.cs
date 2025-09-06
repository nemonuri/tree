namespace Nemonuri.Trees.Parsers;

public class InformedString<TChar, TInfo> : IInformedString<TChar, TInfo>
{
    public InformedString(TInfo info, IString<TChar> @string)
    {
        Guard.IsNotNull(info);
        Guard.IsNotNull(@string);

        Info = info;
        String = @string;
    }

    public TInfo Info { get; }
    public IString<TChar> String { get; }
}