namespace Nemonuri.Grammars.Infrastructure;

public static class LatticeTheory
{
    public static bool TryCreateSublattice<TLatticePremise, T, TSuperset, TLattice>
    (
        TLatticePremise premise, TLattice lattice,
        T lowerBound, T upperBound,
        [NotNullWhen(true)] out TLattice? sublattice
    )
        where TLatticePremise : ILatticePremise<T, TSuperset, TLattice>
        where TLattice : ILattice<T>
    {
        TSuperset set = premise.GetCanonicalSuperset(lattice);

        bool isValidLowerBound =
            !premise.IsEmpty(set) &&
            premise.IsMember(set, lowerBound) &&
            premise.IsLesserOrEqualThan(lattice.GreatestLowerBound, lowerBound);

        bool isValidUpperBound =
            !premise.IsEmpty(set) &&
            premise.IsMember(set, upperBound) &&
            premise.IsLesserOrEqualThan(upperBound, lattice.LeastUpperBound);

        if (!(isValidLowerBound && isValidUpperBound))
        {
            sublattice = default;
            return false;
        }

        sublattice = premise.CreateLattice(set, lowerBound, upperBound);
        return true;
    }
}