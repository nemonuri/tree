using System.Diagnostics.CodeAnalysis;

namespace Nemonuri.Grammars.Infrastructure;

public static class WellFoundedRelationTheory
{
    /**
     * Reference
     * - https://fstar-lang.org/tutorial/book/part2/part2_well_founded.html
     * - https://github.com/FStarLang/FStar/blob/master/ulib/FStar.WellFounded.fst
     */

    public static bool TryCreateLesserIdeal<TPremise, T, TSet, TIdeal>
    (
        TPremise premise, TIdeal ideal, T lesserUpperBound, [NotNullWhen(true)] out TIdeal? lesserIdeal
    )
        where TPremise : IIdealPremise<T, TSet, TIdeal>
        where TIdeal : IIdeal<T>
    {
        TSet set = premise.CastToSet(ideal);

        if
        (
            !(!premise.IsEmpty(set) &&
            premise.IsMember(set, lesserUpperBound) &&
            premise.IsLesserThan(lesserUpperBound, ideal.UpperBound))
        )
        {
            lesserIdeal = default;
            return false;
        }

        lesserIdeal = premise.CreateIdeal(set, lesserUpperBound);
        return true;
    }




}

