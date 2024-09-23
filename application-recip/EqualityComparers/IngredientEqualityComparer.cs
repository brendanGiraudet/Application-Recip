using ms_recip.Ms_recip.Models;
using System.Diagnostics.CodeAnalysis;

namespace application_recip.EqualityComparers;

public class IngredientEqualityComparer : IEqualityComparer<IngredientModel>
{
    public bool Equals(IngredientModel? x, IngredientModel? y)
    {
        return x?.Id == y?.Id
            && x?.Name == y?.Name
            && x?.Image == y?.Image;
    }

    public int GetHashCode([DisallowNull] IngredientModel obj)
    {
        return obj.GetHashCode();
    }
}
