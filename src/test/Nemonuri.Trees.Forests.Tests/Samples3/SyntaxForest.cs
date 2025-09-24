namespace Nemonuri.Trees.Forests.Tests.Samples3;

public class SyntaxForest : IForest<SyntaxForest, SyntaxForestSequence, SyntaxForestSequenceUnion>, ISupportConsumableSourceVarientSet
{
    public SyntaxForest(IGrammar<SyntaxForestSequence, SyntaxForestSequenceUnion>? grammar, SyntaxForestSequenceUnion childMatrix)
    {
        Grammar = grammar;
        ChildMatrix = childMatrix;
    }

    public IGrammar<SyntaxForestSequence, SyntaxForestSequenceUnion>? Grammar { get; }

    public SyntaxForestSequenceUnion ChildMatrix { get; }

    public ConsumableSourceVarientSet ConsumableSourceVarientSet => ChildMatrix.ConsumableSourceVarientSet;
}
