using System.Diagnostics.Contracts;

namespace Dg.Framework.FeatureToggles
{
    internal static class BusinessRules
    {
        [Pure]
        public static bool IsActiveForUser(FeatureToggle featureToggle, int userId) =>
            featureToggle.IsActive && featureToggle.ActivatedUsers.Contains(userId);
    }
}