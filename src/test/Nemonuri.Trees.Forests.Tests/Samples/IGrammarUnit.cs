using CommunityToolkit.Diagnostics;
using SumSharp;

namespace Nemonuri.Trees.Forests.Tests.Samples;

public class GrammarLabel : IEquatable<GrammarLabel>
{
    public static readonly GrammarLabel None = new("");

    public string StringLabel { get; }

    public GrammarLabel(string stringLabel)
    {
        Guard.IsNotNull(stringLabel);
        StringLabel = stringLabel;
    }

    public bool Equals(GrammarLabel? other)
    {
        if (other is null) { return false; }

        return StringLabel == other.StringLabel;
    }

    public override bool Equals(object? obj) => Equals(obj as GrammarLabel);

    public override int GetHashCode()
    {
        return StringLabel.GetHashCode();
    }
}

public interface IGrammarUnit
{
    GrammarLabel GetGrammarLabel();
}

public interface IStringGrammarUnit : IGrammarUnit
{
    int Match(string source, TextSpan textSpan);
}

public readonly record struct StringGrammarUnitMatchEntry(string Source, TextSpan TextSpan);
public readonly record struct StringGrammarUnitMatchInfo(StringGrammarUnitMatchEntry Entry, int Result);

public interface ISyntaxTreeListGrammarUnit : IGrammarUnit
{
    bool Match(IReadOnlyList<SyntaxForest> source);
}

public readonly record struct SyntaxTreeListGrammarUnitMatchInfo(IReadOnlyList<SyntaxForest> Entry);

[UnionCase("String", typeof(StringGrammarUnitMatchInfo), UnionCaseStorage.Default)]
[UnionCase("SyntaxTreeList", typeof(SyntaxTreeListGrammarUnitMatchInfo), UnionCaseStorage.Default)]
public partial class UnionedMatchInfo
{ }