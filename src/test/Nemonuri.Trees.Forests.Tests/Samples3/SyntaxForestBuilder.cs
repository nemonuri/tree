using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Diagnostics;
using SumSharp;

namespace Nemonuri.Trees.Forests.Tests.Samples3;

[UnionCase("Poison")]
[UnionCase("Empty")]
[UnionCase("Sequence", typeof(SyntaxForestSequence))]
[UnionCase("SequenceUnion", typeof(SyntaxForestSequenceUnion))]
public partial class SyntaxForestBuilder
{
    public bool TryGetSequence([NotNullWhen(true)] out SyntaxForestSequence? sequence)
    {
        return (sequence = Match<SyntaxForestSequence?>
        (
            Empty: static () => new SyntaxForestSequence([]),
            Sequence: static a => a,
            _: static () => null
        )) is not null;
    }

    public bool TryGetSequenceUnion([NotNullWhen(true)] out SyntaxForestSequenceUnion? sequenceUnion)
    {
        return (sequenceUnion = Match<SyntaxForestSequenceUnion?>
        (
            Empty: static () => new([new([])]),
            Sequence: static a => new([a]),
            SequenceUnion: static a => a,
            _: static () => null
        )) is not null;
    }

    public SyntaxForestSequenceUnion GetSequenceUnion()
    {
        if (!TryGetSequenceUnion(out var result))
        {
            ThrowHelper.ThrowInvalidOperationException();
        }
        return result;
    }
}
