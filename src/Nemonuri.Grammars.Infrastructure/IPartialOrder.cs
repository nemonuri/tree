
namespace Nemonuri.Grammars.Infrastructure;

public interface IPartialOrder<T>
{
    bool IsLesserOrEqualThan(T left, T right);

    bool AreEqual(T left, T right);
}

public interface ILogicalSetPremise<T, TSet>
{ 
    bool IsEmpty(TSet set);

    bool IsMember(TSet set, T item);
}

public interface IPosetPremise<T, TPoset> : IPartialOrder<T>, ILogicalSetPremise<T, TPoset>
{ }

public interface IWellFoundedOrder<T, TPoset> : IPosetPremise<T, TPoset>
{
    bool IsMinimalElement(TPoset poset, T item);
}

public interface ICanonicalSupersetPremise<TSuperset, TSubset>
{
    TSuperset GetCanonicalSuperset(TSubset subset);
}

public interface IIdeal<T>
{
    T LeastUpperBound { get; }
}

public interface IIdealPremise<T, TSuperset, TIdeal> :
    IPosetPremise<T, TSuperset>,
    ICanonicalSupersetPremise<TSuperset, TIdeal>
    where TIdeal : IIdeal<T>
{
    TIdeal CreateIdeal(TSuperset set, T leastUpperBound);
}

public interface ILattice<T> : IIdeal<T>
{
    T GreatestLowerBound { get; }
}

public interface ILatticePremise<T, TSuperset, TLattice> :
    IIdealPremise<T, TSuperset, TLattice>,
    IWellFoundedOrder<T, TSuperset>
    where TLattice : ILattice<T>
{
    TLattice CreateLattice(TSuperset set, T greatestLowerBound, T leastUpperBound);
}
