using System.Diagnostics.CodeAnalysis;

namespace Nemonuri.Grammars.Infrastructure;

public static class WellFoundedRelationTheory
{
    /**
     * Reference
     * - https://fstar-lang.org/tutorial/book/part2/part2_well_founded.html
     * - https://github.com/FStarLang/FStar/blob/master/ulib/FStar.WellFounded.fst
     */

    public static bool TryCreateLesserIdeal<TIdealPremise, T, TSuperset, TIdeal>
    (
        TIdealPremise premise, TIdeal ideal, T lesserUpperBound,
        [NotNullWhen(true)] out TIdeal? lesserIdeal
    )
        where TIdealPremise : IIdealPremise<T, TSuperset, TIdeal>
        where TIdeal : IIdeal<T>
    {
        TSuperset set = premise.GetCanonicalSuperset(ideal);

        if
        (
            !(
                !premise.IsEmpty(set) &&
                premise.IsMember(set, lesserUpperBound) &&
                premise.IsLesserOrEqualThan(lesserUpperBound, ideal.LeastUpperBound) &&
                !premise.AreEqual(lesserUpperBound, ideal.LeastUpperBound)
            )
        )
        {
            lesserIdeal = default;
            return false;
        }

        lesserIdeal = premise.CreateIdeal(set, lesserUpperBound);
        return true;
    }
}
