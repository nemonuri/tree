using System.Collections;
using System.Collections.Immutable;
using System.Text.RegularExpressions;
using CommunityToolkit.Diagnostics;

namespace Nemonuri.Trees.Forests.Tests.Samples2;

public abstract class Grammar
{ }

public class OuterGrammar : Grammar
{
    public Regex Regex { get; }

    public OuterGrammar(Regex regex)
    {
        Guard.IsNotNull(regex);
        Regex = regex;
    }

    public TextSpan? Match(string sourceText, TextSpan textSpan)
    {
        var match = Regex.Match(sourceText, textSpan.Offset, textSpan.Length);
        if (!match.Success) { return default; }
        return new(match.Index, match.Length);
    }
}

public class InnerGrammar : Grammar
{
    public IEnumerable<Func<SyntaxTree, bool>> Predicates { get; }

    public InnerGrammar(IEnumerable<Func<SyntaxTree, bool>> predicates)
    {
        Predicates = predicates;
    }

    public bool IsMatch(IEnumerable<SyntaxTree> syntaxTrees)
    {
        using var ep = Predicates.GetEnumerator();
        using var es = syntaxTrees.GetEnumerator();

        while (true)
        {
            switch ((ep.MoveNext(), es.MoveNext()))
            {
                case (true, true):
                    if (!ep.Current(es.Current)) { return false; }
                    break;
                case (false, false):
                    return true;
                default:
                    return false;
            }
        }
    }
}

public class RepeatGrammar : Grammar
{
    public Func<SyntaxTree, bool> Predicate { get; }
    public int RepeatCount { get; }

    public RepeatGrammar(Func<SyntaxTree, bool> predicate, int repeatCount)
    {
        Guard.IsNotNull(predicate);
        Predicate = predicate;
        RepeatCount = repeatCount;
    }

    public bool IsMatch(IEnumerable<SyntaxTree> syntaxTrees)
    {
        using var es = syntaxTrees.GetEnumerator();
        int remainedRepeatCount = RepeatCount;

        while (true)
        {
            switch ((remainedRepeatCount > 0, es.MoveNext()))
            {
                case (true, true):
                    if (Predicate(es.Current)) { return false; }
                    break;
                case (false, false):
                    return true;
                default:
                    return false;
            }
            remainedRepeatCount--;
        }
    }
}


public class GrammarComplex
{ }

public class SyntaxTree
{ }

public class SyntaxForest : IEnumerable<ImmutableList<SyntaxTree>>
{
    private readonly ImmutableList<ImmutableList<SyntaxTree>> _syntaxTrees;

    public SyntaxForest(ImmutableList<ImmutableList<SyntaxTree>> syntaxTrees)
    {
        _syntaxTrees = syntaxTrees;
    }

    public IEnumerator<ImmutableList<SyntaxTree>> GetEnumerator()
    {
        return _syntaxTrees.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _syntaxTrees.GetEnumerator();
    }
}



