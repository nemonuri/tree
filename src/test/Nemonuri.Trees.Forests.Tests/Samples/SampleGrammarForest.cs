
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using CommunityToolkit.Diagnostics;

namespace Nemonuri.Trees.Forests.Tests.Samples;

public class SampleUnitGrammar
{
    public SampleUnitGrammar(Regex regex)
    {
        Regex = regex;
    }

    public Regex Regex { get; }
}

public class SampleGrammarForest :
    IGrammarForest<char, SampleGrammarForest, SampleGrammarSum>
{
    private readonly SampleUnitGrammar? _unitGrammar;
    private readonly SampleGrammarSum? _sum;
    public IEnumerable<SampleGrammarForest> Children { get; }
    public MinMax MinMax { get; }

    public SampleGrammarForest
    (
        SampleGrammarSum? alternateTreeMap,
        IEnumerable<SampleGrammarForest>? children,
        MinMax? minMax
    )
    {
        _unitGrammar = null;

        _sum = alternateTreeMap;
        Children = children ?? [];
        MinMax = minMax ?? new(1, 1);
    }

    public SampleGrammarForest
    (
        SampleUnitGrammar unitGrammar
    )
    {
        _unitGrammar = unitGrammar;

        _sum = null;
        Children = [];
        MinMax = new(1, 1);
    }

    public bool HasAlternateTreeMap => _sum is not null;

    public SampleGrammarSum GetAlternateTreeMap()
    {
        Guard.IsNotNull(_sum);
        return _sum;
    }

    public bool IsUnit =>
        !HasAlternateTreeMap &&
        MinMax.Min == 1 && MinMax.Max == 1 &&
        !Children.Any() && _unitGrammar is not null
        ;

    public SampleUnitGrammar AsUnit
    {
        get
        {
            Guard.IsNotNull(_unitGrammar);
            return _unitGrammar;
        }
    }
}

public class SampleGrammarSum :
    IGrammarSum<char, SampleGrammarForest, SampleGrammarSum>
{
    private readonly SampleGrammarForest? _leaf;
    public IEnumerable<SampleGrammarSum> Children { get; }

    public SampleGrammarSum(SampleGrammarForest leaf)
    {
        _leaf = leaf;

        Children = [];
    }

    public SampleGrammarSum(IEnumerable<SampleGrammarSum> children)
    {
        _leaf = null;

        Children = children;
    }

    public bool ContainsKey(IIndexPath key)
    {
        throw new NotImplementedException();
    }

    public bool TryGetValue(IIndexPath key, [MaybeNullWhen(false)] out SampleGrammarForest value)
    {
        throw new NotImplementedException();
    }

    public SampleGrammarForest this[IIndexPath key] => throw new NotImplementedException();

    public IEnumerable<IIndexPath> Keys => throw new NotImplementedException();

    public IEnumerable<SampleGrammarForest> Values => throw new NotImplementedException();

    public int Count => throw new NotImplementedException();

    public IEnumerator<KeyValuePair<IIndexPath, SampleGrammarForest>> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public bool IsLeaf => _leaf is not null;

    public SampleGrammarForest AsLeaf
    {
        get
        {
            Guard.IsNotNull(_leaf);
            return _leaf;
        }
    }
}