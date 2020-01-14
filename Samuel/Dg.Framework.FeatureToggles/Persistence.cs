using System.Collections.Generic;

namespace Dg.Framework.FeatureToggles
{
    internal static class Persistence
    {
        public static IReadOnlyList<FeatureToggle> LoadAll() =>
            new List<FeatureToggle> 
            {
                new FeatureToggle("FeatureAActive", false, new List<int> { 1, 2 }.AsReadOnly()),
                new FeatureToggle("ChristmasActive", true, new List<int> { 2, 3 }.AsReadOnly())
            }.AsReadOnly();
    }

    public readonly struct FeatureToggle
    {
        public readonly string Id;
        public readonly bool IsActive;
        public readonly IReadOnlyList<int> ActivatedUsers;

        public FeatureToggle(string id, bool state, IReadOnlyList<int> activatedUsers)
        {
            Id = id;
            IsActive = state;
            ActivatedUsers = activatedUsers;
        }
    }
}