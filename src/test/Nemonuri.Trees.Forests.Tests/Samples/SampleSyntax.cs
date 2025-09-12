using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using CommunityToolkit.Diagnostics;
using Nemonuri.Trees;
using Nemonuri.Trees.Indexes;
using Nemonuri.Trees.Paths;

namespace Nemonuri.Trees.Forests.Tests.Samples;

#if false
public class SampleSyntaxTree : ITree<SampleSyntaxTree>
{

}

public class SampleSyntaxForest : 
    IAlterableTree<SampleSyntaxForest, IIndexPath, SampleSyntaxMap>
{
    public SampleGrammarForest Grammar { get; }

    public bool HasAlternateTreeMap => throw new NotImplementedException();

    public SampleSyntaxMap GetAlternateTreeMap()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<SampleSyntaxForest> Children => throw new NotImplementedException();
}

public class SampleSyntaxMap : IReadOnlyDictionary<IIndexPath, SampleSyntaxForest>
{ }
#endif