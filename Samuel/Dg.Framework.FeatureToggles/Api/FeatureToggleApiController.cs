using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using static Dg.Framework.FeatureToggles.Persistence;
using static Dg.Framework.FeatureToggles.Transformations;


namespace Dg.Framework.FeatureToggles.Api
{
    public class FeatureToggleApiController : ControllerBase
    {
        [Route("v1/featureToggles/{featureToggleId}/users/{userId}")]
        public IActionResult Get([FromRoute] string featureToggleId, [FromRoute] int userId)
        {
            var featureToggles = LoadAll().Where(ft => ft.Id == featureToggleId).ToList();
            var ft = featureToggles.Count() == 1 ? (FeatureToggle?)featureToggles[0] : null;
            if (ft == null)
            {
                return NotFound();
            }

            var activeStatus = ToActiveStatusForUser(ft.Value, userId);

            return Ok(new FeatureToggleResponse(activeStatus.FeatureToggleId, activeStatus.IsActive));
        }
    }
}