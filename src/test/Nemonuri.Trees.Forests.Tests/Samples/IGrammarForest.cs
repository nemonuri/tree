namespace Nemonuri.Trees.Forests.Tests.Samples;

public interface IGrammarUnit<TChar, TUnit>
    where TUnit : IGrammarUnit<TChar, TUnit>
{ }

public interface IGrammarMap<TChar, TUnit, out TMap, TForest> :
    ITreeStructuredMap<TForest, TMap>
    where TUnit : IGrammarUnit<TChar, TUnit>
    where TForest : IGrammarForest<TChar, TUnit, TMap, TForest>
    where TMap : IGrammarMap<TChar, TUnit, TMap, TForest>
{ }


public interface IGrammarForest<TChar, TUnit, out TMap, TForest> :
    ITreeStructuredForest<TUnit, TMap, TForest>
    where TUnit : IGrammarUnit<TChar, TUnit>
    where TForest : IGrammarForest<TChar, TUnit, TMap, TForest>
    where TMap : IGrammarMap<TChar, TUnit, TMap, TForest>
{
    MinMax MinMax { get; }
}

