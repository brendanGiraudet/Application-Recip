using ms_recip.Ms_recip.Models;
using System.Diagnostics.CodeAnalysis;

namespace application_recip.EqualityComparers;

public class CategoryEqualityComparer : IEqualityComparer<CategoryModel>
{
    public bool Equals(CategoryModel? x, CategoryModel? y)
    {
        return x?.Id == y?.Id
            && x?.Name == y?.Name;
    }

    public int GetHashCode([DisallowNull] CategoryModel obj)
    {
        return obj.GetHashCode();
    }
}
