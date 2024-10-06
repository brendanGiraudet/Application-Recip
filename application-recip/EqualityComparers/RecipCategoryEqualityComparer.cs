using System.Diagnostics.CodeAnalysis;
using ms_recip.Ms_recip.Models;

namespace application_recip.EqualityComparers;

public class RecipCategoryEqualityComparer : IEqualityComparer<RecipCategoryModel>
{
    public bool Equals(RecipCategoryModel? x, RecipCategoryModel? y)
    {
        return x?.RecipId == y?.RecipId
            && x?.CategoryId == y?.CategoryId;
    }

    public int GetHashCode([DisallowNull] RecipCategoryModel obj)
    {
        return obj.GetHashCode();
    }
}
