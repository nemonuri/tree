using SumSharp;

namespace Nemonuri.Trees.Forests.Tests.Samples;

[UnionCase("None")]
[UnionCase("Some", "T")]
public partial struct Optional<T>
{ }
