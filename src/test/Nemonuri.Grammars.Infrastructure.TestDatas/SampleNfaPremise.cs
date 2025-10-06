using System.Collections.Immutable;
using Nemonuri.Graphs.Infrastructure;

namespace Nemonuri.Grammars.Infrastructure.TestDatas;

public class SampleNfaPremise<T, TNode> : INfaPremise
<
    ValueNull, ValueNull, SequenceIdealContext<T, TNode>, ValueNull, ImmutableList<SequenceChunk<T>>, ImmutableList<SequenceChunk<T>>,
    
>
{ 

}