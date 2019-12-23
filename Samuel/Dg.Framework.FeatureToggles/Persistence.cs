using System.Collections.Generic;

namespace Dg.Framework.FeatureToggles
{
    internal static class Persistence
    {
        public static IList<FeatureToggle> LoadAll() =>
            new List<FeatureToggle> 
            {
                new FeatureToggle("FeatureAActive", false, new HashSet<int> { 1, 2 }),
                new FeatureToggle("ChristmasActive", true, new HashSet<int> { 2, 3 })
            };
    }

    internal readonly struct FeatureToggle
    {
        public readonly string Id;
        public readonly bool IsActive;
        public readonly ISet<int> ActivatedUsers;

        public FeatureToggle(string id, bool state, ISet<int> activatedUsers)
        {
            Id = id;
            IsActive = state;
            ActivatedUsers = activatedUsers;
        }
    }
}