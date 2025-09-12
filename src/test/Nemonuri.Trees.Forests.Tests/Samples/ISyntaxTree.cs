using System.Collections.Immutable;
using CommunityToolkit.Diagnostics;
using Nemonuri.Trees.Abstractions;

namespace Nemonuri.Trees.Forests.Tests.Samples;

public class GrammarLabel : IEquatable<GrammarLabel>
{
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

public interface ILabeledGrammar
{
    GrammarLabel GetGrammarLabel();
}

public interface IStringGrammar : ILabeledGrammar
{
    int Match(string source, int offset, int length);
}

public interface ISyntaxTreeListGrammar : ILabeledGrammar
{
    int Match(IReadOnlyList<ISyntaxTree> source, int offset, int length);
}

public interface ISyntaxTree :
    IBottomUpRoseTree<GrammarTree, ISyntaxTree>
{
    GrammarTree GrammarTree { get; }
}

public class SyntaxForest :
    IMultiAxisTree<ISyntaxTree, SyntaxForest>,
    ISyntaxTree
{
    private readonly string? _stringSource;
    private readonly IReadOnlyList<ISyntaxTree>? _syntaxTreeListSource;
    private readonly int _offset;
    private readonly int _length;

    public GrammarTree GrammarTree => throw new NotImplementedException();

    public IEnumerable<ISyntaxTree> Children => throw new NotImplementedException();

    public GrammarTree Value => throw new NotImplementedException();

    public int AxisCount => throw new NotImplementedException();

    public IEnumerable<SyntaxForest> GetChildrenFromAxis(int axisIndex)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ISyntaxTree> UnboundChildren => throw new NotImplementedException();

    public ISyntaxTree BindParent(ISyntaxTree parent)
    {
        throw new NotImplementedException();
    }

    public bool HasParent => throw new NotImplementedException();

    public ISyntaxTree GetParent()
    {
        throw new NotImplementedException();
    }
}

public static class GrammarForestTheory
{
    public static int AxisCount() => 2;
}

public enum GrammarForestAxisKind : int
{
    Space = 0,
    Possibility = 1
}
