using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Chabis.Functional;
using static Dg.Framework.FeatureToggles.Persistence;
using static Dg.Framework.FeatureToggles.Transformations;
using System.Net;

namespace Dg.Framework.FeatureToggles.Api
{
    public class FeatureToggleApiController : ControllerBase
    {
        [Route("v1/featureToggles/{featureToggleId}/users/{userId}")]
        public IActionResult Get([FromRoute] string featureToggleId, [FromRoute] int userId) =>
            GetFeatureToggle(featureToggleId)
                .Map(ft => ToActiveStatusForUser(ft, userId))
                .Map(a => new FeatureToggleResponse(a.FeatureToggleId, a.IsActive))
                .Match<IActionResult>(Ok, err => StatusCode((int)err));

        private static Result<FeatureToggle, HttpStatusCode> GetFeatureToggle(string featureToggleId)
        {
            var featureToggle = LoadAll().Where(ft => ft.Id == featureToggleId).ToList();
            return featureToggle.Count == 1
                ? (Result<FeatureToggle, HttpStatusCode>)featureToggle[0]
                : HttpStatusCode.NotFound;
        }
    }
}