using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Chabis.Functional;
using static Dg.Framework.FeatureToggles.BusinessRules;
using System.Net;
using System.Collections.Generic;

namespace Dg.Framework.FeatureToggles.Api
{
    public class FeatureToggleApiController : ControllerBase
    {
        public delegate IList<FeatureToggle> LoadFeatureToggles();

        private readonly LoadFeatureToggles loadFeatureToggles;

        public FeatureToggleApiController(LoadFeatureToggles loadFeatureToggles)
        {
            this.loadFeatureToggles = loadFeatureToggles;
        }

        [Route("v1/featureToggles/{featureToggleId}/users/{userId}")]
        public IActionResult Get([FromRoute] string featureToggleId, [FromRoute] int userId) =>
            GetFeatureToggle(featureToggleId)
                .Map(ft => IsActiveForUser(ft, userId))
                .Map(active => new FeatureToggleResponse(featureToggleId, active))
                .Match<IActionResult>(Ok, err => StatusCode((int)err));

        private Result<FeatureToggle, HttpStatusCode> GetFeatureToggle(string featureToggleId)
        {
            var featureToggle = loadFeatureToggles().Where(ft => ft.Id == featureToggleId).ToList();
            return featureToggle.Count == 1
                ? (Result<FeatureToggle, HttpStatusCode>)featureToggle[0]
                : HttpStatusCode.NotFound;
        }
    }
}