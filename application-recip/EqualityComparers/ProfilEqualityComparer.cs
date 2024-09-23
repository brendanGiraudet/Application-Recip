using ms_recip.Ms_recip.Models;
using System.Diagnostics.CodeAnalysis;

namespace application_recip.EqualityComparers;

public class ProfilEqualityComparer : IEqualityComparer<ProfilModel>
{
    public bool Equals(ProfilModel? x, ProfilModel? y)
    {
        return x?.Id == y?.Id
            && x?.Name == y?.Name;
    }

    public int GetHashCode([DisallowNull] ProfilModel obj)
    {
        return obj.GetHashCode();
    }
}
