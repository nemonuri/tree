using System.Diagnostics.CodeAnalysis;

namespace Nemonuri.Trees.Forests.Tests.Samples3;

public interface IGrammar<T, TUnion>
    where T : ISupportConsumableSourceVarientSet
    where TUnion : IEnumerable<T>, ISupportConsumableSourceVarientSet
{
    public string GrammarId { get; }

    bool TryProduce(ConsumableSource consumableSource, [NotNullWhen(true)] out TUnion? product);

    bool IsMatch(T item);
}

public interface ISyntaxForestGrammar : IGrammar<SyntaxForestSequence, SyntaxForestSequenceUnion>
{ }