
namespace Nemonuri.Grammars.Infrastructure;

public interface IStrictPartialOrder<T>
{
    bool IsLesserThan(T less, T greater);
}

public interface ILogicalSetPremise<T, TSet>
{ 
    bool IsEmpty(TSet set);

    bool IsMember(TSet set, T item);
}

public interface IWellFoundedRelation<T, TSet> :
    ILogicalSetPremise<T, TSet>,
    IStrictPartialOrder<T>
{
    bool IsMinimalElement(TSet set, T item);
}

public interface IIdeal<T>
{
    T UpperBound { get; }
}

public interface IIdealPremise<T, TSet, TIdeal> :
    IWellFoundedRelation<T, TSet>
    where TIdeal : IIdeal<T>
{
    TIdeal CreateIdeal(TSet set, T upperBound);

    TSet CastToSet(TIdeal ideal);
}

public interface ILessAccessor<TAccessor, T, TSubset, TWellFoundedRelation> : ISupportFixedPoint<TAccessor>
    where TAccessor : ILessAccessor<TAccessor, T, TSubset, TWellFoundedRelation>
    where TWellFoundedRelation : IWellFoundedRelation<T, TSubset>
{
    TWellFoundedRelation WellFoundedRelation { get; }

    T UpperBound { get; }

    TSubset Subset { get; }

    TAccessor GetLessAccessor(T lessValue);
}

