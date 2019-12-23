using System.Diagnostics.Contracts;
using Newtonsoft.Json;

namespace Dg.Framework.FeatureToggles
{
    internal static class Transformations
    {
        [Pure]
        public static UserActiveStatus ToActiveStatusForUser(FeatureToggle featureToggle, int userId) => 
            new UserActiveStatus(featureToggle.Id, userId, featureToggle.IsActive && featureToggle.ActivatedUsers.Contains(userId));
    }

    internal readonly struct UserActiveStatus
    {
        public readonly string FeatureToggleId;
        public readonly int UserId;
        public readonly bool IsActive;

        public UserActiveStatus(string featureToggleId, int userId, bool isActive)
        {
            FeatureToggleId = featureToggleId;
            UserId = userId;
            IsActive = isActive;
        }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}