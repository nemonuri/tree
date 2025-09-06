namespace Nemonuri.Trees.Parsers;

public interface IInformedString<TChar, TInfo>
{
    TInfo Info { get; }
    IString<TChar> String { get; }
}
