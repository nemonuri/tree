
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using CommunityToolkit.Diagnostics;
using Nemonuri.Trees;
using Nemonuri.Trees.Indexes;
using Nemonuri.Trees.Paths;

namespace Nemonuri.Trees.Forests.Tests.Samples;

public class SampleUnitGrammar : IGrammarUnit<char, SampleUnitGrammar>
{
    public SampleUnitGrammar(Regex regex)
    {
        Regex = regex;
    }

    public Regex Regex { get; }

    public int Match(string text, int offset, int length)
    {
        var match = Regex.Match(text, offset, length);
        if (!match.Success) { return -1; }
        if (!text[offset..].StartsWith(match.Value)) { return -1; }

        return match.ValueSpan.Length;
    }
}

public class SampleGrammarForest :
    IGrammarForest<char, SampleUnitGrammar, SampleGrammarSum, SampleGrammarForest>
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

    public SampleUnitGrammar GetUnitValue()
    {
        Guard.IsNotNull(_unitGrammar);
        return _unitGrammar;
    }

    public int Match(SampleGrammarForest[] text, int offset, int length)
    {
        throw new NotImplementedException();
    }
}

public class SampleGrammarSum :
    IGrammarMap<char, SampleUnitGrammar, SampleGrammarSum, SampleGrammarForest>
{
    private readonly SampleGrammarForest? _leaf;
    private Dictionary<IIndexPath, SampleGrammarForest>? _internalDictionaryCache;
    public IEnumerable<SampleGrammarSum> Children { get; }

    public SampleGrammarSum
    (
        SampleGrammarForest leaf
    )
    {
        _leaf = leaf;

        Children = [];
    }

    public SampleGrammarSum
    (
        IEnumerable<SampleGrammarSum> children
    )
    {
        _leaf = default;

        Children = children;
    }

    private Dictionary<IIndexPath, SampleGrammarForest> InternalDictionary => _internalDictionaryCache ??=
        EnumerateCore().ToDictionary(Constants.DefaultIndexPathEqualityComparer);

    private IEnumerable<KeyValuePair<IIndexPath, SampleGrammarForest>> EnumerateCore()
    {
        IIndexPath? indexPath = null;

        while (TreeNavigaionTheory.TryGetNextIndexPath(this, indexPath, out indexPath, out SampleGrammarSum? tree))
        {
            if (!tree.IsLeaf) { continue; }
            yield return new(indexPath, tree.GetLeafValue());
        }
    }

    public bool ContainsKey(IIndexPath key) => InternalDictionary.ContainsKey(key);

    public bool TryGetValue(IIndexPath key, [MaybeNullWhen(false)] out SampleGrammarForest value) =>
        InternalDictionary.TryGetValue(key, out value);

    public SampleGrammarForest this[IIndexPath key] => InternalDictionary[key];

    public IEnumerable<IIndexPath> Keys => InternalDictionary.Keys;

    public IEnumerable<SampleGrammarForest> Values => InternalDictionary.Values;

    public int Count => InternalDictionary.Count;

    public IEnumerator<KeyValuePair<IIndexPath, SampleGrammarForest>> GetEnumerator() =>
        InternalDictionary.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public bool IsLeaf =>
        _leaf is not null &&
        !Children.Any()
        ;

    public SampleGrammarForest GetLeafValue()
    {
        Guard.IsNotNull(_leaf);
        return _leaf;
    }
}

internal static class Constants
{
    public static readonly IndexPathEqualityComparer DefaultIndexPathEqualityComparer = new();
}