namespace Dg.Framework.FeatureToggles.Api
{
    public class FeatureToggleResponse
    {
        public string Id { get; }
        public bool IsActive { get; }

        public FeatureToggleResponse(string id, bool isActive)
        {
            Id = id;
            IsActive = isActive;
        }
    }
}