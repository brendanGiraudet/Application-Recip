using ms_recip.Ms_recip.Models;
using System.Diagnostics.CodeAnalysis;

namespace application_recip.EqualityComparers;

public class RecipEqualityComparer : IEqualityComparer<RecipModel>
{
    public bool Equals(RecipModel? x, RecipModel? y)
    {
        return x?.Id == y?.Id
            && x?.Name == y?.Name
            && x?.Image == y?.Image
            && x?.PersonNumber == y?.PersonNumber
            && x?.CookingDuration == y?.CookingDuration;
    }

    public int GetHashCode([DisallowNull] RecipModel obj)
    {
        return obj.GetHashCode();
    }
}
