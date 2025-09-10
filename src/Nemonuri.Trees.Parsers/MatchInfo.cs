using Nemonuri.Trees.Indexes;

namespace Nemonuri.Trees.Parsers;

public readonly struct MatchInfo
{
    public static readonly MatchInfo InValid = new(null, -1);

    public MatchInfo(IIndexPath? matchIndexPath, int matchLength)
    {
        MatchIndexPath = matchIndexPath;
        MatchLength = matchLength;
    }

    public IIndexPath? MatchIndexPath { get; }
    public int MatchLength { get; }

    public static implicit operator MatchInfo((IndexPath? MatchIndexPath, int MatchLength) v) =>
        new(v.MatchIndexPath, v.MatchLength);

    public bool IsValid => MatchLength >= 0;
}