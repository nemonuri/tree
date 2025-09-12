#if false
using Nemonuri.Trees.Forests;

namespace Nemonuri.Trees.Parsers;

public interface IGrammarForest<TChar, TForest, out TSum> : 
    ITreeStructuredForest<TForest, TSum>
    where TForest : IGrammarForest<TChar, TForest, TSum>
    where TSum : IGrammarSum<TChar, TForest, TSum>
{

}

public interface IGrammarSum<TChar, TForest, out TSum> : 
    ITreeStructuredSum<TForest, TSum>
    where TForest : IGrammarForest<TChar, TForest, TSum>
    where TSum : IGrammarSum<TChar, TForest, TSum>
{ }
#endif