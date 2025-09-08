namespace Nemonuri.Trees.Parsers;

public interface IStringSegment<TChar> : IString<TChar>
{
    IString<TChar> InternalString { get; }
    int Offset { get; }
}
